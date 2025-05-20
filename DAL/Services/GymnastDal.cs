using DAL.Api;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class GymnastDal : IGymnastDal
    {
        private readonly DB_Manager _dbManager;
        public GymnastDal(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        public bool IsExistId(string id)
        {
            return _dbManager.Gymnasts.Any(g => g.Id.Equals(id));
        }
        public Gymnast GetGymnastById(string id)
        {
            return _dbManager.Gymnasts.Find(id);
        }
        public void AddMembershipType(Gymnast gymnast, MembershipTypeEnum membershipType)
        {
            gymnast.MemberShipType = membershipType.ToString();
        }


        public void AddGymnast(Gymnast gymnast)
        {
            _dbManager.Gymnasts.Add(gymnast);
        }

        public List<Gymnast> GetAllGymnast()
        {
            return _dbManager.Gymnasts.ToList();
        }
        public void RemoveGymnastFromClass(GymnastClass gymnastClass)
        {
            _dbManager.GymnastClasses.Remove(gymnastClass);
        }
        public GymnastClass GetGymnastClass(string gymnastId, int classId)
        {
            return _dbManager.GymnastClasses
                .FirstOrDefault(gc => gc.GymnastId == gymnastId && gc.ClassId == classId);
        }

        public StudioClass GetStudioClass(int classId)
        {
            return _dbManager.StudioClasses.Find(classId);
        }

        public void SaveChanges()
        {
            _dbManager.SaveChanges();
        }
        public void UpdateGymnast(Gymnast gymnast)
        {
            var existing = GetGymnastById(gymnast.Id);
            _dbManager.Entry(existing).CurrentValues.SetValues(gymnast);
        }
        public void DeleteGymnast(string gymnastId)
        {
            var gymnast = _dbManager.Gymnasts.Find(gymnastId);
            if (gymnast != null)
            {
                _dbManager.Gymnasts.Remove(gymnast);
            }
        }
        public List<GymnastClass> GetGymnastClassesByStudentId(string id)
        {
            return _dbManager.GymnastClasses
                .Where(gc => gc.GymnastId == id)
                .ToList();
        }

        public void RemoveGymnastClass(GymnastClass gymnast)
        {
            _dbManager.GymnastClasses.Remove(gymnast);
        }

        //public bool UpdateGymnast(string id, Gymnast updatedGymnast)
        //{
        //    var gymnast1 = _dbManager.Gymnasts.FirstOrDefault(g => g.Id == id);
        //    if (gymnast1 == null)
        //        return false;
        //    gymnast1.FirstName = updatedGymnast.FirstName;
        //    gymnast1.LastName = updatedGymnast.LastName;
        //    gymnast1.BirthDate = updatedGymnast.BirthDate;
        //    gymnast1.MedicalInsurance = updatedGymnast.MedicalInsurance;
        //    gymnast1.Level = updatedGymnast.Level;
        //    gymnast1.MemberShipType = updatedGymnast.MemberShipType;
        //    gymnast1.StudioClasses = updatedGymnast.StudioClasses = updatedGymnast.StudioClasses; ;
        //    gymnast1.PaymentType = updatedGymnast.PaymentType;

        //    _dbManager.SaveChanges();
        //    return true;
        //}

    }

}

