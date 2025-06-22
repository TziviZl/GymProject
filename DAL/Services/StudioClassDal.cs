using DAL.Api;
using DAL.Models;
using DAL.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{



    public class StudioClassDal : IStudioClassDal
    {
        private readonly DB_Manager _dbManager;

        public StudioClassDal(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        public List<StudioClass> GetAllLessons()
        {
            return _dbManager.StudioClasses
                .Include(sc => sc.Global)
                    .ThenInclude(g => g.Trainer)
                .ToList();
        }

        public StudioClass GetById(int studioClassId)
        {
            Console.WriteLine(studioClassId);
            return _dbManager.StudioClasses.Where(sc => sc.Id == studioClassId).FirstOrDefault();
        }

        public void CancelStudioClass(int classId)
        {
            var studioClass = _dbManager.StudioClasses.FirstOrDefault(sc => sc.Id == classId);
            if (studioClass != null)
            {
                studioClass.IsCancelled = true;
                _dbManager.SaveChanges();
            }
        }

        public bool IsCancelled(int classId)
        {
            var studioClass = _dbManager.StudioClasses.FirstOrDefault(s => s.Id == classId);
            return studioClass?.IsCancelled ?? false;
        }




    }
}