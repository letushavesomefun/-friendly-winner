using dude.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        StudentContext db;
        public StudentForm()
        {
            InitializeComponent();
            db = new StudentContext();
            db.Students.Load();
            dataGridView1.DataSource = db.Students.Local.ToBindingList();
        }

        private void Button1_Click(object sender, EventArgs e)
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
            student.FIO = studentForm.textBox1.Text.ToString();
            student.adress = studentForm.textBox2.Text;
            student.Phone =  Convert.ToInt32(studentForm.textBox3.Text);
            student.Year = Convert.ToInt32(studentForm.textBox4.Text);
            student.Specialty = (Specialty)studentForm.comboBox1.SelectedItem;
            db.Students.Add(student);
            db.SaveChanges();
            MessageBox.Show("Новый студент добавлен");
        }

        private void Button2_Click(object sender, EventArgs e)
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
                studentChange.textBox2.Text = student.adress;
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

                student.FIO = studentChange.textBox1.Text.ToString();
                student.adress = studentChange.textBox2.Text;
                student.Phone = Convert.ToInt32(studentChange.textBox3.Text);
                student.Year = Convert.ToInt32(studentChange.textBox4.Text);
                student.Specialty = (Specialty)studentChange.comboBox1.SelectedItem;

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Объект обновлен");
            }
        }

        private void Button3_Click(object sender, EventArgs e)
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
