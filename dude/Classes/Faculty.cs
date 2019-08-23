using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dude
{
    class Faculty
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Specialty> Specialties { get; set; }
        public Faculty()
        {
            Specialties = new List<Specialty>();
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
