using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{


    public class Specialization
    {
        [Key]
        public int SpecializationId { get; set; }

        [Required]
        public string Name { get; set; }
    }

}
