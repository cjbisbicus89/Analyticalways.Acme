using Analyticalways.Acme.Domain.Entity;
using Analyticalways.Acme.Domain.Interface;
using Analyticalways.Acme.Infraestructure.Interface;
using Analyticalways.Acme.Tranversal.Common;
using Analyticalways.Acme.Tranversal.Interfaces;

public class SchoolDomain : ISchoolDomain
{
    private readonly ISchoolRepository _schoolRepository;
    private readonly IPaymentGateway _paymentGateway;

    #region Builder
    public SchoolDomain(ISchoolRepository schoolRepository, IPaymentGateway paymentGateway)
    {
        _schoolRepository = schoolRepository;
        _paymentGateway = paymentGateway;
    }
    #endregion

    #region Asynchronous Methods

    public async Task<Response<dynamic>> RegisterStudentAsync(Student student)
    {
        if (student.Age < 18)
        {
            return new Response<dynamic>
            {
                Success = false,
                Error = true,
                Message = Messages.MessageAgeValidation
            };
        }

        bool isStudentRegistered = await _schoolRepository.RegisterStudentAsync(student);

        return new Response<dynamic>
        {
            Success = isStudentRegistered,
            Error = !isStudentRegistered,
            Message = isStudentRegistered ? Messages.MessageProcessNotCompleted : Messages.MessageProcessCompletedSuccessfully
        };
    }

    public async Task<Response<dynamic>> CreateCourseAsync(Course course)
    {
        var validationResponse = ValidateCourseRegistration(course);
        if (!validationResponse.Success)
        {
            return validationResponse;
        }

        bool? isCourseCreated = await _schoolRepository.CreateCourseAsync(course);

        if (isCourseCreated == null || !isCourseCreated.Value)
        {
            return CreateFailureResponse(Messages.MessageProcessNotCompleted);
        }

        return CreateSuccessResponse(Messages.MessageProcessCompletedSuccessfully);
    }

    public async Task<Response<dynamic>> EnrollStudentToCourseAsync(Student student, Course course)
    {
        var paymentResponse = await _paymentGateway.ProcessPayment(course.RegistrationFee);

        if (!paymentResponse.Success)
        {
            return CreateFailureResponse(Messages.MessageConnectionPaymentGatewayFailed);
        }

        bool? isStudentEnrolled = await _schoolRepository.EnrollStudentInCourseAsync(student, course);

        if (isStudentEnrolled == null || !isStudentEnrolled.Value)
        {
            return CreateFailureResponse(Messages.MessageProcessNotCompleted);
        }

        return CreateSuccessResponse(Messages.MessageProcessCompletedSuccessfully);
    }

    public async Task<Response<List<Course>>> GetCoursesWithinDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
        {
            return new Response<List<Course>>
            {
                Success = false,
                Error = true,
                Message = Messages.MessageValidationOfDates
            };
        }

        var allCourses = await _schoolRepository.GetAllCoursesAsync();
        var filteredCourses = allCourses
            .Where(course => course.StartDate >= startDate && course.EndDate <= endDate)
            .ToList();

        if (filteredCourses.Count == 0)
        {
            return new Response<List<Course>>
            {
                Success = false,
                Error = true,
                Message = Messages.MessageCoursesNotFoundInTheDateRange
            };
        }

        return new Response<List<Course>>
        {
            Success = true,
            Error = false,
            Result = filteredCourses,
            Message = Messages.MessageProcessCompletedSuccessfully
        };
    }

    #endregion

    #region Métodos Privados

    private Response<dynamic> ValidateCourseRegistration(Course course)
    {
        
        if (string.IsNullOrWhiteSpace(course.Name))
        {
            return CreateFailureResponse(Messages.MessageValidationNullOrEmptyField);
        }
       
        if (course.RegistrationFee <= 0)
        {
            return CreateFailureResponse(Messages.MessageRegistrationFee);
        }
      
        if (course.EndDate <= course.StartDate)
        {
            return CreateFailureResponse(Messages.MessageValidationOfDates);
        }

        return new Response<dynamic> { Success = true, Error = false };
    }

    private Response<dynamic> CreateFailureResponse(string message)
    {
        return new Response<dynamic>
        {
            Success = false,
            Error = true,
            Message = message
        };
    }

    private Response<dynamic> CreateSuccessResponse(string message)
    {
        return new Response<dynamic>
        {
            Success = true,
            Error = false,
            Message = message
        };
    }

    #endregion
}
