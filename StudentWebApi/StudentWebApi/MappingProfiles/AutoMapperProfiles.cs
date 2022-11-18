using AutoMapper;
using StudentWebApi.DataModels;
using StudentWebApi.DomainModels;
using StudentWebApi.MappingProfiles.AfterMaps;

namespace StudentWebApi.MappingProfiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<UpdateStudentRequestDTO, Student>().AfterMap<UpdateStudentRequestDTOAfterMap>();
            CreateMap<AddStudentRequestDTO, Student>().AfterMap<AddStudentRequestDTOAfterMap>();

        }
    }
}
