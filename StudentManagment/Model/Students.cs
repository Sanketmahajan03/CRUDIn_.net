using System.ComponentModel.DataAnnotations;

namespace StudentManagment.Model
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public string PassoutYear { get; set; }
    }
}
