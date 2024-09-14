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
    public static class PackageModeule
    {
        public static void ConfigPackageModeule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Package, PackageViewModel>().ReverseMap();

        }
    }
}
