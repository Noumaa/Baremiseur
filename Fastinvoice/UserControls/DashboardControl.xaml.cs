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

            Models.Grid grid;
            Models.Classroom classroom;

            using (var db = new StudentsContext())
            {
                grid = db.Grids.Where(g => g.Name == comboGrids.SelectedItem.ToString()).First();
                classroom = db.Classrooms.Where(g => g.Name == comboClassrooms.SelectedItem.ToString()).First();
                
                if (grid == null || classroom == null)
                {
                    MessageBox.Show("Un problème est survenu, veuillez contacter le développeur. #007");
                    return;
                }

                index = -1;
                students = new List<Student>(db.Students.Where(s => s.Classroom == classroom).ToList());

                List<Skill> skills = db.Skills.Where(s => s.GridId == grid.Id).ToList();
                MessageBox.Show("test1 " + skills.Count);
            }
            btnNextStudent.IsEnabled = true;

            if (grid.Skills == null)
            {
                MessageBox.Show("Un problème est survenu, veuillez contacter le développeur. #006");
                return;
            }

            StackPanel stackPanel = new StackPanel();

            Label title = new Label();
            title.Content = grid.Name;

            stackPanel.Children.Add(title);

            foreach (Skill skill in grid.Skills)
            {
                MessageBox.Show("test");
                StackPanel skillPanel = new StackPanel();
                skillPanel.Orientation = Orientation.Horizontal;

                Label label = new Label();
                label.Content = skill.Name;

                CheckBox veryBad = new CheckBox();
                veryBad.Name = "veryBad";

                CheckBox bad = new CheckBox();
                bad.Name = "bad";

                CheckBox good = new CheckBox();
                good.Name = "good";

                CheckBox veryGood = new CheckBox();
                veryGood.Name = "veryGood";

                skillPanel.Children.Add(label);
                skillPanel.Children.Add(veryBad);
                skillPanel.Children.Add(bad);
                skillPanel.Children.Add(good);
                skillPanel.Children.Add(veryGood);

                stackPanel.Children.Add(skillPanel);
            }

            frame.Content = stackPanel;

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

        private void Import(object sender, RoutedEventArgs e)
        {
            XmlImport import = new XmlImport();
            import.Import("C:\\Users\\vallee\\Downloads\\sio_v2012_final.xml");
            RefreshSkillTree();
        }

        private void RefreshSkillTree()
        {
            using (var db = new StudentsContext())
            {
                // Affecter les compétences à la source de données de la TreeView
                skillTreeView.ItemsSource = db.Skills.ToList();
            }
        }
    }
}
