using Microsoft.EntityFrameworkCore;
using StudentManagment.Model;

namespace StudentManagment.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Students> StudentsData { get; set; }
    }
}
