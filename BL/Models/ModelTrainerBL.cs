using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{// הנתונים כפי שרוצים לראות אותם
    public class ModelTrainerBL
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

      
        public string Level { get; set; } = null!;

        public int NumOfStudioClasses { get; set; } 

    }
}
