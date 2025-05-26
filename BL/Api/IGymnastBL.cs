using BL.Models;
using BL.Services;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IGymnastBL
    {
        public void NewGymnast(M_Gymnast gymnast);
        //public bool UpdateGymnast(string id, Gymnast updatedGymnast);

        public void AddMembershipType(string id, MembershipTypeEnum membershipType);
        public List<M_ViewGymnastBL> GetAllGymnast();
        public void RemoveGymnastFromClass(string gymnastId, int classId);
        public Gymnast GetGymnastById(string id);
        public void UpdateGymnanst(M_Gymnast m_Gymnast);
        public void DeleteGymnast(string id);
      
        public void AddGymnastLesson(string gymnastId,StudioClass studioClass);
        public void RemoveGymnastFromLesson(string gymnastId, StudioClass studioClass);
        public List<M_ViewStudioClasses> GetGymnastLessons(string gymnastId,int numOfLesson);
        public List<M_ViewContactGymnast> GetAllGymnastInSpecificClass(StudioClass studioClass);



    }
}
