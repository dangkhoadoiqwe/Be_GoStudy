using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.AutoMapperModule
{
    
    public static class SpecializationModule
    {
        public static void ConfigSpecializationModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Specialization, Specialization_View_Model>().ReverseMap();
        }
    }
}
