using MongoDB.Driver;
using SchoolSystem.Business.Business.Contract;
using SchoolSystem.Repository.Repository.Contract;
using StudentSystem.Abstractions.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolSystem.Business.Business.Concrete
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IMongoRepository<Student> _studentRepository;

        /// <summary>
        /// Initialises studentRepo using DI 
        /// </summary>
        /// <param name="studentRepository"></param>
        public StudentBusiness(IMongoRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Gets the list of all students in the collection
        /// </summary>
        /// <returns>List of Students</returns>
        public List<Student> ReadAllStudents()
        {
            var list = _studentRepository.GetAllDocuments();
            return list;
        }

        /// <summary>
        /// Gets a single student document based on Roll Number
        /// </summary>
        /// <param name="rollNum"></param>
        /// <returns>Student Document</returns>
        public Student ReadStudent(int rollNum)
        {
            var student = _studentRepository.FilterBy(x => x.RollNumber == rollNum).FirstOrDefault();
            //if (student == null)
            //    throw new DomainNotFoundException("Student not found in the db.");
            return student;
        }

        /// <summary>
        /// Checks if student with same roll num exists or not and then insert it into the collection
        /// </summary>
        /// <param name="student"></param>
        /// <returns>Bool</returns>
        public bool AddStudent(Student student)
        {
            var stu = _studentRepository.FilterBy(x => x.RollNumber == student.RollNumber).FirstOrDefault();
            if (stu == null)
            {
                _studentRepository.InsertDocument(student);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Deletes a single student document based on roll number
        /// </summary>
        /// <param name="rollNum"></param>
        /// <returns>bool</returns>
        public bool DeleteStudent(int rollNum)
        {
            var student = _studentRepository.FilterBy(x => x.RollNumber == rollNum).FirstOrDefault();
            var stu = _studentRepository.DeleteDocument(student);
            if (stu == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Deletes all documents in the collection
        /// </summary>
        public void DeleteAllStudents()
        {
            _studentRepository.DeleteAllDocuments();
        }

        /// <summary>
        /// Updates student document in collection only if it exists and no other document exists with same roll number as the updated student document's roll number
        /// </summary>
        /// <param name="rollNum"></param>
        /// <param name="student"></param>
        /// <returns>updated student document</returns>
        public Student UpdateStudent(int rollNum, Student student)
        {
            var x = _studentRepository.FilterBy(x => x.RollNumber == rollNum).FirstOrDefault();
            if (x != null)
            {
                var z = _studentRepository.FilterBy(x => x.RollNumber == student.RollNumber).FirstOrDefault();
                if (z == null || student.RollNumber == rollNum)
                {
                    student.Id = x.Id;
                    var y = _studentRepository.UpdateDocument(student);
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
