using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }

        [Required]
        public string Name { get; set; }

        // Foreign key to the Package
        public int PackageId { get; set; }

        // Navigation property to link to the Package
        public Package Package { get; set; }
    }
}
