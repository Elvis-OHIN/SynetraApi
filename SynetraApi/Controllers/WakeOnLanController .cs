using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SynetraUtils.Models.MessageManagement;
using System.Net.Sockets;

namespace SynetraApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    [ApiController]
    public class WakeOnLanController : ControllerBase
    {
        /// <summary>
        /// Envoie un paquet Magic Wake-on-LAN pour réveiller un ordinateur.
        /// </summary>
        /// <param name="request">Les détails de la demande contenant l'adresse de diffusion et l'adresse MAC.</param>
        /// <returns>Retourne un message de succès si le paquet Magic est envoyé, sinon une erreur.</returns>
        [HttpPost]
        public async Task<IActionResult> Wake([FromBody] WakeRequest request)
        {
            try
            {
                SendWakeOnLan(request.BroadcastAddress, request.MacAddress);
                return Ok("Magic Packet envoyé.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private static void SendWakeOnLan(string broadcastAddress, string macAddress)
        {
            byte[] macBytes = GetMacBytes(macAddress);

            byte[] packet = new byte[102];

            for (int i = 0; i < 6; i++)
            {
                packet[i] = 0xFF;
            }

            for (int i = 1; i <= 16; i++)
            {
                Buffer.BlockCopy(macBytes, 0, packet, i * 6, macBytes.Length);
            }

            using (UdpClient client = new UdpClient())
            {
                client.EnableBroadcast = true;
                client.Send(packet, packet.Length, broadcastAddress, 9);
            }
        }

        private static byte[] GetMacBytes(string macAddress)
        {
            string[] macAddressParts = macAddress.Split('-');
            if (macAddressParts.Length != 6)
            {
                throw new ArgumentException("Adresse MAC non valide.");
            }

            byte[] macBytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                macBytes[i] = Convert.ToByte(macAddressParts[i], 16);
            }
            return macBytes;
        }
    }
}
