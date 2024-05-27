using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SynetraUtils.Models.MessageManagement;
using System.Net.Sockets;

namespace SynetraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WakeOnLanController : ControllerBase
    {
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
            // Convertir l'adresse MAC en tableau de bytes
            byte[] macBytes = GetMacBytes(macAddress);

            // Créer un paquet magique (Magic Packet)
            byte[] packet = new byte[102];
            // Remplir les 6 premiers bytes avec 0xFF
            for (int i = 0; i < 6; i++)
            {
                packet[i] = 0xFF;
            }
            // Ajouter l'adresse MAC 16 fois
            for (int i = 1; i <= 16; i++)
            {
                Buffer.BlockCopy(macBytes, 0, packet, i * 6, macBytes.Length);
            }

            // Envoyer le paquet magique
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
