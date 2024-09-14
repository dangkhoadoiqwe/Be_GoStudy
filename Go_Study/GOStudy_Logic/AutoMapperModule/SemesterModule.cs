using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel;

namespace GO_Study_Logic.AutoMapperModule
{
    public static class SemesterModule
    {
        public static void ConfigSemesterModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Semester, Semester_View_Model>().ReverseMap();
        }
    }
}