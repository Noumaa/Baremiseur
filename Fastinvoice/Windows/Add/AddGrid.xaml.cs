using Baremiseur.Contexts;
using Baremiseur.Models;
using Baremiseur.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    public partial class AddGrid : Window
    {

        private GridsControl control;

        public AddGrid(GridsControl control)
        {
            InitializeComponent();
            this.control = control;
        }

        private void AddClassroomClick(object sender, RoutedEventArgs e)
        {
            using (var db = new StudentsContext())
            {
                db.Grids.Add(new Grid { Name = TextName.Text });
                db.SaveChanges();
            }
            control.RefreshGrids();
        }
    }
}
