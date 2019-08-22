using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dude
{
    class StudentContext:DbContext
    {
        public StudentContext() : base("StudentDb")
        { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
    }
}
