using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface ITrainerDal
    {
        public int GetClassId(string  trainerId, DateTime courseDate);

        public List<string> GetGymnasts(int classId);

        public List<Trainer> GetList();
    }
}
