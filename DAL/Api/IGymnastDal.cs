using DAL.Models;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IGymnastDal
    {
        public bool NewGymnast(Gymnast gymnast);
        public void AddMembershipType(string id, MembershipTypeEnum membershipType);
        public bool IsExistId(string id);
        public Gymnast GetGymnastById(string id);

    }
}
