using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface ITrainerBL
    {
        public int GetNumOfGymnasts(string trainerId, DateTime courseDate);
        public List<ModelTrainerBL> GetList();
      }
}
