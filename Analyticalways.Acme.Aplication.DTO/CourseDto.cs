using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyticalways.Acme.Aplication.DTO
{
    public class CourseDto
    {
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<StudentDto> EnrolledStudents { get; private set; } = new List<StudentDto>();

    }
}
