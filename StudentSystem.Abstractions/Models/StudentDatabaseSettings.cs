namespace StudentSystem.Abstractions.Models
{
    public class StudentDatabaseSettings : IStudentDatabaseSettings
        {
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IStudentDatabaseSettings
        {
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
}
