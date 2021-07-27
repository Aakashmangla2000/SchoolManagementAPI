using SchoolSystem.Business.Business.Contract;
using SchoolSystem.Repository.Repository.Contract;
using StudentSystem.Abstractions.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolSystem.Business.Business.Concrete
{
    public class TeacherBusiness : ITeacherBusiness
    {
        private readonly IMongoRepository<Teacher> _teacherRepository;

        /// <summary>
        /// Initialises TeacherRepo using DI 
        /// </summary>
        /// <param name="TeacherRepository"></param>
        public TeacherBusiness(IMongoRepository<Teacher> TeacherRepository)
        {
            _teacherRepository = TeacherRepository;
        }

        /// <summary>
        /// Gets the list of all Teachers in the collection
        /// </summary>
        /// <returns>List of Teachers</returns>
        public List<Teacher> ReadAllTeachers()
        {
            var list = _teacherRepository.GetAllDocuments();
            return list;
        }

        /// <summary>
        /// Gets a single Teacher document based on registration Number
        /// </summary>
        /// <param name="regNum"></param>
        /// <returns>Teacher Document</returns>
        public Teacher ReadTeacher(int regNum)
        {
            var Teacher = _teacherRepository.FilterBy(x => x.RegistrationNum == regNum).FirstOrDefault();
            return Teacher;
        }

        /// <summary>
        /// Checks if Teacher with same registration num exists or not and then insert it into the collection
        /// </summary>
        /// <param name="Teacher"></param>
        /// <returns>Bool</returns>
        public bool AddTeacher(Teacher Teacher)
        {
            var stu = _teacherRepository.FilterBy(x => x.RegistrationNum == Teacher.RegistrationNum).FirstOrDefault();
            if (stu == null)
            {
                _teacherRepository.InsertDocument(Teacher);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Deletes a single Teacher document based on registration number
        /// </summary>
        /// <param name="regNum"></param>
        /// <returns>bool</returns>
        public bool DeleteTeacher(int regNum)
        {
            var Teacher = _teacherRepository.FilterBy(x => x.RegistrationNum == regNum).FirstOrDefault();
            var stu = _teacherRepository.DeleteDocument(Teacher);
            if (stu == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Deletes all documents in the collection
        /// </summary>
        public void DeleteAllTeachers()
        {
            _teacherRepository.DeleteAllDocuments();
        }

        /// <summary>
        /// Updates Teacher document in collection only if it exists and no other document exists with same registration number as the updated Teacher document's roll number
        /// </summary>
        /// <param name="regNum"></param>
        /// <param name="Teacher"></param>
        /// <returns>updated Teacher document</returns>
        public Teacher UpdateTeacher(int regNum, Teacher Teacher)
        {
            var x = _teacherRepository.FilterBy(x => x.RegistrationNum == regNum).FirstOrDefault();
            if (x != null)
            {
                var z = _teacherRepository.FilterBy(x => x.RegistrationNum == Teacher.RegistrationNum).FirstOrDefault();
                if (z == null || Teacher.RegistrationNum == regNum)
                {
                    Teacher.Id = x.Id;
                    var y = _teacherRepository.UpdateDocument(Teacher);
                    return y;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
