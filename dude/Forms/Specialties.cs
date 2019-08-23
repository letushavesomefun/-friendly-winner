using dude.Forms;
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

namespace dude
{
    public partial class Specialties : Form
    {
        StudentContext db;


        private BindingList<Specialty> specialties { get; set; }

        public Specialties()
        {
            InitializeComponent();

            db = new StudentContext();

            //db.Specialties.Load();
            //db.Faculties.Load();  
            specialties = db.Specialties.Include(specialty => specialty.Faculty).ToBindingList();
            dataGridView1.DataSource = specialties;
            //dataGridView1.DataSource = db.Specialties.Local.ToBindingList();
            //dataGridView2.DataSource = db.Faculties.Local.ToBindingList();
        }
        // добавление
       

        private void Add_button_Click_1(object sender, EventArgs e)
        { 
        SpecialtyChange tmForm = new SpecialtyChange();
        List<Faculty> faculties = db.Faculties.ToList();
        
            tmForm.comboBox1.DataSource = faculties;
            tmForm.comboBox1.ValueMember = "Id";
            tmForm.comboBox1.DisplayMember = "Name";
            DialogResult result = tmForm.ShowDialog(this);
           

            if (result == DialogResult.Cancel)
                return;
            Specialty specialty = new Specialty();
            try
            {
                specialty.Name = tmForm.textBox1.Text;
                specialty.Faculty = (Faculty)tmForm.comboBox1.SelectedItem;
            }
            catch
            { MessageBox.Show("Поля заполнены некорректно"); }
            var results = new List<ValidationResult>();
            var context = new ValidationContext(specialty);
            if (!Validator.TryValidateObject(specialty, context, results, true))
            {

                MessageBox.Show("Поля заполнены некорректно");

            }
            else
            {
                db.Specialties.Add(specialty);
                db.SaveChanges();
                specialties.Add(specialty);
                MessageBox.Show("Новый объект добавлен");
            }
        }
    private void Delete_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Specialty sp = db.Specialties.Find(id);
                sp.Students.Clear();
                db.Specialties.Remove(sp);
                db.SaveChanges();
                specialties.Remove(sp);
                MessageBox.Show("Объект удален");
            }
        }

        private void Change_button_Click(object sender, EventArgs e)
        {
           
              
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;

                    Specialty sp = db.Specialties.Find(id);

                    SpecialtyChange sc = new SpecialtyChange();
                List<Faculty> faculties = db.Faculties.ToList();
                sc.comboBox1.DataSource = faculties;
                sc.comboBox1.ValueMember = "Id";
                sc.comboBox1.DisplayMember = "Name";
                sc.textBox1.Text = sp.Name;
                    sc.comboBox1.SelectedItem = sp.Faculty;

                    DialogResult result = sc.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return;
                try
                {
                    sp.Name = sc.textBox1.Text;
                    sp.Faculty = (Faculty)sc.comboBox1.SelectedItem;
                    db.Entry(sp).State = EntityState.Modified;
                    var results = new List<ValidationResult>();
                    var context = new ValidationContext(sc);
                    if (!Validator.TryValidateObject(sc, context, results, true))
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

      

        private void Faculties_Click_1(object sender, EventArgs e)
        {
            FormFaculty formFaculty = new FormFaculty();
            formFaculty.Show();
        }
    }
}
 