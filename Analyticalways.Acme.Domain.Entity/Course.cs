namespace Analyticalways.Acme.Domain.Entity
{
    public class Course
    {
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Student> EnrolledStudents { get; private set; } = new List<Student>();

       
        public void RegisterStudent(Student student)
        {
           
            EnrolledStudents.Add(student);
        }
    }
}
