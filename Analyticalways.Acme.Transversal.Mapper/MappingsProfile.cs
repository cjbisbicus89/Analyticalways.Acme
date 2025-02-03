using System;
using Analyticalways.Acme.Aplication.DTO;
using Analyticalways.Acme.Domain.Entity;
using AutoMapper;

namespace Analyticalways.Acme.Transversal.Mapper
{
    public class MappingsProfile:Profile
    {
        public MappingsProfile() 
        {
            CreateMap<Student,StudentDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
        }

    }
}
