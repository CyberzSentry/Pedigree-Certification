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

namespace Pedigree_Certification
{
    /// <summary>
    /// Interaction logic for BrowseSpecimensWindow.xaml
    /// </summary>
    public partial class BrowseSpecimensWindow : Window
    {
        private PedigreeDbContext context = new PedigreeDbContext();

        public BrowseSpecimensWindow()
        {
            InitializeComponent();
            var dogs = context.Dogs.ToList();
            this.DogsGrid.ItemsSource = dogs;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            FindDogs();
        }

        private void FindDogs()
        {
            var dogs = context.Dogs
                .Where(x => x.ChipNumber.Contains(this.ChipNumber.Text.Trim()) | this.ChipNumber.Text.Trim().Length == 0)
                .Where(x => x.Name.Contains(this.DogName.Text.Trim()) | this.DogName.Text.Trim().Length == 0)
                .Where(x => x.Nickname.Contains(this.Nickname.Text.Trim()) | this.Nickname.Text.Trim().Length == 0)
                .ToList();

            if (dogs.Count == 0)
            {
                System.Windows.MessageBox.Show("Nie odnaleziono psów", "Info", System.Windows.MessageBoxButton.OK);
            }
            else
            {
                this.DogsGrid.ItemsSource = dogs;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window mainWindow = new MainWindow();
                mainWindow.Left = this.Left;
                mainWindow.Top = this.Top;
                mainWindow.Show();
                

                this.Close();
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void ShowError(string Message)
        {
            System.Windows.MessageBox.Show(Message, "Błąd", System.Windows.MessageBoxButton.OK);
        }

        private void DogsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dog dog = (Dog)this.DogsGrid.SelectedItem;

                if (dog != null)
                {
                    Window modifyDog = new AddSpecimenWindow(dog.DogId);
                    modifyDog.Left = this.Left;
                    modifyDog.Top = this.Top;
                    modifyDog.Show();

                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Nie wybrano psa.", "Błąd", System.Windows.MessageBoxButton.OK);
                }

            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dog dog = (Dog)this.DogsGrid.SelectedItem;

                if (dog != null)
                {
                    Window printWindow = new IssueLineageWindow(dog.DogId);
                    printWindow.Left = this.Left;
                    printWindow.Top = this.Top;
                    printWindow.Show();
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Nie wybrano psa.", "Błąd", System.Windows.MessageBoxButton.OK);
                }
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }
    }
}
