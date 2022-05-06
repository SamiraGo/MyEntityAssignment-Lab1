using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyEntityAssignment.Models
{
   public class Employee
    {
        
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Leave> Leaves { get; set; }
        
    }
}
