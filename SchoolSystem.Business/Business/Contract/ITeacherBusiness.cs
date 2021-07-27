using StudentSystem.Abstractions.Models;
using System.Collections.Generic;

namespace SchoolSystem.Business.Business.Contract
{
    public interface ITeacherBusiness
    {
        List<Teacher> ReadAllTeachers();
        Teacher ReadTeacher(int RegNum);
        bool AddTeacher(Teacher Teacher);
        Teacher UpdateTeacher(int RegNum, Teacher Teacher);
        bool DeleteTeacher(int RegNum);
        void DeleteAllTeachers();
    }
}
