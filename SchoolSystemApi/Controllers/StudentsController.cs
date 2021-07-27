using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Business.Business.Contract;
using StudentSystem.Abstractions.Models;
using System.Net;

namespace SchoolSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentBusiness studentBusiness;
        public StudentsController(IStudentBusiness student)
        {
            studentBusiness = student;
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            var students = studentBusiness.ReadAllStudents();
            if (students == null)
                return StatusCode((int)HttpStatusCode.NotFound, "The database is empty");
            else
                return Ok(students);
        }

        /// <summary>
        /// Get specific student by Roll Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult GetStudents(int id)
        {
            var student = studentBusiness.ReadStudent(id);
            if (student == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Entry not found");
            else
                return Ok(student);
        }

        /// <summary>
        /// Add new student document
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddStudent([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                var res = studentBusiness.AddStudent(student);
                if (res == true)
                    return CreatedAtAction(nameof(AddStudent), new { id = student.Id }, student);
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, "Student with same Roll Number already exists");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Your Model body is not Valid");
            }
        }

        /// <summary>
        /// Update a student document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                var stu = studentBusiness.UpdateStudent(id, student);
                if (stu != null)
                    return Ok(student);
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Roll Number");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Your Model body is invalid");
            }
        }

        /// <summary>
        /// Delete all students
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteAll()
        {
            studentBusiness.DeleteAllStudents();
            return StatusCode((int)HttpStatusCode.OK, "Deleted all records Sucessfully");
        }

        /// <summary>
        /// Delete a specific student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteStu(int id)
        {
            var flag = studentBusiness.DeleteStudent(id);
            if (flag)
                return StatusCode((int)HttpStatusCode.OK, "Deleted Sucessfully");
            else
                return StatusCode((int)HttpStatusCode.NotFound, "Entry not found");
        }
    }
}
