using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dude
{
    class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string FIO { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Adress { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 9)]
        public string Phone { get; set; }
        [Required]
        [Range(1990, 2020)]
        public int Year { get; set; }


        public int SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}
