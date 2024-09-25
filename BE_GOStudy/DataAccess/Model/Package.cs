using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

       
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
        public ICollection<Feature> Feature { get; set; }
    }
}
