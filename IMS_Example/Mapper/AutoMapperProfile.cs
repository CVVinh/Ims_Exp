using AutoMapper;
using IMS_Example.Data.DTOs.ProjectDTO;
using IMS_Example.Data.Models;

namespace IMS_Example.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Projects, AddNewProjectDTO>().ReverseMap();
            CreateMap<Projects, EditProjectDTO>().ReverseMap();
        }
    }
}
