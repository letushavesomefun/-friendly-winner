using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dude.Forms
{
    
    public partial class FormFaculty : Form
    {
        StudentContext db;
        private BindingList<Faculty> faculties { get; set; }
        public FormFaculty()
        {
            InitializeComponent();
            db = new StudentContext();
            faculties = db.Faculties.ToBindingList();
            dataGridView1.DataSource = faculties;
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            FacultyChange facultyChange = new FacultyChange();
       
            Faculty faculty = new Faculty();
            try
            {

                
                DialogResult result = facultyChange.ShowDialog(this);
                faculty.Name = facultyChange.textBox1.Text;
                if (result == DialogResult.Cancel)
                    return;


            }
            catch
            { MessageBox.Show("Поля заполнены некорректно"); }
            var results = new List<ValidationResult>();
            var context = new ValidationContext(faculty);
            if (!Validator.TryValidateObject(faculty, context, results, true))
            {

                MessageBox.Show("Поля заполнены некорректно");

            }
            else
            {
                db.Faculties.Add(faculty);
                db.SaveChanges();
                faculties.Add(faculty);
                MessageBox.Show("Новый объект добавлен");
            }
        }


       

        private void Change_button_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Faculty faculty = db.Faculties.Find(id);

                FacultyChange facultyChange = new FacultyChange();
                List<Faculty> faculties = db.Faculties.ToList();
                facultyChange.textBox1.Text = faculty.Name;

                DialogResult result = facultyChange.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                try
                {
                    faculty.Name = facultyChange.textBox1.Text;
                    db.Entry(faculty).State = EntityState.Modified;
                    var results = new List<ValidationResult>();
                    var context = new ValidationContext(faculty);
                    if (!Validator.TryValidateObject(faculty, context, results, true))
                    {

                        MessageBox.Show("Поля заполнены некорректно");

                    }
                    db.SaveChanges();
                    MessageBox.Show("Объект обновлен");
                }
                catch
                { MessageBox.Show("Поля заполнены некорректно"); }
            }

        }

        private void Delete_button_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Faculty faculty = db.Faculties.Find(id);
                db.Faculties.Remove(faculty);
                db.SaveChanges();
                faculties.Remove(faculty);
                MessageBox.Show("Объект удален");
            }
        }
    }
}
