using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagment.Data;
using StudentManagment.Data.DTO;
using StudentManagment.Model;
using StudentManagment.Repositories;

namespace StudentManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDbContext dbContext;
        private readonly IStudentRepository studentRepository;

        public StudentController(StudentDbContext dbContext, IStudentRepository studentRepository)
        {
            this.dbContext = dbContext;
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get All the data in domain from Database
            var StudentDomain = await studentRepository.GetAllStudentAsync();

            //Convert Domain into DTO 
            var StudentDTO = new List<StudentsDto>();

            foreach (var student in StudentDomain) 
            {
                StudentDTO.Add(new StudentsDto()
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    Department = student.Department,
                    PassoutYear = student.PassoutYear,
                });
                
            }

            return Ok(StudentDTO);
           
        }

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            // check its exist or not
            var StudentsDomain = await studentRepository.GetStudentByIdAsync(id);

            if (StudentsDomain == null) 
            {
                return NotFound();
            }

            //Map Student Domain to Student Dto 

            var StudentDTO = new StudentsDto()
            {
                StudentId = StudentsDomain.StudentId,
                StudentName = StudentsDomain.StudentName,
                Department = StudentsDomain.Department,
                PassoutYear = StudentsDomain.PassoutYear,
            };

            return Ok(StudentDTO);

        }

        [HttpPost]

        public async Task <IActionResult> AddStudent([FromBody] AddStudentRequestDto addStudentRequestDto)
        {
            //Map DTO to Domain Model 
            var StudentDomain = new Students
            {
                StudentName = addStudentRequestDto.StudentName,
                Department = addStudentRequestDto.Department,
                PassoutYear = addStudentRequestDto.PassoutYear,
            };

            await studentRepository.CreateStudentAsync(StudentDomain);

            //Map Domain model to DTO 

            var StudentDTO = new StudentsDto()
            {
                StudentId = StudentDomain.StudentId,
                StudentName = StudentDomain.StudentName,
                Department = StudentDomain.Department,
                PassoutYear = addStudentRequestDto.PassoutYear,
            };


            return Ok(StudentDTO);
              
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentRequestDto updateStudentRequestDto)
        {
            //Map DTO to Domain model 

            var StudentDomain = new Students
            {
             
                StudentName = updateStudentRequestDto.StudentName,
                Department = updateStudentRequestDto.Department,
                PassoutYear = updateStudentRequestDto.PassoutYear,
            };
            //check if id exist or not.?
            StudentDomain = studentRepository.UpdateStudentAsync(id, StudentDomain);

            if (StudentDomain == null) 
            {
                return NotFound();
            }

            dbContext.SaveChanges();

            //Map Domain to DTO 
            var StudentDTO = new StudentsDto
            {
                StudentId = StudentDomain.StudentId,
                StudentName = StudentDomain.StudentName,
                Department = StudentDomain.Department,
                PassoutYear = updateStudentRequestDto.PassoutYear,
            };

            return Ok(StudentDTO);

        }


        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteStudent([FromRoute] int id) 
        { 
            //check if Student data exist or not
            var StudentDomain = await dbContext.StudentsData.FirstOrDefaultAsync(x => x.StudentId==id);

            if (StudentDomain == null) 
            {
                return NotFound();
            }

            dbContext.StudentsData.Remove(StudentDomain);
            dbContext.SaveChanges();


            //its optional step if you want see the data into console then you can use it or skip it

            //Map domain to Dto 
            var StudentDTO = new StudentsDto
            {
                StudentId = StudentDomain.StudentId,
                StudentName = StudentDomain.StudentName,
                Department = StudentDomain.Department,
                PassoutYear = StudentDomain.PassoutYear,
            };

            return Ok(StudentDTO);
        }



    }
}
