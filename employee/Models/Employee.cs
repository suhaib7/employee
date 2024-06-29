using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Employee
    {
        public Employee()
        {
            EmployeeTrainings = new HashSet<EmployeeTraining>();
        }
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public ICollection<EmployeeTraining> EmployeeTrainings { get; set; }
    }
}
