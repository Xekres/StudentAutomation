using StudentWebApi.DomainModels;

namespace StudentWebApi.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(Guid studentId);
        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exist(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId, Student student);
        Task<Student> DeleteStudent(Guid studentId);
        Task<Student> AddStudentAsync(Student student);

        //image için tanımlamalarım bittikten sonra burada işlemlerimi tamamlıyorum
        //bunu profilImage i updatelemek için yazıcam
        Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl);
        //parametrelerim studentId ve profileImageUrl.
        
    }
}
