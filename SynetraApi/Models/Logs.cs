using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynetraApi.Models
{
    public class Logs
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users User { get; set; }
        public string Action { get; set; }
        public string SqlRequest { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
