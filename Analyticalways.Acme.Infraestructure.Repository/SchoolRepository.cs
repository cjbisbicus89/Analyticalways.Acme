using Analyticalways.Acme.Domain.Entity;
using Analyticalways.Acme.Infraestructure.Interface;
using Analyticalways.Acme.Tranversal.Common;

namespace Analyticalways.Acme.Infraestructure.Repository
{
    public class SchoolRepository:ISchoolRepository
    {

        private readonly IConnectionFactory _connectionFactory;

        #region Builder
        public SchoolRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #endregion

        #region Asynchronous Methods

        public async Task<bool> RegisterStudentAsync(Student student)
        {
            using(var connection = _connectionFactory.GetConnection)
            {
                return true;
            }
        }
        public async Task<bool> CreateCourseAsync(Course course)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                return true;
            }
        }
        public async Task<bool> EnrollStudentInCourseAsync(Student student, Course course)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                return true;
            }
        }
        public async Task<List<Course>> GetAllCoursesAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                return new List<Course>();
            }
        }

        #endregion

    }
}
