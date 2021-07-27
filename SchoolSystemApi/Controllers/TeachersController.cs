using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Business.Business.Contract;
using StudentSystem.Abstractions.Models;
using System.Net;

namespace SchoolSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherBusiness teacherBusiness;
        public TeachersController(ITeacherBusiness teacher)
        {
            teacherBusiness = teacher;
        }


        /// <summary>
        /// Get all teachers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            var teachers = teacherBusiness.ReadAllTeachers();
            if (teachers == null)
                return StatusCode((int)HttpStatusCode.NotFound, "The database is empty");
            else
                return Ok(teachers);
        }

        /// <summary>
        /// Get specific teacher by Roll Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Getteachers(int id)
        {
            var teacher = teacherBusiness.ReadTeacher(id);
            if (teacher == null)
                return StatusCode((int)HttpStatusCode.NotFound, "Entry not found");
            else
                return Ok(teacher);
        }


        /// <summary>
        /// Add new teacher document
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Addteacher([FromBody] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var res = teacherBusiness.AddTeacher(teacher);
                if (res == true)
                    return CreatedAtAction(nameof(Addteacher), new { id = teacher.Id }, teacher);
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, "teacher with same Registration Number already exists");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Your Model body is not Valid");
            }
        }


        /// <summary>
        /// Update a teacher document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Updateteacher(int id, [FromBody] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var x = teacherBusiness.UpdateTeacher(id, teacher);
                if (x != null)
                    return Ok(teacher);
                else
                    return StatusCode((int)HttpStatusCode.BadRequest, "Invalid Registration Number");
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Your Model body is invalid");
            }
        }

        /// <summary>
        /// Delete all teachers
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteAll()
        {
            teacherBusiness.DeleteAllTeachers();
            return StatusCode((int)HttpStatusCode.OK, "Deleted all records Sucessfully");
        }


        /// <summary>
        /// Delete a specific teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteTeacher(int id)
        {
            var x = teacherBusiness.DeleteTeacher(id);
            if (x)
                return StatusCode((int)HttpStatusCode.OK, "Deleted Sucessfully");
            else
                return StatusCode((int)HttpStatusCode.NotFound, "Entry not found");
        }
    }
}
