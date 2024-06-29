using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models.ViewModels
{
    public class EditEmployeeVM
    {
        public Employee Employee { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> EmployeeTraining { get; set; }
    }
}
