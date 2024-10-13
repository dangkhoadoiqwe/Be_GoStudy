using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class BlogPost
    {
        [Key]
        public int PostId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        //New
        public string Category { get; set; }
        public string Tags { get; set; }
        public int ViewCount { get; set; }
        public bool IsDraft { get; set; }
        public int shareCount { get; set; }
        public int likeCount { get; set; }
        public String image { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsTrending { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public ICollection<BlogImg> BlogImgs { get; set; }

        public ICollection<UserLike> UserLikes { get; set; }
    }
}
