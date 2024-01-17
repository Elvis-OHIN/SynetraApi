using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynetraApi.Models
{
    public class Rooms
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
        [ForeignKey("Parcs")]
        public int ParcsId { get; set; }
        public Parcs Parcs { get; set; }
        public ICollection<Rooms> rooms { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
