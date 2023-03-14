using Baremiseur.Contexts;
using Baremiseur.Models;
using Baremiseur.Windows.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Baremiseur.Pages
{
    /// <summary>
    /// Logique d'interaction pour ProductsControl.xaml
    /// </summary>
    public partial class StudentsControl : UserControl
    {
        public Classroom classroom;

        public StudentsControl()
        {
            InitializeComponent();
            RefreshClassrooms();
            addStudent.IsEnabled = false;
        }

        public void RefreshStudents()
        {
            if (classroom == null)
            {
                addStudent.IsEnabled = false;
                return;
            }

            addStudent.IsEnabled = true;

            StudentList.Items.Clear();
            using (var db = new StudentsContext())
            {
                db.Students.Where(s => s.Classroom == classroom).ToList().ForEach(delegate (Student student)
                {
                    StudentList.Items.Add(student);
                });
            }
        }

        private void ChangeClassroom(object sender, RoutedEventArgs e)
        {
            Classroom? classroom = ((FrameworkElement)sender).DataContext as Classroom;
            if (classroom == null) return;

            this.classroom = classroom;
            RefreshStudents();
        }

        private void AddClassroomClick(object sender, RoutedEventArgs e)
        {
            new AddClassroom(this).Show();
        }

        private void EditClassroomClick(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveClassroomClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddStudentClick(object sender, RoutedEventArgs e)
        {
            new AddStudent(this).Show();
        }

        private void EditStudentClick(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Voulez-vous enregistrer les modifications apportées au produit ?", "Confirmation", MessageBoxButton.YesNo);
        }

        private void RemoveStudentClick(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Voulez-vous vraiment supprimer cet élève ?", "Confirmation", MessageBoxButton.YesNo);
            if (confirm != MessageBoxResult.Yes) return;

            Student? student = ((FrameworkElement)sender).DataContext as Student;

            if (student != null)
            {
                using (var db = new StudentsContext())
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                }
            }

            RefreshStudents();
        }

        public void RefreshClassrooms()
        {
            ClassroomList.Items.Clear();
            using (var db = new StudentsContext())
            {
                db.Classrooms.ToList().ForEach(delegate (Classroom classroom)
                {
                    ClassroomList.Items.Add(classroom);
                });
            }
        }
    }
}
