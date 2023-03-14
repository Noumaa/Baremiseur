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
    /// Logique d'interaction pour AddSkill.xaml
    /// </summary>
    public partial class AddSkill : Window
    {

        private GridsControl control;

        public AddSkill(GridsControl control)
        {
            InitializeComponent();
            this.control = control;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new StudentsContext())
            {
                db.Skills.Add(new Skill { Name = TextName.Text, VeryBad = TextVeryBad.Text, Bad = TextBad.Text, Good = TextGood.Text, VeryGood = TextVeryGood.Text, GridId = control.grid.Id });
                db.SaveChanges();
            }
            control.RefreshSkills();
        }
    }
}
