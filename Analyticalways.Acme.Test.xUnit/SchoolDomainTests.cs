using Analyticalways.Acme.Aplication.DTO;
using Analyticalways.Acme.Aplication.Implementation;
using Analyticalways.Acme.Domain.Entity;
using Analyticalways.Acme.Domain.Interface;
using Analyticalways.Acme.Tranversal.Common;
using AutoMapper;
using Moq;

namespace Analyticalways.Acme.Tests
{
    public class SchoolAplicationTests
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ISchoolDomain> _mockSchoolDomain;
        private readonly SchoolAplication _schoolAplication;

        public SchoolAplicationTests()
        {
            
            _mockMapper = new Mock<IMapper>();
            _mockSchoolDomain = new Mock<ISchoolDomain>();
            
            _schoolAplication = new SchoolAplication(_mockMapper.Object, _mockSchoolDomain.Object);
        }

        #region RegisterStudentAsync Tests
        [Fact]
        public async Task RegisterStudentAsync_ShouldReturnSuccess_WhenStudentIsRegistered()
        {
            // Arrange
            var studentDto = new StudentDto { Name = "Cristian Bisbicus", Age = 20 };
            var studentEntity = new Student { Name = "Cristian Bisbicus", Age = 20 };
            var registerResult = new Response<dynamic>
            {
                Success = true,
                Result = studentEntity
            };

            // Mocking
            _mockMapper.Setup(m => m.Map<Student>(It.IsAny<StudentDto>())).Returns(studentEntity);
            _mockSchoolDomain.Setup(s => s.RegisterStudentAsync(It.IsAny<Student>())).ReturnsAsync(registerResult);

            // Act
            var response = await _schoolAplication.RegisterStudentAsync(studentDto);

            // Assert
            Assert.True(response.Success);
            Assert.Null(response.Message);
            Assert.Equal(studentEntity, response.Result);
        }

        [Fact]
        public async Task RegisterStudentAsync_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            var studentDto = new StudentDto { Name = "Cristian Bisbicus", Age = 20 };

            // Mock exception
            _mockSchoolDomain.Setup(s => s.RegisterStudentAsync(It.IsAny<Student>())).ThrowsAsync(new Exception("Test exception"));

