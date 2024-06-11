using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynetraApi.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string Action { get; set; }
        public string SqlRequest { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
