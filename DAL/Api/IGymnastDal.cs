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
        public void AddGymnast(Gymnast gymnast);
        public void AddMembershipType(Gymnast gymnast, MembershipTypeEnum membershipType);
        public bool IsExistId(string id);
        public Gymnast GetGymnastById(string id);
        public List<Gymnast> GetAllGymnast();
        public void RemoveGymnastFromClass(GymnastClass gymnastClass);
        //public bool UpdateGymnast(string id, Gymnast updatedGymnast);
        public void SaveChanges();
        public StudioClass GetStudioClass(int classId);
        public GymnastClass GetGymnastClass(string gymnastId, int classId);
        public void UpdateGymnast(Gymnast gymnast);
        public List<GymnastClass> GetGymnastClassesByStudentId(string id);

        public void RemoveGymnastClass(GymnastClass gymnast);
        public void DeleteGymnast(string gymnastId);



    }
}
