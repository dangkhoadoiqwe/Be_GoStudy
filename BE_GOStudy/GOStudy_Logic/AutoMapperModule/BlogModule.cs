using System;
using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;

namespace GO_Study_Logic.AutoMapperModule
{
    public static class BlogModule
    {
        public static void ConfigBlogModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<BlogPost, BlogPost_View_Model>().ReverseMap();
            mc.CreateMap<BlogPost, BlogPost_Create_Model>().ReverseMap();
            mc.CreateMap<Comment, Comment_View_Model>().ReverseMap();
            mc.CreateMap<Bookmark, Bookmark_View_Model>().ReverseMap();
        }
    }
}

