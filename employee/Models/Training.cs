using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Training
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Training Name: ")]
        public string Name { get; set; }
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }

    }
}
