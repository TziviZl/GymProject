using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class M_ViewTrainerBL
    {

        public string Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

      
        public string Level { get; set; } = null!;

        public int NumOfStudioClasses { get; set; } 

    }
}
