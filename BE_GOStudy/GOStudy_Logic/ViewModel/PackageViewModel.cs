using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
      //  public List<FeatuerViewModel> Features { get; set; }
    }

    public class PackageViewModel1
    {
        public int PackageId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<FeatuerViewModel> Features { get; set; }
    }
    public class FeatuerViewModel
    {
       
        public string Name { get; set; }
    }
      

}
