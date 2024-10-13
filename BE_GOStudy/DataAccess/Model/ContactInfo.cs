using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Model
{
    public class ContactInfo
    {
        [Key]
        public int ContactInfoId { get; set; }
        [Required]
        public String ContactName { get; set; }
        public String? StreetAddress { get; set; }
        public String? City { get; set; }
        public String? ContactPhone { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Content { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; } 
        public String? UploadedFilePath { get; set; }
    }
}
