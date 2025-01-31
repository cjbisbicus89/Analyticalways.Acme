using Analyticalways.Acme.Domain.Core;
using Analyticalways.Acme.Domain.Entity;
using Analyticalways.Acme.Infraestructure.Interface;
using Analyticalways.Acme.Tranversal.Common;
using Analyticalways.Acme.Tranversal.Interfaces;
using Moq;

namespace Analyticalways.Acme.Domain.Tests
{
    public class SchoolDomainTests
    {
        private readonly Mock<ISchoolRepository> _mockRepository;
        private readonly Mock<IPaymentGateway> _mockPaymentGateway;
        private readonly SchoolDomain _schoolDomain;

        public SchoolDomainTests()
        {
            // Mock dependencies
            _mockRepository = new Mock<ISchoolRepository>();
            _mockPaymentGateway = new Mock<IPaymentGateway>();

            // Instantiate the class we are testing
            _schoolDomain = new SchoolDomain(_mockRepository.Object, _mockPaymentGateway.Object);
        }

        #region RegisterStudentAsync Tests

        [Fact]
        public async Task RegisterStudentAsync_ShouldReturnError_WhenStudentIsUnderage()
        {
            // Arrange
            var student = new Student { Name = "cristian bisbicus", Age = 17 }; //Underage

            // Act
            var result = await _schoolDomain.RegisterStudentAsync(student);

            // Assert
            Assert.False(result.success);
            Assert.Equal(Messages.Msg001, result.message); // Student must be 18 years or older to register
        }

        [Fact]
        public async Task RegisterStudentAsync_ShouldReturnSuccess_WhenStudentIs18()
        {
            // Arrange
            var student = new Student { Name = "cristian urbano", Age = 18 }; // Student must be 18 years old or older to register

            // We simulate that the registration was successful in the repository
            _mockRepository.Setup(repo => repo.RegisterStudentAsync(It.IsAny<Student>())).ReturnsAsync(true);

            // Act
            var result = await _schoolDomain.RegisterStudentAsync(student);

            // Assert
            Assert.True(result.success);
            Assert.Equal(Messages.Msg002, result.message); // Success message
        }

        [Fact]
        public async Task RegisterStudentAsync_ShouldReturnSuccess_WhenStudentIsOver18()
        {
            // Arrange
            var student = new Student { Name = "jesus bisbicus", Age = 25 }; // Adult

            // We simulate that the registration was successful in the repository
            _mockRepository.Setup(repo => repo.RegisterStudentAsync(It.IsAny<Student>())).ReturnsAsync(true);

            // Act
            var result = await _schoolDomain.RegisterStudentAsync(student);

            // Assert
            Assert.True(result.success);
            Assert.Equal(Messages.Msg002, result.message); // Success message
        }

        #endregion

        #region RegisterCourseAsync Tests

        [Fact]
        public async Task RegisterCourseAsync_ShouldReturnError_WhenCourseNameIsEmpty()
        {
            // Arrange
            var course = new Course { Name = "", RegistrationFee = 100, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(10) };

            // Act
            var result = await _schoolDomain.RegisterCourseAsync(course);

            // Assert
            Assert.False(result.success);
            Assert.Equal(Messages.Msg004, result.message); // Error message if course name is empty
        }

        [Fact]
        public async Task RegisterCourseAsync_ShouldReturnError_WhenRegistrationFeeIsNegative()
        {
            // Arrange
            var course = new Course { Name = "Math 101", RegistrationFee = -100, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(10) };

            // Act
            var result = await _schoolDomain.RegisterCourseAsync(course);

            // Assert
            Assert.False(result.success);
            Assert.Equal(Messages.Msg005, result.message); // Error message if registration fee is negative
        }

