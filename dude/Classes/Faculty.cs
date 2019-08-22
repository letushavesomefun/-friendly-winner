using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dude
{
    class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Specialty> Specialties { get; set; }

    }
}
