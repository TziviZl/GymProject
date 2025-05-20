using AutoMapper;
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
        private readonly IMapper _mapper;


        public GymnastBL(IGymnastDal gymnastDal, IMapper mapper)
        {
            _gymnastDal = gymnastDal;
            _mapper = mapper;
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

        public bool NewGymnast(M_Gymnast m_gymnast)
        {
            var gymnast = _mapper.Map<Gymnast>(m_gymnast);
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

        public List<M_ViewGymnastBL> GetAllGymnast()
        {
            var dalGymnasts = _gymnastDal.GetAllGymnast();
            var blGymnasts = _mapper.Map<List<M_ViewGymnastBL>>(dalGymnasts);
            return blGymnasts;
        }


        // נלך לדל
        // נביא נתוני מאמנים
        // נערוך אותם למבנה הרצוי
        //ונחזיר



        //public bool UpdateGymnast(string id, Gymnast updatedGymnast)
        //{
        //    return _gymnastDal.UpdateGymnast(id, updatedGymnast);
        //}

    }
}
