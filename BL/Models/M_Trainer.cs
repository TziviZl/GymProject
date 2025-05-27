using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class M_Trainer
    {
        [Required]
        [StringLength(9)]
        public string Id { get; set; } = null!;

        [StringLength(20, MinimumLength = 2), Required]
        public string FirstName { get; set; } = null!;

        [StringLength(20, MinimumLength = 2), Required]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Specialization { get; set; }

        [Required] // Only if your DB requires it
        [StringLength(50)]
        public string Email { get; set; } = null!;

        [Required] // Only if your DB requires it
        [StringLength(10)]
        public string Cell { get; set; } = null!;
    }
}