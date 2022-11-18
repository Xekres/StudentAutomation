using AutoMapper;
using StudentWebApi.DataModels;
using StudentWebApi.DomainModels;

namespace StudentWebApi.MappingProfiles.AfterMaps
{
    public class UpdateStudentRequestDTOAfterMap : IMappingAction<UpdateStudentRequestDTO, Student>
    {
        public void Process(UpdateStudentRequestDTO source, Student destination, ResolutionContext context)
        {
            destination.Address = new Address()
            {
                PhsicalAddress = source.PhsicalAddress,
                PostalAddress = source.PostalAddress
            };
                
            }
        }
    }
