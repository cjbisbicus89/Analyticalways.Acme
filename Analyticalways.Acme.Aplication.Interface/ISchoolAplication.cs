using Analyticalways.Acme.Aplication.DTO;
using Analyticalways.Acme.Tranversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyticalways.Acme.Aplication.Interface
{
    public interface ISchoolAplication
    {
        #region asynchronous methods
        Task<Response<dynamic>> RegisterStudentAsync(StudentDto student);
        Task<Response<dynamic>> CreateCourseAsync(CourseDto course);
        Task<Response<dynamic>> EnrollStudentToCourseAsync(StudentDto student, CourseDto course);
        Task<Response<List<CourseDto>>> GetCoursesWithinDateRangeAsync(DateTime startDate, DateTime endDate);

        #endregion
    }
}
