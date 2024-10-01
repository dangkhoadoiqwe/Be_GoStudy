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
            // Cấu hình ánh xạ giữa BlogPost và BlogPostViewdetailModel
            mc.CreateMap<BlogPost, BlogPostViewdetailModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)) // Ánh xạ thông tin user của blog
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments)) // Ánh xạ bình luận
                .ForMember(dest => dest.BlogImgs, opt => opt.MapFrom(src => src.BlogImgs)) // Ánh xạ hình ảnh
                .ReverseMap();
             
                 mc.CreateMap<BlogPost, BlogPost_Upadte_Model>().ReverseMap();
            mc.CreateMap<BlogPost, BlogPost_View_Model>().ReverseMap();
            mc.CreateMap<BlogPost, BlogPost_View_Model_All>()
                .ForMember(dest => dest.UserViewModel, opt => opt.MapFrom(src => src.User)) // Ánh xạ thông tin user
                .ReverseMap();

            mc.CreateMap<BlogPost, BlogPost_Create_Model1>().ReverseMap();
            mc.CreateMap<BlogPost, BlogPost_Create_Model>().ReverseMap();
            mc.CreateMap<BlogPost, BlogPost_Create_Model2>().ReverseMap();
            mc.CreateMap<Comment, CommentModel>().ReverseMap();
            mc.CreateMap<User, UserViewBlogModel>().ReverseMap();
            mc.CreateMap<BlogImg, BlogImgViewModel>().ReverseMap();

            // Ánh xạ Comment sang CommentViewModel kèm theo User
            mc.CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)) // Ánh xạ User trong Comment
                .ReverseMap();
        }
    }

}

