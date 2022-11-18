using AutoMapper;
using StudentWebApi.DataModels;
using StudentWebApi.DomainModels;

namespace StudentWebApi.MappingProfiles.AfterMaps
{
    public class AddStudentRequestDTOAfterMap : IMappingAction<AddStudentRequestDTO, Student>
    {
        public void Process(AddStudentRequestDTO source, Student destination, ResolutionContext context)
        {
            //öncelikle destination address i student adresime eşitlemeliyim.
            //student id ye de random bir değer ataması yaptıktan sonra
            //database de kaydolurken addresin id si oldugu için yeni bir id de türetmeliyim

            destination.Id=Guid.NewGuid();
            destination.Address = new Address()
            {
                Id = Guid.NewGuid(),
                PhsicalAddress = source.PhsicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
