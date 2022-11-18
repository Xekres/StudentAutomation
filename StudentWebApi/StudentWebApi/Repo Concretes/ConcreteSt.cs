using Microsoft.EntityFrameworkCore;
using StudentWebApi.DataModels;
using StudentWebApi.DomainModels;
using StudentWebApi.Repositories;

namespace StudentWebApi.Repo_Concretes
{

    public class ConcreteSt : IStudentRepository
    {
        private readonly DataContext _context;
        public ConcreteSt(DataContext context)
        {
            _context = context;
        }

        public async Task<Student> AddStudentAsync(Student request)
        {
            var student = await _context.Students.AddAsync(request);
            await _context.SaveChangesAsync();
            return student.Entity;
            //eklediğimin geri dönüşünü aldım.
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            //yapı olarak Update student taki gibi..eğer öğrenci varsa gidip DB den silsin,yoksa return null dönsün
            var existingStudent=await GetStudentByIdAsync(studentId);
            if (existingStudent!=null)
            {
                _context.Students.Remove(existingStudent);
                await _context.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        public async Task<bool> Exist(Guid studentId)
        {
            return await _context.Students.AnyAsync(x => x.Id == studentId);
            //içerisinde studentId ye eşit olan bir data var mı diye kontrol ettik

        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(Guid studentId)
        {
            return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(x => x.Id == studentId);
            //yani liste olarak değil ID si StudentID ye eşit olanı getircem

        }

        public async Task<List<Student>> GetStudentsAsync()
        {

            return await _context.Students.ToListAsync();


        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentByIdAsync(studentId);
            if (student!=null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
            //eğer böyle bir ögrenci var ise true dön ve işlemlerimi tamamla
        }

        public async Task<Student?> UpdateStudent(Guid studentId, Student request)
        {
            //şimdi bir değişken oluşturup içerisine eşleme yapalım
            var existingStudent = await GetStudentByIdAsync(studentId);
            if (existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.Address.PhsicalAddress = request.Address.PhsicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;
                existingStudent.GenderId = request.GenderId;
                await _context.SaveChangesAsync();
                return existingStudent;

            }
            return null;
        }


    }
}
