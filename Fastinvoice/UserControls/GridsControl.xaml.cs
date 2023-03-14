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
    /// Logique d'interaction pour GridControl.xaml
    /// </summary>
    public partial class GridsControl : UserControl
    {

        public Models.Grid grid;

        public GridsControl()
        {
            InitializeComponent();
            RefreshGrids();
            addSkill.IsEnabled = false;
        }


        private void ChangeGrid(object sender, RoutedEventArgs e)
        {
            Models.Grid? grid = ((FrameworkElement)sender).DataContext as Models.Grid;
            if (grid == null) return;

            this.grid = grid;
            RefreshSkills();
        }

        internal void RefreshGrids()
        {
            GridsList.Items.Clear();
            using (var db = new StudentsContext())
            {
                db.Grids.ToList().ForEach(grid =>
                {
                    GridsList.Items.Add(grid);
                });
            }
        }

        internal void RefreshSkills()
        {
            if (grid == null)
            {
                addSkill.IsEnabled = false;
                return;
            }

            addSkill.IsEnabled = true;

            SkillsList.Items.Clear();
            using (var db = new StudentsContext())
            {
                db.Skills.Where(skill => skill.Grid == grid).ToList().ForEach(skill =>
                {
                    SkillsList.Items.Add(skill);
                });
            }
        }

        private void AddGridClick(object sender, RoutedEventArgs e)
        {
            new AddGrid(this).Show();
        }

        private void AddSkillClick(object sender, RoutedEventArgs e)
        {
            new AddSkill(this).Show();
        }
    }
}
