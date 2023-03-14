using Baremiseur.Contexts;
using Baremiseur.Models;
using Baremiseur.Pages;
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
using System.Windows.Shapes;

namespace Baremiseur.Windows.Add
{
    /// <summary>
    /// Logique d'interaction pour AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {

        private StudentsControl control;

        public AddStudent(StudentsControl control)
        {
            InitializeComponent();
            this.control = control;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new StudentsContext())
            {
                db.Students.Add(new Student { Firstname = TextFirstname.Text, Lastname = TextLastname.Text, Gender = ComboGender.Text == "Homme", ClassroomId = control.classroom.Id });
                db.SaveChanges();
            }
            control.RefreshStudents();
        }
    }
}
