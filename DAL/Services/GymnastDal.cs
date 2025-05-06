using DAL.Api;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class GymnastDal:IGymnastDal
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
    }
}
