using Analyticalways.Acme.Domain.Entity;
using Analyticalways.Acme.Domain.Interface;
using Analyticalways.Acme.Infraestructure.Interface;
using Analyticalways.Acme.Tranversal.Common;
using Analyticalways.Acme.Tranversal.Interfaces;


namespace Analyticalways.Acme.Domain.Core
{
    public class SchoolDomain:ISchoolDomain
    {
        private readonly ISchoolRepository _repository;
        private readonly IPaymentGateway _paymentGateway;

        #region Builder
        public SchoolDomain(ISchoolRepository repository, IPaymentGateway paymentGateway)
        {
            _repository = repository;
            _paymentGateway = paymentGateway;
        }
        #endregion

       
        #region asynchronous methods

        public async Task<Response<dynamic>> RegisterStudentAsync(Student student)
        {
            if (student.Age < 18)  
            {
                return new Response<dynamic>
                {
                    success = false,
                    error = true,
                    message = Messages.Msg001
                };
            }

            bool result = await _repository.RegisterStudentAsync(student);
                        
            return new Response<dynamic>
            {
                success = result, // If the result is true, success will be true
                error = !result,  // If the result is false, then there is an error
                message = result ? Messages.Msg002 : Messages.Msg003 // I changed the error message if the operation fails
            };

        }
        public async Task<Response<dynamic>> RegisterCourseAsync(Course course)
        {
            // Validations with error messages
            var validationResponse = ValidateCourseInputs(course);
            if (!validationResponse.success)
            {
                return validationResponse;
            }

            bool? result = await _repository.RegisterCourseAsync(course);

            // Handling result from the repository
            if (result == null || !result.Value)
            {
                return CreateResponse(false, Messages.Msg002);
            }

            // If all validations are correct, we return a success response.
            return CreateResponse(true, Messages.Msg003);

        }
        public async Task<Response<dynamic>> EnrollStudentInCourseAsync(Student student, Course course)
        {
            // We process the payment first
            var payment = await _paymentGateway.ProcessPayment(course.RegistrationFee);

            if (!payment.success)
            {
                // If the payment fails, we return the corresponding error
                return CreateResponse(false, Messages.Msg007); // Payment failure message
            }

            // If the payment is successful, we process the enrollment
            bool? result = await _repository.EnrollStudentInCourseAsync(student, course);

            if (result == null || !result.Value)
            {
                // If the enrollment fails, we return the error message
                return CreateResponse(false, Messages.Msg002);
            }

            // If everything is successful, we return the success message
            return CreateResponse(true, Messages.Msg003);

        }
        public async Task<Response<List<Course>>> GetCoursesWithinDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            // Validate the date range
            if (startDate >= endDate)
            {
                return new Response<List<Course>>
                {
                    success = false,
                    error = true,
                    message = Messages.Msg006
                };
            }

            // Get all courses from the repository
            var allCourses = await _repository.GetAllCoursesAsync();

            // Filter courses that are within the date range
            var filteredCourses = allCourses
                .Where(course => course.StartDate >= startDate && course.EndDate <= endDate)
                .ToList();

            // If there are no courses in the date range, return an empty response
            if (filteredCourses.Count == 0)
            {
                return new Response<List<Course>>
                {
                    success = false,
                    error = true,
                    message = Messages.Msg008
                };
            }

            // Return the courses that meet the date range
            return new Response<List<Course>>
            {
                success = true,
                error = false,
                result = filteredCourses,
                message = "OK"
            };
        }

        #endregion


        #region private methods
        private Response<dynamic> ValidateCourseInputs(Course course)
        {
            // Course name validation
            if (string.IsNullOrWhiteSpace(course.Name))
            {
                return CreateErrorResponse(Messages.Msg004);
            }
            // Enrollment fee validation
            if (course.RegistrationFee <= 0)
            {
                return CreateErrorResponse(Messages.Msg005);
            }
            // Date validation
            if (course.EndDate <= course.StartDate)
            {
                return CreateErrorResponse(Messages.Msg006);
            }
            // If all validations are correct, we return a success response.
            return new Response<dynamic> { success = true, error = false };
        }
        private Response<dynamic> CreateErrorResponse(string message)
        {
            return new Response<dynamic>
            {
                success = false,
                error=false,
                message = message
            };
        }
        private Response<dynamic> CreateResponse(bool success, string message)
        {
            return new Response<dynamic>
            {
                success = success,
                error = !success,
                message = message
            };
        }
        #endregion

    }
}
