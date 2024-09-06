using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel.User;
using GO_Study_Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.AutoMapperModule
{
    public static class TasksModule
    {
        public static void ConfigTasksModule(this IMapperConfigurationExpression mc)
        {
           mc.CreateMap<TaskViewMeeting,Tasks>().ReverseMap();
            mc.CreateMap<TaskViewModel, Tasks>().ReverseMap();

        }
    }
}
