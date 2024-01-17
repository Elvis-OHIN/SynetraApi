using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SynetraApi.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<Logs> logs { get; set; }
    }
}
