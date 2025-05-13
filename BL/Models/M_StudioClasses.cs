using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class M_StudioClasses
    {
        [Required]

        public string Name { get; set; } = null!;
        [Required]

        public string Level { get; set; } = null!;
        [Required]

        public DateTime Date { get; set; }



    }
}
