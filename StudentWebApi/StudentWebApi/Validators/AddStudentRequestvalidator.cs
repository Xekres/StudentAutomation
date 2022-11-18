using FluentValidation;
using StudentWebApi.DataModels;
using StudentWebApi.Repositories;

namespace StudentWebApi.Validators
{
    public class AddStudentRequestvalidator:AbstractValidator<AddStudentRequestDTO>
    {
        public AddStudentRequestvalidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).NotEmpty();
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList()
                .FirstOrDefault(x => x.Id == id);

                if (gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please select a valid Gender");
            RuleFor(x => x.PhsicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();

        }

    }
}
