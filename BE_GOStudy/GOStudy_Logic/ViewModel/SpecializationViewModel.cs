using GO_Study_Logic.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOStudy_Logic.ViewModel
{
    public class SpecializationViewModel
    {
        public int SpecializationId { get; set; } // Thêm thuộc tính này
        public string Name { get; set; }
    }
    public class SpecializationViewModelByUser
    {
        public int SpecializationId { get; set; } // Thêm thuộc tính này
        public string Name { get; set; }

        public UserViewModel user { get; set; }
    }
}
