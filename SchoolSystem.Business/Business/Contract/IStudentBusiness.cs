using StudentSystem.Abstractions.Models;
using System.Collections.Generic;

namespace SchoolSystem.Business.Business.Contract
{
    public interface IStudentBusiness
    {
        List<Student> ReadAllStudents();
        Student ReadStudent(int rollNum);
        bool AddStudent(Student student);
        Student UpdateStudent(int rollNum, Student student);
        bool DeleteStudent(int rollNum);
        void DeleteAllStudents();
    }
}
