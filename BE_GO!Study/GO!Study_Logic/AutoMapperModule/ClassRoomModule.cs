using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.AutoMapperModule
{
    
        public static class ClassRoomModule
           {
            public static void ConfigClassRoomModule(this IMapperConfigurationExpression mc)
            {
                mc.CreateMap<Classroom, ClassroomModel>().ReverseMap();
                mc.CreateMap<Classroom, AllClassModel>().ReverseMap();
                mc.CreateMap<Classroom, ClassUserModel>().ReverseMap();
        }
        }
     
}