            // Act
            var response = await _schoolAplication.RegisterStudentAsync(studentDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Test exception", response.Message);
        }
        [Fact]
        public async Task RegisterStudentAsync_ShouldReturnFailure_WhenInvalidDataProvided()
        {
            // Arrange
            var studentDto = new StudentDto { Name = "", Age = 20 }; 
            var studentEntity = new Student { Name = "", Age = 20 };  

            // Mocking
            _mockMapper.Setup(m => m.Map<Student>(It.IsAny<StudentDto>())).Returns(studentEntity);
            _mockSchoolDomain.Setup(s => s.RegisterStudentAsync(It.IsAny<Student>())).ReturnsAsync(new Response<dynamic> { Success = false, Message = "Invalid data" });

            // Act
            var response = await _schoolAplication.RegisterStudentAsync(studentDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Invalid data", response.Message);
        }
        #endregion

        #region CreateCourseAsync Tests
        [Fact]
        public async Task CreateCourseAsync_ShouldReturnSuccess_WhenCourseIsCreated()
        {
            // Arrange
            var courseDto = new CourseDto { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var courseEntity = new Course { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var registerResult = new Response<dynamic>
            {
                Success = true,
                Result = courseEntity
            };

            // Mocking
            _mockMapper.Setup(m => m.Map<Course>(It.IsAny<CourseDto>())).Returns(courseEntity);
            _mockSchoolDomain.Setup(s => s.CreateCourseAsync(It.IsAny<Course>())).ReturnsAsync(registerResult);

            // Act
            var response = await _schoolAplication.CreateCourseAsync(courseDto);

            // Assert
            Assert.True(response.Success);
            Assert.Null(response.Message);
            Assert.Equal(courseEntity, response.Result);
        }

        [Fact]
        public async Task CreateCourseAsync_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            var courseDto = new CourseDto { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };

            // Mock exception
            _mockSchoolDomain.Setup(s => s.CreateCourseAsync(It.IsAny<Course>())).ThrowsAsync(new Exception("Test exception"));

            // Act
            var response = await _schoolAplication.CreateCourseAsync(courseDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Test exception", response.Message);
        }
        [Fact]
        public async Task CreateCourseAsync_ShouldReturnFailure_WhenCourseAlreadyExists()
        {
            // Arrange
            var courseDto = new CourseDto { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var courseEntity = new Course { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
                 
            _mockMapper.Setup(m => m.Map<Course>(It.IsAny<CourseDto>())).Returns(courseEntity);
            _mockSchoolDomain.Setup(s => s.CreateCourseAsync(It.IsAny<Course>())).ReturnsAsync(new Response<dynamic> { Success = false, Message = "Course already exists" });

            // Act
            var response = await _schoolAplication.CreateCourseAsync(courseDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Course already exists", response.Message);
        }
        #endregion

        #region EnrollStudentToCourseAsync Tests
        [Fact]
        public async Task EnrollStudentToCourseAsync_ShouldReturnSuccess_WhenEnrollmentIsSuccessful()
        {
            // Arrange
            var studentDto = new StudentDto { Name = "Cristian Bisbicus", Age = 20 };
            var courseDto = new CourseDto { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var studentEntity = new Student { Name = "Cristian Bisbicus", Age = 20 };
            var courseEntity = new Course { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var registerResult = new Response<dynamic>
            {
                Success = true,
                Result = Messages.MessageProcessCompletedSuccessfully
            };

            // Mocking
            _mockMapper.Setup(m => m.Map<Student>(It.IsAny<StudentDto>())).Returns(studentEntity);
            _mockMapper.Setup(m => m.Map<Course>(It.IsAny<CourseDto>())).Returns(courseEntity);
            _mockSchoolDomain.Setup(s => s.EnrollStudentToCourseAsync(It.IsAny<Student>(), It.IsAny<Course>())).ReturnsAsync(registerResult);

            // Act
            var response = await _schoolAplication.EnrollStudentToCourseAsync(studentDto, courseDto);

            // Assert
            Assert.True(response.Success);
            Assert.Null(response.Message);
            Assert.Equal(Messages.MessageProcessCompletedSuccessfully, response.Result);
        }

        [Fact]
        public async Task EnrollStudentToCourseAsync_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            var studentDto = new StudentDto { Name = "Cristian Bisbicus", Age = 20 };
            var courseDto = new CourseDto { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };

            // Mock exception
            _mockSchoolDomain.Setup(s => s.EnrollStudentToCourseAsync(It.IsAny<Student>(), It.IsAny<Course>())).ThrowsAsync(new Exception("Test exception"));

            // Act
            var response = await _schoolAplication.EnrollStudentToCourseAsync(studentDto, courseDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Test exception", response.Message);
        }
        [Fact]
        public async Task EnrollStudentToCourseAsync_ShouldReturnFailure_WhenStudentAlreadyEnrolled()
        {
            // Arrange
            var studentDto = new StudentDto { Name = "Cristian Bisbicus", Age = 20 };
            var courseDto = new CourseDto { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            var studentEntity = new Student { Name = "Cristian Bisbicus", Age = 20 };
            var courseEntity = new Course { Name = "Programming cycle 1", RegistrationFee = 200m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };

            // Simulate domain returning failure if the student is already enrolled
            _mockMapper.Setup(m => m.Map<Student>(It.IsAny<StudentDto>())).Returns(studentEntity);
            _mockMapper.Setup(m => m.Map<Course>(It.IsAny<CourseDto>())).Returns(courseEntity);
            _mockSchoolDomain.Setup(s => s.EnrollStudentToCourseAsync(It.IsAny<Student>(), It.IsAny<Course>())).ReturnsAsync(new Response<dynamic> { Success = false, Message = Messages.MessageProcessCompletedSuccessfully });

            // Act
            var response = await _schoolAplication.EnrollStudentToCourseAsync(studentDto, courseDto);

            // Assert
            Assert.False(response.Success);
            Assert.Equal(Messages.MessageProcessCompletedSuccessfully, response.Message);
        }
        #endregion

        #region GetCoursesWithinDateRangeAsync Tests
        [Fact]      
        public async Task GetCoursesWithinDateRangeAsync_ShouldReturnCourses_WhenSuccess()
        {
            // Arrange
                var startDate = DateTime.Now.AddDays(-10);
                var endDate = DateTime.Now.AddDays(10);
                
                var courses = new List<Course>
                {
                    new Course
                    {
                            Name = "Programming cycle 1",
                            RegistrationFee = 200,
                            StartDate = startDate,
                            EndDate = endDate
                    }
                };

               
                var registerResult = new Response<List<Course>>
                {
                    Success = true,
                    Result = courses
                };
                
                var courseDtos = new List<CourseDto>
                {
                    new CourseDto
                    {
                        Name = "Programming cycle 1",
                        RegistrationFee = 200,
                        StartDate = startDate,
                        EndDate = endDate
                    }
                };

                _mockSchoolDomain.Setup(s => s.GetCoursesWithinDateRangeAsync(startDate, endDate))
                                    .ReturnsAsync(registerResult);

                _mockMapper.Setup(m => m.Map<List<CourseDto>>(It.IsAny<List<Course>>()))
                            .Returns(courseDtos);

                // Act
                var response = await _schoolAplication.GetCoursesWithinDateRangeAsync(startDate, endDate);

                // Assert
                Assert.True(response.Success); 
                Assert.NotNull(response.Result); 
                Assert.Equal(courseDtos.Count, response.Result.Count); 
                Assert.Equal(courseDtos[0].Name, response.Result[0].Name); 
                Assert.Equal(courseDtos[0].RegistrationFee, response.Result[0].RegistrationFee); 
                Assert.Equal(courseDtos[0].StartDate, response.Result[0].StartDate); 
                Assert.Equal(courseDtos[0].EndDate, response.Result[0].EndDate); 
        }


        [Fact]
        public async Task GetCoursesWithinDateRangeAsync_ShouldReturnFailure_WhenErrorOccurs()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now.AddDays(10);

            // Mock exception
            _mockSchoolDomain.Setup(s => s.GetCoursesWithinDateRangeAsync(startDate, endDate)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var response = await _schoolAplication.GetCoursesWithinDateRangeAsync(startDate, endDate);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Error: Test exception", response.Message);
        }
        
        #endregion
    }
}
