using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ContactInfo
    {
        [Key]
        public int ContactInfoId { get; set; }

        [Required]
        public string ContactType { get; set; }

        [Required]
        public string Detail { get; set; }
    }
}
