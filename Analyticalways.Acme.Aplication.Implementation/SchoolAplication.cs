using Analyticalways.Acme.Aplication.DTO;
using Analyticalways.Acme.Aplication.Interface;
using Analyticalways.Acme.Domain.Entity;
using Analyticalways.Acme.Domain.Interface;
using Analyticalways.Acme.Tranversal.Common;
using AutoMapper;

namespace Analyticalways.Acme.Aplication.Implementation
{
    public class SchoolAplication:ISchoolAplication
    {

        private readonly IMapper _mapper;
        private readonly ISchoolDomain _schoolDomain;

        public SchoolAplication(IMapper mapper, ISchoolDomain schoolDomain)
        {
            _mapper = mapper;
            _schoolDomain = schoolDomain;
        }

        #region asynchronous methods
        public async Task<Response<dynamic>> RegisterStudentAsync(StudentDto student)
        {
            var response = new Response<dynamic>();
            try
            {  
                var studentEntity = _mapper.Map<Student>(student);

                var registerResult = await _schoolDomain.RegisterStudentAsync(studentEntity);

                response.Result = registerResult.Result;
                response.Success = registerResult.Success;
                response.Message = registerResult.Success ? null : registerResult.Message;
            }
            catch (Exception ex) { 
                response.Message = $"{ex.Message}";
                response.Success = false;
            }
            return response;
        }


        public async Task<Response<dynamic>> CreateCourseAsync(CourseDto course)
        {
            var response = new Response<dynamic>();
            try
            {
                var courseEntity = _mapper.Map<Course>(course);

                var registerResult = await _schoolDomain.CreateCourseAsync(courseEntity);

                response.Result = registerResult.Result;
                response.Success = registerResult.Success;
                response.Message = registerResult.Success ? null : registerResult.Message;
            }
            catch (Exception ex)
            {
                response.Message = $"{ex.Message}";
                response.Success = false;
            }
            return response;
        }
        public async Task<Response<dynamic>> EnrollStudentToCourseAsync(StudentDto student, CourseDto course)
        {
            var response = new Response<dynamic>();
            try
            {
                var studentEntity = _mapper.Map<Student>(student);
                var courseEntity = _mapper.Map<Course>(course);

                var registerResult = await _schoolDomain.EnrollStudentToCourseAsync(studentEntity, courseEntity);

                response.Result = registerResult.Result;
                response.Success = registerResult.Success;
                response.Message = registerResult.Success ? null : registerResult.Message;
            }
            catch (Exception ex)
            {
                response.Message = $"{ex.Message}";
                response.Success = false;
            }
            return response;
        }

        public async Task<Response<List<CourseDto>>> GetCoursesWithinDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var response = new Response<List<CourseDto>>();
            try
            {
                var registerResult = await _schoolDomain.GetCoursesWithinDateRangeAsync(startDate, endDate);

                if (registerResult.Success)
                {
                    response.Result = _mapper.Map<List<CourseDto>>(registerResult.Result);
                    response.Success = true;
                }
                else
                {
                    response.Message = registerResult.Message;
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error: {ex.Message}";
                response.Success = false;
            }
            return response;
        }


        #endregion
    }
}
