using BL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IGymnastBL
    {
        public bool NewGymnast(Gymnast gymnast);
        public void AddMembershipType(string id, MembershipTypeEnum membershipType);
        public List<ModelGymnastBL> GetAllGymnast();

        public bool RemoveGymnastFromClass(string gymnastId, int classId);
   
    }
}
