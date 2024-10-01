using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel;

namespace GO_Study_Logic.AutoMapperModule
{
    public static class CommentModule
    {
        public static void ConfigCommentModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Comment, Comment_View_Model>().ReverseMap();
        }
    }
}

