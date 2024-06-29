using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class SectionDTO
    {
        public int Id { get; set; }
        public string NameEN { get; set; }
        public string NameAR { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentNameEN { get; set; }
        public string DepartmentNameAR { get; set; }
    }
}
