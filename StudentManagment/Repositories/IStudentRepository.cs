using StudentManagment.Model;

namespace StudentManagment.Repositories
{
    public interface IStudentRepository
    {
       Task<List<Students>> GetAllStudentAsync();

        Task <Students>GetStudentByIdAsync(int StuId);

        Task <Students>CreateStudentAsync(Students students);

        Task <Students?>UpdateStudentAsync(Students students,int stuId);
    }
}
