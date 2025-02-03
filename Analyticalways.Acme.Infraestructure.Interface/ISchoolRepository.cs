using Analyticalways.Acme.Domain.Entity;

namespace Analyticalways.Acme.Infraestructure.Interface
{
    public interface ISchoolRepository
    {       
        #region asynchronous methods
        Task<bool> RegisterStudentAsync(Student student);
        Task<bool> CreateCourseAsync(Course course);
        Task<bool> EnrollStudentInCourseAsync(Student student, Course course);
        Task<List<Course>> GetAllCoursesAsync();
        #endregion

    }
}
