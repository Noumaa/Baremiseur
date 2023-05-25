using Baremiseur.Contexts;
using Baremiseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Baremiseur.Pages
{
    /// <summary>
    /// Logique d'interaction pour DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl
    {
        private int index;
        private List<Student> students;

        public DashboardControl()
        {
            InitializeComponent();
            RefreshGridList();
            RefreshClassroomList();
            
            index = -1;
            students = new List<Student>();
        }

        public void RefreshGridList()
        {
            comboGrids.Items.Clear();
            using (var db = new StudentsContext())
            {
                foreach (Models.Grid grid in db.Grids)
                {
                    comboGrids.Items.Add(grid.Name);
                }
            }
        }

        public void RefreshClassroomList()
        {
            comboClassrooms.Items.Clear();
            using (var db = new StudentsContext())
            {
                foreach (Models.Classroom classroom in db.Classrooms)
                {
                    comboClassrooms.Items.Add(classroom.Name);
                }
            }
        }

        private void btnLaunchTest_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (comboGrids.SelectedItem == null || comboClassrooms.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une grille d'évaluation ainsi qu'une classe à évaluer.");
                return;
            }

            using (var db = new StudentsContext())
            {
                Models.Grid grid = db.Grids.Where(g => g.Name == comboGrids.SelectedItem.ToString()).First();
                Models.Classroom classroom = db.Classrooms.Where(g => g.Name == comboClassrooms.SelectedItem.ToString()).First();
                
                if (grid == null || classroom == null)
                {
                    MessageBox.Show("Un problème est survenu, veuillez contacter le développeur.");
                    return;
                }

                index = -1;
                students = new List<Student>(db.Students.Where(s => s.Classroom == classroom).ToList());
            }
            btnNextStudent.IsEnabled = true;
            nextStudent();
        }

        private void nextStudent()
        {
            index += 1;
            Student student;
            try
            {
                student = students[index];
            }
            catch (ArgumentOutOfRangeException e) {
                MessageBox.Show("C'était le dernier élève.");
                index = 0;
                student = students.First();
            }

            labelStudent.Content = "Elève : " + student.Firstname + " " + student.Lastname.ToUpper();
        }

        private void btnNextStudent_Click(object sender, RoutedEventArgs e)
        {
            nextStudent();
        }
    }
}
