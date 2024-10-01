using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class UserLike
    {
        [Key]
        public int UserLikeId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int BlogId { get; set; }
        [ForeignKey("BlogId")]
        public BlogPost BlogPost { get; set; }

       

    }
}
