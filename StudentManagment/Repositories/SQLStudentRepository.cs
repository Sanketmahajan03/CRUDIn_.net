using Microsoft.EntityFrameworkCore;
using StudentManagment.Data;
using StudentManagment.Model;

namespace StudentManagment.Repositories
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly StudentDbContext dbContext;

        public SQLStudentRepository(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Students> CreateStudentAsync(Students students)
        {
            await dbContext.AddAsync(students);
            await dbContext.SaveChangesAsync();
            return students;
        }

        public async Task<List<Students>> GetAllStudentAsync()
        {
           return await dbContext.StudentsData.ToListAsync();
        }

        public async Task<Students> GetStudentByIdAsync(int StuId)
        {
            var Student = await dbContext.StudentsData.FirstOrDefaultAsync(x => x.StudentId == StuId);

            if (Student == null) 
            { 
                return null; 
            }

            return Student;
        }

        public async Task<Students?> UpdateStudentAsync(Students students, int stuId)
        {
           var StudentDomain = dbContext.StudentsData.FirstOrDefaultAsync(x => x.StudentId == stuId);

            if (StudentDomain == null) 
            { 
                return null; 
            }

            StudentDomain.StudentId = students.StudentId;
            StudentDomain.StudentName = students.StudentName;
            StudentDomain.Department = students.Department;
            StudentDomain.PassoutYear = students.PassoutYear;

            await dbContext.SaveChangesAsync();
            return students;

        }
    }
}
