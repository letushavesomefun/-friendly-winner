using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dude
{
    class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public Specialty()
        {
            Students = new List<Student>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
