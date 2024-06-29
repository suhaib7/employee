using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("English Name: ")]
        public string NameEN { get; set; }
        [Required]
        [DisplayName("Arabic Name: ")]
        public string NameAR { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
