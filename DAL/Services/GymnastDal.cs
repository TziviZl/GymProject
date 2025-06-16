using DAL.Api;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
                .Include(gc => gc.Class)
                    .ThenInclude(sc => sc.Global)
                        .ThenInclude(g => g.Trainer)
                .Where(gc => gc.GymnastId == id)
                .ToList();
        }
        public void RemoveGymnastClass(GymnastClass gymnast)
        {
            _dbManager.GymnastClasses.Remove(gymnast);
        }
        public void AddGymnastLesson(string gymnastId, int classId)
        {
            var gymnastClass = new GymnastClass
            {
                GymnastId = gymnastId,
                ClassId = classId
            };
            _dbManager.GymnastClasses.Add(gymnastClass);
            SaveChanges();
        }

        public List<string> GetAllGymnastInSpecificClass(int classId)
        {
            return _dbManager.GymnastClasses
                .Where(gc => gc.ClassId == classId)
                .Select(gc => gc.GymnastId.Trim()) 
                .ToList();
        }



    }

}

