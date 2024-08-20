using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Semester
    {
        [Key]
        public int SemesterId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
