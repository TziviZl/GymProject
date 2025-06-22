using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Secretary
    {
        [Key]
        [StringLength(9)]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;
    }
}

