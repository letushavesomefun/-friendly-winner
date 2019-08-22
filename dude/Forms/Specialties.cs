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
    public partial class Specialties : Form
    {
        StudentContext db;
        StudentContext db1;



        public Specialties()
        {
            InitializeComponent();

            db = new StudentContext();
            
            db.Specialties.Load();
            db.Faculties.Load();  
            dataGridView1.DataSource = db.Specialties.Local.ToBindingList();
            dataGridView2.DataSource = db.Faculties.Local.ToBindingList();
        }
        // добавление
        private void button1_Click(object sender, EventArgs e)
        {
            SpecialtyChange tmForm = new SpecialtyChange();
             List<Faculty> faculties = db.Faculties.ToList();
            textBox1.Text = faculties.ToString();
            tmForm.comboBox1.DataSource = faculties;
            tmForm.comboBox1.ValueMember = "Id";
            tmForm.comboBox1.DisplayMember = "Name";
            DialogResult result = tmForm.ShowDialog(this);
           

            if (result == DialogResult.Cancel)
                return;
            Specialty specialty = new Specialty();
            specialty.Name = tmForm.textBox1.Text;
            specialty.Faculty = (Faculty)tmForm.comboBox1.SelectedItem;
            db.Specialties.Add(specialty);
            db.SaveChanges();
            MessageBox.Show("Новый объект добавлен");
        }
        
       

        private void Button1_Click_1(object sender, EventArgs e)
        {
            SpecialtyChange tmForm = new SpecialtyChange();
            DialogResult result = tmForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;
            Specialty specialty = new Specialty();
            specialty.Name = tmForm.textBox1.Text;
            db.Specialties.Add(specialty);
            db.SaveChanges();
            MessageBox.Show("Новый объект добавлен");
        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }
    }
}
 