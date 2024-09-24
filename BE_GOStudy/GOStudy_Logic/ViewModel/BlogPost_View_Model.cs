using System;

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
    public class BlogPost_Create_Model1
    {
   
     //   public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string Image { get; set; }

    }
}
