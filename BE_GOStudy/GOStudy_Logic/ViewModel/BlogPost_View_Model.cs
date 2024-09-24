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
    public class BlogPost_Create_Model1
    {
   
     //   public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string Image { get; set; }

    }
}
