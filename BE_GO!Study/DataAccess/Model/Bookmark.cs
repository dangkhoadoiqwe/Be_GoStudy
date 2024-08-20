using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Bookmark
    {
        public int BookmarkId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public BlogPost Post { get; set; }
        public User User { get; set; }

    }

}
