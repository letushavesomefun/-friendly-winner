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
    public partial class StudentForm : Form
    {
       
        readonly StudentContext db;
        private BindingList<Student> Students { get; set; }
        public StudentForm()
        {
            InitializeComponent();
            db = new StudentContext();
            //db.Students.Load();
            // dataGridView1.DataSource = db.Students.Include(student => student.Specialty).ToList();
            Students = db.Students.Include(student => student.Specialty).ToBindingList();
            dataGridView1.DataSource = Students;
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            StudentChange studentForm = new StudentChange();
            List<Specialty> specialties = db.Specialties.ToList();
           
            studentForm.comboBox1.DataSource = specialties;
            studentForm.comboBox1.ValueMember = "Id";
            studentForm.comboBox1.DisplayMember = "Name";
            DialogResult result = studentForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
           
                Student student = new Student();
            try
            {
                student.FIO = studentForm.textBox1.Text.ToString();
                student.Adress = studentForm.textBox2.Text;
                student.Phone = studentForm.textBox3.Text;
                student.Year = Convert.ToInt32(studentForm.textBox4.Text);
                student.Specialty = (Specialty)studentForm.comboBox1.SelectedItem;
            }
            catch
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            var results = new List<ValidationResult>();
            var context = new ValidationContext(student);
            if (!Validator.TryValidateObject(student, context, results, true))
            {
                
                    MessageBox.Show("Поля заполнены некорректно");
                
            }
            else
            {
                db.Students.Add(student);
                db.SaveChanges();
                Students.Add(student);
                MessageBox.Show("Новый студент добавлен");
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

                Student student = db.Students.Find(id);

                StudentChange studentChange = new StudentChange();

                studentChange.textBox1.Text = student.FIO;
                studentChange.textBox2.Text = student.Adress;
                studentChange.textBox3.Text = student.Phone.ToString();
                studentChange.textBox4.Text = student.Year.ToString();
                studentChange.comboBox1.SelectedItem = student.Specialty;
               

                List<Specialty> specialties = db.Specialties.ToList();
                studentChange.comboBox1.DataSource = specialties;
                studentChange.comboBox1.ValueMember = "Id";
                studentChange.comboBox1.DisplayMember = "Name";

                if (student.Specialty != null)
                    studentChange.comboBox1.SelectedValue = student.Specialty.Id;

                DialogResult result = studentChange.ShowDialog(this);

                if (result == DialogResult.Cancel)
                    return;
                try
                {
                    student.FIO = studentChange.textBox1.Text.ToString();
                    student.Adress = studentChange.textBox2.Text;
                    student.Phone = studentChange.textBox3.Text;
                    student.Year = Convert.ToInt32(studentChange.textBox4.Text);
                    student.Specialty = (Specialty)studentChange.comboBox1.SelectedItem;
                }
                catch
                { MessageBox.Show("Поля заполнены некорректно"); }
                var results = new List<ValidationResult>();
                var context = new ValidationContext(student);
                if (!Validator.TryValidateObject(student, context, results, true))
                {

                    MessageBox.Show("Поля заполнены некорректно");

                }
                else
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Объект обновлен");
                }
               

               
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

                Student student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
                Students.Remove(student);


                MessageBox.Show("Объект удален");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Specialties specialties = new Specialties();
            specialties.Show();
        }
    }
}
