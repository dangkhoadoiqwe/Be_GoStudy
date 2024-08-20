using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class PrivacySetting
    {
        [Key]
        public int PrivacySettingId { get; set; }

        [Required]
        public string Visibility { get; set; }
    }
}
