using Baremiseur.Contexts;
using Baremiseur.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

namespace Baremiseur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Brush NavBackColor;

        public MainWindow()
        {
            using (var db = new StudentsContext())
            {
                db.Database.EnsureCreated();
            }

            InitializeComponent();
            NavBackColor = NavDashboard.Background;

            Display(NavDashboard);
        }

        private void Display(Button button)
        {
            Dictionary<Button, UserControl> buttons = new Dictionary<Button, UserControl>()
            {
                { NavDashboard, new DashboardControl() },
                { NavStudents, new StudentsControl() }
            };

            foreach (var b in buttons.Keys)
            {
                if (!b.Equals(button)) b.Background = NavBackColor;
            }

            button.Background = Brushes.Orange;

            MainFrame.Content = buttons[button];
        }

        private void NavDashboardClick(object sender, RoutedEventArgs e)
        {
            Display((Button)sender);
        }

        private void NavStudentsClick(object sender, RoutedEventArgs e)
        {
            Display((Button)sender);
        }
    }
}