        [Fact]
        public async Task RegisterCourseAsync_ShouldReturnError_WhenEndDateBeforeStartDate()
        {
            // Arrange
            var course = new Course { Name = "Math 101", RegistrationFee = 100, StartDate = DateTime.Now.AddDays(5), EndDate = DateTime.Now.AddDays(1) };

            // Act
            var result = await _schoolDomain.RegisterCourseAsync(course);

            // Assert
            Assert.False(result.success);
            Assert.Equal(Messages.Msg006, result.message); // Error message if end date is before start date
        }

        [Fact]
        public async Task RegisterCourseAsync_ShouldReturnSuccess_WhenCourseIsValid()
        {
            // Arrange
            var course = new Course { Name = "Math 101", RegistrationFee = 100, StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(10) };

            // We simulate that the course was successfully registered in the repository
            _mockRepository.Setup(repo => repo.RegisterCourseAsync(It.IsAny<Course>())).ReturnsAsync(true);

            // Act
            var result = await _schoolDomain.RegisterCourseAsync(course);

            // Assert
            Assert.True(result.success);
            Assert.Equal(Messages.Msg003, result.message); // Success message
        }

        #endregion

        #region EnrollStudentInCourseAsync Tests

        [Fact]
        public async Task EnrollStudentInCourseAsync_ShouldReturnError_WhenPaymentFails()
        {
            // Arrange
            var student = new Student { Name = "cristian bisbicus", Age = 25 };
            var course = new Course
            {
                Name = "Math 101",
                RegistrationFee = 100,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(10)
            };

            // Simulate that the payment fails
            _mockPaymentGateway.Setup(pg => pg.ProcessPayment(It.IsAny<decimal>()))
                .ReturnsAsync(new Response<dynamic> { success = false, message = Messages.Msg007 });

            // Act
            var result = await _schoolDomain.EnrollStudentInCourseAsync(student, course);

            // Assert
            Assert.False(result.success);  // Verify that the operation has failed
            Assert.Equal(Messages.Msg007, result.message); // Verify that the error message is as expected

        }

        #endregion

        #region GetCoursesWithinDateRangeAsync Tests

        [Fact]
        public async Task GetCoursesWithinDateRangeAsync_ShouldReturnError_WhenStartDateIsAfterEndDate()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(5);
            var endDate = DateTime.Now.AddDays(1);

            // Act
            var result = await _schoolDomain.GetCoursesWithinDateRangeAsync(startDate, endDate);

            // Assert
            Assert.False(result.success);
            Assert.Equal(Messages.Msg006, result.message); // Error message if start date is after end date
        }

        [Fact]
        public async Task GetCoursesWithinDateRangeAsync_ShouldReturnEmptyList_WhenNoCoursesInRange()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(5);
            var endDate = DateTime.Now.AddDays(10);

            // We pretend that there are no courses in the repository
            _mockRepository.Setup(repo => repo.GetAllCoursesAsync()).ReturnsAsync(new List<Course>());

            // Act
            var result = await _schoolDomain.GetCoursesWithinDateRangeAsync(startDate, endDate);

            // Assert
            Assert.False(result.success);
            Assert.Equal(Messages.Msg008, result.message); // Error message if there are no courses in the date range
        }

        [Fact]
        public async Task GetCoursesWithinDateRangeAsync_ShouldReturnCourses_WhenCoursesInRange()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(30);

            var courses = new List<Course>
            {
                new Course { Name = "Math 101", RegistrationFee = 100, StartDate = DateTime.Now.AddDays(5), EndDate = DateTime.Now.AddDays(10) },
                new Course { Name = "Science 101", RegistrationFee = 150, StartDate = DateTime.Now.AddDays(12), EndDate = DateTime.Now.AddDays(18) }
            };

            _mockRepository.Setup(repo => repo.GetAllCoursesAsync()).ReturnsAsync(courses);

            // Act
            var result = await _schoolDomain.GetCoursesWithinDateRangeAsync(startDate, endDate);

            // Assert
            Assert.True(result.success);
            Assert.Equal(2, result.result.Count); // You must return two courses within the date range
        }

        #endregion
    }
}
