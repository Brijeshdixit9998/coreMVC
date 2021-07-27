using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coreMVC.Models
{
    public class Departments
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Required")]
        public string Department { get; set; }
    }
}
