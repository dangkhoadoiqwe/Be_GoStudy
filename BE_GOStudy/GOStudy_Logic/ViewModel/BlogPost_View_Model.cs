using DataAccess.Model;
using GO_Study_Logic.ViewModel.User;
using System;
using System.ComponentModel.DataAnnotations;

namespace GO_Study_Logic.ViewModel
{
    public class BlogPost_View_Model
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
    //    public DataAccess.Model.User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewCount { get; set; }
        public bool IsDraft { get; set; }
        public int shareCount { get; set; }
        public int likeCount { get; set; }
        public string image { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsTrending { get; set; }
    }
    public class Comment_View_Model
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class PaginatedResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public IEnumerable<T> Data { get; set; }
    }

    public class BlogPost_View_Model_All
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        //    public DataAccess.Model.User User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ViewCount { get; set; }
        public bool IsDraft { get; set; }
        public int shareCount { get; set; }
        public int likeCount { get; set; }
   
        public bool IsFavorite { get; set; }
        public bool IsTrending { get; set; }

        public UserViewBlogModel UserViewModel { get; set; }
        public ICollection<BlogImgViewModel> BlogImgs { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
    public class BlogPostViewdetailModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserViewModel User { get; set; }
        public ICollection<BlogImgViewModel> BlogImgs { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
    }
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserViewModel User { get; set; }  // Thông tin user của comment
    }
    public class UserViewBlogModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public string ProfileImage { get; set; }
    }
    public class BlogImgViewModel
    {
        public int BlogImgId { get; set; }
        public string Img { get; set; }
    }
    public class BlogPost_Create_Model
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public int ViewCount { get; set; }
        public bool IsDraft { get; set; }
        public int shareCount { get; set; }
        public int likeCount { get; set; }
        public string image { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsTrending { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class BlogPost_Create_Model2
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Images { get; set; } // Allow multiple images
    }
    public class CommentModel
    {
        
        public int PostId { get; set; }  

       
        public int UserId { get; set; }  
        public string Content { get; set; }  

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class BlogPost_Upadte_Model
    {

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int userId { get; set; }

        public List<string> Images { get; set; }


    }
    public class BlogPost_Create_Model1
    {
   
     //   public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string Image { get; set; }

    }
}
