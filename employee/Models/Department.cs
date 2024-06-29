using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("English Name: ")]
        public string NameEN { get; set; }
        [Required]
        [DisplayName("Arabic Name: ")]
        public string NameAR { get; set; }

    }
}
