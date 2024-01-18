using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SynetraApi.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser<int>
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsEnable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
