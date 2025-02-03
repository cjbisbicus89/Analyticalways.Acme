using Analyticalways.Acme.Domain.Entity;
using Analyticalways.Acme.Tranversal.Common;

namespace Analyticalways.Acme.Domain.Interface
{
    public interface ISchoolDomain
    {
       
        #region asynchronous methods
        Task<Response<dynamic>> RegisterStudentAsync(Student student);
        Task<Response<dynamic>> CreateCourseAsync(Course course);
        Task<Response<dynamic>> EnrollStudentToCourseAsync(Student student, Course course);
        Task<Response<List<Course>>> GetCoursesWithinDateRangeAsync(DateTime startDate, DateTime endDate);


        #endregion
    }
}
