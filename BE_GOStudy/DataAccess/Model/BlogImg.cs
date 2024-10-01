using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class BlogImg
    {
        [Key]
        public int BlogImgId { get; set; } // Primary key for BlogImg

        public int BlogId { get; set; }  // Foreign key linking to BlogPost
        [ForeignKey("BlogId")]
        public BlogPost BlogPost { get; set; }

        [Required]
        public string Img { get; set; }  // URL or path to the image
    }
}
