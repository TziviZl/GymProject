using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class M_Gymnast
    {
        [Required, StringLength(9, MinimumLength = 9)]
        public string Id { get; set; } = null!;

        [StringLength(20, MinimumLength = 2), Required]
        public string FirstName { get; set; } = null!;

        [StringLength(20, MinimumLength = 2), Required]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string MedicalInsurance { get; set; }


        [EmailAddress]
            public string Email { get; set; }

        [Phone]
        public string Cell { get; set; }

        public char Level { get; set; }
        public string? MemberShipType { get; set; } 
        public int WeeklyCounter { get; set; }


    }
}
