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

namespace Pedigree_Certification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void AddSpecimen_Click(object sender, RoutedEventArgs e)
        {
            Window addSpecimenWindow = new AddSpecimenWindow();
            addSpecimenWindow.Left = this.Left;
            addSpecimenWindow.Top = this.Top;
            addSpecimenWindow.Show();
            this.Close();
        }

        private void IssueLineage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BrowseSpecimens_Click(object sender, RoutedEventArgs e)
        {
            Window browse = new BrowseSpecimensWindow();
            browse.Left = this.Left;
            browse.Top = this.Top;
            browse.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
