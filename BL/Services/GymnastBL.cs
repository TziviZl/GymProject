using BL.Api;
using BL.Models;
using DAL.Api;
using DAL.Models;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class GymnastBL:IGymnastBL
    {
        private readonly IGymnastDal _gymnastDal;

        public GymnastBL(IGymnastDal gymnastDal)
        {
            _gymnastDal = gymnastDal;
        }

        public void AddMembershipType(string id, MembershipTypeEnum membershipType)
        {
            _gymnastDal.AddMembershipType(id, membershipType);
        }

        public bool NewGymnast(Gymnast gymnast)
        {
            gymnast.Level = "A";
            
         return  _gymnastDal.NewGymnast(gymnast);
        }
        public List<ModelGymnastBL> GetAllGymnast()
        {
            var previous = _gymnastDal.GetAllGymnast();
            List<ModelGymnastBL> updatedG = new();
            previous.ForEach(t => updatedG.Add
                (new ModelGymnastBL()
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Level = t.Level,
                   

                }));
            return updatedG;
            // נלך לדל
            // נביא נתוני מאמנים
            // נערוך אותם למבנה הרצוי
            //ונחזיר

        }
    }
}
