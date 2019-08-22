using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dude
{
    class Student
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string adress { get; set; }
        public int Phone { get; set; }
        public int Year { get; set; }


        public int? SpecialtyId { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}
