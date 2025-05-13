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
            return _dbManager.Gymnasts.FirstOrDefault(g => g.Id.Equals(id));
        }

        public void AddMembershipType(string id, MembershipTypeEnum membershipType)
        {

            Gymnast gymnast = GetGymnastById(id);

            if (gymnast != null)
            {
                gymnast.MemberShipType = membershipType.ToString();

                _dbManager.SaveChanges();
            }
            else
            {
                throw new Exception($"Gymnast with ID {id} not found.");
            }
        }

        public bool NewGymnast(Gymnast gymnast)
        {
            try
            {
                _dbManager.Gymnasts.Add(gymnast);

                _dbManager.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public List<Gymnast> GetAllGymnast()
        {
            return _dbManager.Gymnasts.ToList();
        }
        public bool RemoveGymnastFromClass(string gymnastId, int classId)
        {
            var registration = _dbManager.GymnastClasses
        .FirstOrDefault(gc => gc.GymnastId == gymnastId && gc.ClassId == classId);
            if (registration != null)
            {
                _dbManager.GymnastClasses.Remove(registration);

                var studioClass = _dbManager.StudioClasses.Find(classId);
                if (studioClass != null && studioClass.ParticipantsNumber > 0)
                {
                    studioClass.ParticipantsNumber--;
                }

                _dbManager.SaveChanges();
                return true;

            }

            return false;





        }
        public bool UpdateGymnast(string id, Gymnast updatedGymnast)
        {
            var gymnast1 = _dbManager.Gymnasts.FirstOrDefault(g => g.Id == id);
            if (gymnast1 == null)
               return false;
            gymnast1.FirstName = updatedGymnast.FirstName;
            gymnast1.LastName = updatedGymnast.LastName;
            gymnast1.BirthDate = updatedGymnast.BirthDate;
            gymnast1.MedicalInsurance = updatedGymnast.MedicalInsurance;
            gymnast1.Level = updatedGymnast.Level;
            gymnast1.MemberShipType = updatedGymnast.MemberShipType;
            gymnast1.StudioClasses = updatedGymnast.StudioClasses = updatedGymnast.StudioClasses;;
            gymnast1.PaymentType = updatedGymnast.PaymentType;

            _dbManager.SaveChanges();
            return true;
        }
    }



}

