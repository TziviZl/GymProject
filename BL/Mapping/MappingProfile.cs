using AutoMapper;
using BL.Models;
using BL.Services;
using DAL.Models;
using DAL.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Gymnast, GymnastBL>();
            CreateMap<GymnastBL, Gymnast>();
            CreateMap<Gymnast, M_ViewGymnastBL>();
            CreateMap<M_ViewGymnastBL, Gymnast>();
            CreateMap<Trainer, M_Trainer>();
            CreateMap<M_Trainer, Trainer>();
        }
    }
}