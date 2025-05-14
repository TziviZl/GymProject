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
    public class GymnastBL : IGymnastBL
    {
        private readonly IGymnastDal _gymnastDal;

        public GymnastBL(IGymnastDal gymnastDal)
        {
            _gymnastDal = gymnastDal;
        }

        public void AddMembershipType(string id, MembershipTypeEnum membershipType)
        {

            Gymnast gymnast = _gymnastDal.GetGymnastById(id);
            if (gymnast != null)
            {
                _gymnastDal.AddMembershipType(gymnast, membershipType);
                _gymnastDal.SaveChanges();
            }

            else
            {
                throw new Exception($"Gymnast with ID {id} not found.");
            }

        }

        public bool NewGymnast(Gymnast gymnast)
        {
            gymnast.Level = "A";
            if (gymnast != null)
            {
                _gymnastDal.AddGymnast(gymnast);
                _gymnastDal.SaveChanges();
                return true;
            }
             return false;

        }

        public bool RemoveGymnastFromClass(string gymnastId, int classId)
        {
            var gymnastClass = _gymnastDal.GetGymnastClass(gymnastId, classId);
            if (gymnastClass == null)
            {
                return false;
            }

            _gymnastDal.RemoveGymnastFromClass(gymnastClass);

            var studioClass = _gymnastDal.GetStudioClass(classId);
            if (studioClass != null && studioClass.CurrentNum > 0)
            {
                studioClass.CurrentNum--;
            }

            _gymnastDal.SaveChanges();

            return true;
        }

        public List<M_Gymnast> GetAllGymnast()
        {
            var previous = _gymnastDal.GetAllGymnast();
            List<M_Gymnast> updatedG = new();
            previous.ForEach(t => updatedG.Add
                (new M_Gymnast()
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

        public bool UpdateGymnast(string id, Gymnast updatedGymnast)
        {
            return _gymnastDal.UpdateGymnast(id, updatedGymnast);
        }
    }
}
