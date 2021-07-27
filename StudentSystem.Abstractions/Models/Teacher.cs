using SchoolSystem.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Abstractions.Models
{
    [BsonCollection("Teachers")]
    public class Teacher : Document
    {
        [Required, Range(1, int.MaxValue)]
        public int RegistrationNum { get; set; }
        [Required, RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }
        [Required, RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }
        [Required, RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Invalid Subject")]
        public string Subject { get; set; }
        [Required, Range(1, 150)]
        public int Age { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Salary { get; set; }
    }
}
