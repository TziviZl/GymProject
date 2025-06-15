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
    public class StudioClassBL:IStudioClassBL
    {
        private readonly IStudioClassDal _StudioClass;

        private readonly IMapper _mapper;
        public StudioClassBL(IStudioClassDal StudioClass, IMapper mapper)
        {
            _StudioClass = StudioClass;
            _mapper = mapper;
        }
        public List<M_ViewStudioClasses> GetAllLessons()
        {
            var studioClasses = _StudioClass.GetAllLessons();

            if (studioClasses == null || !studioClasses.Any())
            {
                return new List<M_ViewStudioClasses>();
            }

            return _mapper.Map<List<M_ViewStudioClasses>>(studioClasses);
        }


    }
}
