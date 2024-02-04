using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynetraApi.Models
{
    public class ShareScreen
    {
        [Key]
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
