using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynetraApi.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        [ForeignKey("Parc")]
        public int ParcsId { get; set; }
        public Parc Parcs { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
