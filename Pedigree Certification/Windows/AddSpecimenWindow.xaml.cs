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
    /// Interaction logic for AddSpecimenWindow.xaml
    /// </summary>
    public partial class AddSpecimenWindow : Window
    {
        #region vars
        //private readonly int MIN_INPUT = 3;

        private List<string> _chipsMother = new List<string>();
        private List<string> _chipsFather = new List<string>();

        private List<string> _namesMother = new List<string>();
        private List<string> _namesFather = new List<string>();

        private List<string> _nicknamesMother = new List<string>();
        private List<string> _nicknamesFather = new List<string>();

        private List<string> _namesOwner = new List<string>();
        private List<string> _surnamesOwner = new List<string>();

        private List<string> _namesBreeder = new List<string>();
        private List<string> _surnamesBreeder = new List<string>();

        private PedigreeDbContext context = new PedigreeDbContext();

        private List<Dog> _fathers = null;
        private List<Dog> _mothers = null;

        private List<Breeder> _breeders = null;
        private List<Owner> _owners = null;

        private bool _modify = false;
        private int? _idToModify = null;

        #endregion

        #region ctor
        public AddSpecimenWindow()
        {
            InitializeComponent();
            this.MotherChipCombobox.ItemsSource = _chipsMother;
            this.MotherNameCombobox.ItemsSource = _namesMother;
            this.MotherNicknameCombobox.ItemsSource = _nicknamesMother;

            this.FatherChipCombobox.ItemsSource = _chipsFather;
            this.FatherNameCombobox.ItemsSource = _namesFather;
            this.FatherNicknameCombobox.ItemsSource = _nicknamesFather;

            this.OwnerNameCombobox.ItemsSource = _namesOwner;
            this.OwnerSurnameCombobox.ItemsSource = _surnamesOwner;

            this.BreederNameCombobox.ItemsSource = _namesBreeder;
            this.BreederSurnameCombobox.ItemsSource = _surnamesBreeder;

            this.CertificateNo.Text = (context.Dogs.Max(x=>x.CertificationNo) + 1).ToString();
        }

        public AddSpecimenWindow(int Id)
        {
            InitializeComponent();

            this.MotherChipCombobox.ItemsSource = _chipsMother;
            this.MotherNameCombobox.ItemsSource = _namesMother;
            this.MotherNicknameCombobox.ItemsSource = _nicknamesMother;

            this.FatherChipCombobox.ItemsSource = _chipsFather;
            this.FatherNameCombobox.ItemsSource = _namesFather;
            this.FatherNicknameCombobox.ItemsSource = _nicknamesFather;

            this.OwnerNameCombobox.ItemsSource = _namesOwner;
            this.OwnerSurnameCombobox.ItemsSource = _surnamesOwner;

            this.BreederNameCombobox.ItemsSource = _namesBreeder;
            this.BreederSurnameCombobox.ItemsSource = _surnamesBreeder;

            try
            {
                var dog = context.Dogs.Where(x => x.DogId == Id).FirstOrDefault();
                _idToModify = Id;

                Dog father;
                Dog mother;

                if(dog.FatherId != null)
                {
                    father = context.Dogs.Where(x => x.DogId == dog.FatherId).SingleOrDefault();
                    this.FatherChipNumber.Text = father.ChipNumber;
                    this.FatherName.Text = father.Name;
                    this.FatherNickname.Text = father.Nickname;
                }

                if (dog.MotherId != null)
                {
                    mother = context.Dogs.Where(x => x.DogId == dog.MotherId).SingleOrDefault();
                    this.MotherChipNumber.Text = mother.ChipNumber;
                    this.MotherName.Text = mother.Name;
                    this.MotherNickname.Text = mother.Nickname;
                }

                this.ChipNumber.Text = dog.ChipNumber.ToString();
                this.Coat.Text = dog.Coat.ToString();
                this.DNA.Text = dog.DNA.ToString();
                this.Dysplasia.Text = dog.Dysplasia.ToString();
                this.ExhibitionTitles.Text = dog.ExhibitionTitles.ToString();
                this.DogName.Text = dog.Name.ToString();
                this.PedegreeCertificateNumber.Text = dog.PedegreeCertificateNumber.ToString();
                this.Sex.Text = dog.Sex.ToString();
                this.Training.Text = dog.Training.ToString();
                this.CertificateNo.Text = dog.CertificationNo.ToString();
                this.Rase.Text = dog.Rase;
                this.Nickname.Text = dog.Nickname;
                this.BirthDate.SelectedDate = dog.BirthDate;
                this.CertificationDate.SelectedDate = dog.CertificationDate;

                if (dog.Owner != null)
                {
                    this.OwnerName.Text = dog.Owner.Name;
                    this.OwnerSurname.Text = dog.Owner.Surname;
                    this.OwnerAdress.Text = dog.Owner.Adderss;
                }

                if (dog.Breeder != null)
                {
                    this.BreederName.Text = dog.Breeder.Name;
                    this.BreederSurname.Text = dog.Breeder.Surname;
                }
                this._modify = true;
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }
        #endregion

        #region buttons
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_modify)
                {
                    Window browseWindow = new BrowseSpecimensWindow();
                    browseWindow.Left = this.Left;
                    browseWindow.Top = this.Top;
                    browseWindow.Show();
                    this.Close();
                }
                else
                {
                    Window mainWindow = new MainWindow();
                    mainWindow.Left = this.Left;
                    mainWindow.Top = this.Top;
                    mainWindow.Show();
                    this.Close();
                }
            }catch(Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dog specimen = null;
                if (_modify == false)
                {
                    specimen = new Dog();
                }
                else
                {
                    specimen = context.Dogs.Where(x => x.DogId == _idToModify).SingleOrDefault();
                }

                bool noFather = false;
                bool noMother = false;
                int? fatherId = null;
                int? motherId = null;

                Breeder breeder = null;
                Owner owner = null;
                //parents
                MessageBoxResult popupResult = MessageBoxResult.Yes;


                owner = context.Owners.Where(x => x.Name == this.OwnerName.Text.Trim() & x.Surname == this.OwnerSurname.Text.Trim() & x.Adderss == this.OwnerAdress.Text.Trim()).SingleOrDefault();

                if (this.BreederName.Text.Trim().Length != 0 & this.BreederSurname.Text.Trim().Length != 0)
                {
                    breeder = context.Breeders.Where(x => x.Name == this.BreederName.Text.Trim() & x.Surname == this.BreederSurname.Text.Trim()).SingleOrDefault();
                    if (breeder == null)
                    {
                        var pop = System.Windows.MessageBox.Show("Hodowca nie odnaleziony. Czy chcesz dodać do bazy?", "Brak hodowcy", System.Windows.MessageBoxButton.YesNo);
                        if (pop == MessageBoxResult.No)
                        {
                            return;
                        }
                        else if (pop == MessageBoxResult.Yes)
                        {
                            breeder = new Breeder()
                            {
                                Name = this.BreederName.Text.Trim(),
                                Surname = this.BreederSurname.Text.Trim()
                            };
                        }
                    }
                }

                if (this.OwnerName.Text.Trim().Length != 0 & this.OwnerSurname.Text.Trim().Length != 0 & this.OwnerAdress.Text.Trim().Length != 0)
                {
                    owner = context.Owners.Where(x => x.Name == this.OwnerName.Text.Trim() & x.Surname == this.OwnerSurname.Text.Trim() & x.Adderss == this.OwnerAdress.Text.Trim()).SingleOrDefault();
                    if (owner == null)
                    {
                        var pop = System.Windows.MessageBox.Show("Właściciel nie odnaleziony. Czy chcesz dodać do bazy?", "Brak hodowcy", System.Windows.MessageBoxButton.YesNo);
                        if (pop == MessageBoxResult.No)
                        {
                            return;
                        }
                        else if (pop == MessageBoxResult.Yes)
                        {
                            owner = new Owner()
                            {
                                Name = this.OwnerName.Text.Trim(),
                                Surname = this.OwnerSurname.Text.Trim(),
                                Adderss = this.OwnerAdress.Text.Trim()
                            };
                        }
                    }
                }

                if (this.FatherChipNumber.Text.Trim().Length == 0 & this.FatherName.Text.Trim().Length == 0 & this.FatherNickname.Text.Trim().Length == 0)
                {
                    if (System.Windows.MessageBox.Show("Czy chcesz dodać psa bez ojca?", "Pies bez rodzica", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        noFather = true;
                    }
                }
                if (this.MotherChipNumber.Text.Trim().Length == 0 & this.MotherName.Text.Trim().Length == 0 & this.MotherNickname.Text.Trim().Length == 0)
                {
                    if (System.Windows.MessageBox.Show("Czy chcesz dodać psa bez matki?", "Pies bez rodzica", System.Windows.MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        noMother = true;
                    }
                }

                if (noFather == false)
                {
                    ////Więcej niż jeden rodzic...
                    var fathers = context.Dogs.Where(x => x.Name == this.FatherName.Text.Trim() & x.Nickname == this.FatherNickname.Text.Trim() & x.ChipNumber == this.FatherChipNumber.Text.Trim()).SingleOrDefault();

                    if (fathers == null)
                    {
                        popupResult = System.Windows.MessageBox.Show("Nie udało się odnaleźć ojca lub w bazie znajduje się więcej niż jeden osobnik, do którego pasują parametry.\n Proszę poprawić dane lub pozostawić okna puste.", "Brak ojca", System.Windows.MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        fatherId = fathers.DogId;
                    }
                }

                if (noMother == false)
                {
                    var mothers = context.Dogs.Where(x => x.Name == this.MotherName.Text.Trim() & x.Nickname == this.MotherNickname.Text.Trim() & x.ChipNumber == this.MotherChipNumber.Text.Trim()).SingleOrDefault();

                    if (mothers == null)
                    {
                        popupResult = System.Windows.MessageBox.Show("Nie udało się odnaleźć matki lub w bazie znajduje się więcej niż jeden osobnik, do którego pasują parametry.\n Proszę poprawić dane lub pozostawić okna puste.", "Brak matki", System.Windows.MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        motherId = mothers.DogId;
                    }
                }


                if (this.BirthDate.SelectedDate.HasValue == true)
                {
                    specimen.BirthDate = this.BirthDate.SelectedDate.Value;
                }
                else
                {
                    specimen.BirthDate = null;
                }
                if (this.CertificationDate.SelectedDate.HasValue == true)
                {
                    specimen.CertificationDate = this.CertificationDate.SelectedDate.Value;
                }
                else
                {
                    specimen.CertificationDate = null;
                }
                specimen.ChipNumber = this.ChipNumber.Text.Trim();
                specimen.Coat = this.Coat.Text.Trim();
                specimen.DNA = this.DNA.Text.Trim();
                specimen.Dysplasia = this.Dysplasia.Text.Trim();
                specimen.ExhibitionTitles = this.ExhibitionTitles.Text.Trim();
                specimen.Name = this.DogName.Text.Trim();
                specimen.Nickname = this.Nickname.Text.Trim();
                specimen.Breeder = breeder;
                specimen.Owner = owner;
                specimen.PedegreeCertificateNumber = this.PedegreeCertificateNumber.Text.Trim();
                specimen.Sex = this.Sex.Text.Trim();
                specimen.Training = this.Training.Text.Trim();
                specimen.CertificationNo = Int32.Parse(this.CertificateNo.Text.Trim());
                specimen.Rase = this.Rase.Text.Trim();

                specimen.FatherId = fatherId;
                specimen.MotherId = motherId;

                if (_modify == false)
                {
                    context.Dogs.Add(specimen);
                }
                context.SaveChanges();

                if (_modify)
                {
                    Window browseWindow = new BrowseSpecimensWindow();
                    browseWindow.Left = this.Left;
                    browseWindow.Top = this.Top;
                    browseWindow.Height = this.Height;
                    browseWindow.Width = this.Width;
                    browseWindow.Show();
                    this.Close();
                }
                else
                {
                    Window mainWindow = new MainWindow();
                    mainWindow.Left = this.Left;
                    mainWindow.Top = this.Top;
                    mainWindow.Height = this.Height;
                    mainWindow.Width = this.Width;
                    mainWindow.Show();
                    this.Close();
                }

            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }
        #endregion

        #region Parent input trigers
        private void MotherChipNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateMotherChip(true);
        }

        private void PopulateMotherChip(bool show)
        {
            try {
                this.MotherChipCombobox.SelectedIndex = -1;
                _chipsMother.Clear();
                FindMother();

                foreach (var dog in _mothers)
                {
                    _chipsMother.Add(dog.ChipNumber);
                }

                if (this._chipsMother.Count != 0 & show)
                {
                    this.MotherChipCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.MotherChipCombobox.IsDropDownOpen = false;
                }
                this.MotherChipCombobox.Items.Refresh();
                this.MotherChipNumber.Focus();
            }catch(Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void MotherName_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateMotherName(true);
        }

        private void PopulateMotherName(bool show)
        {
            try {
                this.MotherNameCombobox.SelectedIndex = -1;
                _namesMother.Clear();
                FindMother();

                foreach (var dog in _mothers)
                {
                    _namesMother.Add(dog.Name);
                }
                if (this._namesMother.Count != 0 & show)
                {
                    this.MotherNameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.MotherNameCombobox.IsDropDownOpen = false;
                }
                this.MotherNameCombobox.Items.Refresh();
                this.MotherName.Focus();
            }catch(Exception x)
            {
                ShowError(x.Message);
            }
        }
        private void MotherNickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateMotherNickname(true);
        }

        private void PopulateMotherNickname(bool show)
        {
            try {
                this.MotherNicknameCombobox.SelectedIndex = -1;
                _nicknamesMother.Clear();
                FindMother();

                foreach (var dog in _mothers)
                {
                    _nicknamesMother.Add(dog.Nickname);
                }
                if (this._nicknamesMother.Count != 0 & show)
                {
                    this.MotherNicknameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.MotherNicknameCombobox.IsDropDownOpen = false;
                }
                this.MotherNicknameCombobox.Items.Refresh();
            }catch(Exception x)
            {
                ShowError(x.Message);
            }
        }
        private void FatherChipNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateFatherChip(true);
        }

        private void PopulateFatherChip(bool show)
        {
            try {
                this.FatherChipCombobox.SelectedIndex = -1;
                _chipsFather.Clear();
                FindFather();

                foreach (var dog in _fathers)
                {
                    _chipsFather.Add(dog.ChipNumber);
                }

                if (this._chipsFather.Count != 0 & show)
                {
                    this.FatherChipCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.FatherChipCombobox.IsDropDownOpen = false;
                }
                this.FatherChipCombobox.Items.Refresh();
                this.FatherChipNumber.Focus();
            }catch(Exception x)
            {
                ShowError(x.Message);
            }
        }
        private void FatherName_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateFatherName(true);
        }

        private void PopulateFatherName(bool show)
        {
            try {
                this.FatherNameCombobox.SelectedIndex = -1;
                _namesFather.Clear();
                FindFather();

                foreach (var dog in _fathers)
                {
                    _namesFather.Add(dog.Name);
                }
                if (this._namesFather.Count != 0 & show)
                {
                    this.FatherNameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.FatherNameCombobox.IsDropDownOpen = false;
                }
                this.FatherNameCombobox.Items.Refresh();
                this.FatherName.Focus();
            }catch(Exception x)
            {
                ShowError(x.Message);
            }
        }
        private void FatherNickname_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateFatherNickname(true);
        }

        private void PopulateFatherNickname(bool show)
        {
            try {
                this.FatherNicknameCombobox.SelectedIndex = -1;
                _nicknamesFather.Clear();
                FindFather();

                foreach (var dog in _fathers)
                {
                    _nicknamesFather.Add(dog.Nickname);
                }
                if (this._nicknamesFather.Count != 0 & show)
                {
                    this.FatherNicknameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.FatherNicknameCombobox.IsDropDownOpen = false;
                }
                this.FatherNicknameCombobox.Items.Refresh();
                this.FatherNickname.Focus();
            }catch(Exception x)
            {
                ShowError(x.Message);
            }
        }
        #endregion

        #region Parent combobox trigers
        private void MotherChipCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MotherChipCombobox.SelectedItem != null)
            {
                this.MotherChipNumber.Text = this.MotherChipCombobox.SelectedItem.ToString();
                var mother = context.Dogs.Where(x => x.ChipNumber == this.MotherChipNumber.Text.Trim());
                if (mother.Count() != 1)
                {
                    PopulateMotherName(true);
                }
                else
                {
                    this.MotherName.Text = mother.FirstOrDefault().Name;
                    this.MotherNickname.Text = mother.FirstOrDefault().Nickname;
                }
            }
        }
        private void MotherNameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MotherNameCombobox.SelectedItem != null)
            {
                this.MotherName.Text = this.MotherNameCombobox.SelectedItem.ToString();
                var mother = context.Dogs.Where(x => x.Name == this.MotherName.Text.Trim());
                if (mother.Count() != 1)
                {
                    PopulateMotherNickname(true);
                }
                else
                {
                    this.MotherChipNumber.Text = mother.FirstOrDefault().ChipNumber;
                    this.MotherNickname.Text = mother.FirstOrDefault().Nickname;
                }
            }
        }

        private void MotherNicknameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MotherNicknameCombobox.SelectedItem != null)
            {
                this.MotherNickname.Text = this.MotherNicknameCombobox.SelectedItem.ToString();
                var mother = context.Dogs.Where(x => x.Nickname == this.MotherNickname.Text.Trim());
                if (mother.Count() != 1)
                {
                    PopulateMotherChip(true);
                }
                else
                {
                    this.MotherChipNumber.Text = mother.FirstOrDefault().ChipNumber;
                    this.MotherName.Text = mother.FirstOrDefault().Name;
                }
            }
        }

        private void FatherChipCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FatherChipCombobox.SelectedItem != null)
            {
                this.FatherChipNumber.Text = this.FatherChipCombobox.SelectedItem.ToString();
                var father = context.Dogs.Where(x => x.ChipNumber == this.FatherChipNumber.Text.Trim());
                if (father.Count() != 1)
                {
                    PopulateFatherName(true);
                }
                else
                {
                    this.FatherName.Text = father.FirstOrDefault().Name;
                    this.FatherNickname.Text = father.FirstOrDefault().Nickname;
                }
            }
        }

        private void FatherNameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FatherNameCombobox.SelectedItem != null)
            {
                this.FatherName.Text = this.FatherNameCombobox.SelectedItem.ToString();
                var father = context.Dogs.Where(x => x.Name == this.FatherName.Text.Trim());
                if (father.Count() != 1)
                {
                    PopulateFatherNickname(true);
                }
                else
                {
                    this.FatherChipNumber.Text = father.FirstOrDefault().ChipNumber;
                    this.FatherNickname.Text = father.FirstOrDefault().Nickname;
                }
            }
        }

        private void FatherNicknameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FatherNicknameCombobox.SelectedItem != null)
            {
                this.FatherNickname.Text = this.FatherNicknameCombobox.SelectedItem.ToString();
                var father = context.Dogs.Where(x => x.Nickname == this.FatherNickname.Text.Trim());
                if(father.Count() != 1)
                {
                    PopulateMotherChip(true);
                }
                else
                {
                    this.FatherChipNumber.Text = father.FirstOrDefault().ChipNumber;
                    this.FatherName.Text = father.FirstOrDefault().Name;
                }
            }
        }
        #endregion

        #region methods
        private void FindFather()
        {
            try
            {
                _fathers = context.Dogs
                    .Where(x => x.ChipNumber.Contains(this.FatherChipNumber.Text.Trim()) | this.FatherChipNumber.Text.Trim().Length == 0)
                    .Where(x => x.Name.Contains(this.FatherName.Text.Trim()) | this.FatherName.Text.Trim().Length == 0)
                    .Where(x => x.Nickname.Contains(this.FatherNickname.Text.Trim()) | this.FatherNickname.Text.Trim().Length == 0)
                    .ToList();
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void FindMother()
        {
            try
            {
                _mothers = context.Dogs
                    .Where(x => x.ChipNumber.Contains(this.MotherChipNumber.Text.Trim()) | this.MotherChipNumber.Text.Trim().Length == 0)
                    .Where(x => x.Name.Contains(this.MotherName.Text.Trim()) | this.MotherName.Text.Trim().Length == 0)
                    .Where(x => x.Nickname.Contains(this.Nickname.Text.Trim()) | this.MotherNickname.Text.Trim().Length == 0)
                    .ToList();
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void FindBreeder()
        {
            try
            {
                _breeders = context.Breeders
                    .Where(x => x.Name.Contains(this.BreederName.Text.Trim()) | this.BreederName.Text.Trim().Length == 0)
                    .Where(x => x.Surname.Contains(this.BreederSurname.Text.Trim()) | this.BreederSurname.Text.Trim().Length == 0)
                    .ToList();
            }
            catch(Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void FindOwner()
        {
            try
            {
                _owners = context.Owners
                    .Where(x => x.Name.Contains(this.OwnerName.Text.Trim()) | this.OwnerName.Text.Trim().Length == 0)
                    .Where(x => x.Name.Contains(this.OwnerSurname.Text.Trim()) | this.OwnerSurname.Text.Trim().Length == 0)
                    .ToList();
            }
            catch(Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void ShowError(string Message)
        {
            System.Windows.MessageBox.Show(Message, "Błąd", System.Windows.MessageBoxButton.OK);
        }
        #endregion

        #region breeder and owner 
        private void BreederName_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateBreederName();
        }

        private void PopulateBreederName()
        {
            try
            {
                this.BreederNameCombobox.SelectedIndex = -1;
                _namesBreeder.Clear();
                FindBreeder();

                foreach (var breeder in _breeders)
                {
                    _namesBreeder.Add(breeder.Name);
                }
                if (this._namesBreeder.Count != 0)
                {
                    this.BreederNameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.BreederNameCombobox.IsDropDownOpen = false;
                }
                this.BreederNameCombobox.Items.Refresh();
                this.BreederName.Focus();
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void BreederNameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.BreederNameCombobox.SelectedItem != null)
            {
                this.BreederName.Text = this.BreederNameCombobox.SelectedItem.ToString();
                var breeder = context.Breeders.Where(x => x.Name == this.BreederName.Text.Trim());
                if (breeder.Count() != 1)
                {
                    PopulateBreederSurname();
                }
                else
                {
                    this.BreederSurname.Text = breeder.FirstOrDefault().Surname;
                }
            }
        }

        private void BreederSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateBreederSurname();
        }

        private void PopulateBreederSurname()
        {
            try
            {
                this.BreederSurnameCombobox.SelectedIndex = -1;
                _surnamesBreeder.Clear();
                FindBreeder();

                foreach (var breeder in _breeders)
                {
                    _surnamesBreeder.Add(breeder.Surname);
                }
                if (this._surnamesBreeder.Count != 0)
                {
                    this.BreederSurnameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.BreederSurnameCombobox.IsDropDownOpen = false;
                }
                this.BreederSurnameCombobox.Items.Refresh();
                this.BreederSurname.Focus();
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }
        private void BreederSurnameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.BreederSurnameCombobox.SelectedItem != null)
            {
                this.BreederSurname.Text = this.BreederSurnameCombobox.SelectedItem.ToString();
                var breeder = context.Breeders.Where(x => x.Surname == this.BreederSurname.Text.Trim());
                if (breeder.Count() != 1)
                {
                    PopulateBreederName();
                }
                else
                {
                    this.BreederName.Text = breeder.FirstOrDefault().Name;
                }
            }
        }

        private void OwnerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateOwnerName();
        }

        private void PopulateOwnerName()
        {
            try
            {
                this.OwnerNameCombobox.SelectedIndex = -1;
                _namesOwner.Clear();
                FindOwner();

                foreach (var owner in _owners)
                {
                    _namesOwner.Add(owner.Name);
                }
                if (this._namesOwner.Count != 0)
                {
                    this.OwnerNameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.OwnerNameCombobox.IsDropDownOpen = false;
                }
                this.OwnerNameCombobox.Items.Refresh();
                this.OwnerName.Focus();
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }
        private void OwnerNameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.OwnerNameCombobox.SelectedItem != null)
            {
                this.OwnerName.Text = this.OwnerNameCombobox.SelectedItem.ToString();
                var owner = context.Owners.Where(x => x.Name == this.OwnerName.Text.Trim());
                if (owner.Count() != 1)
                {
                    PopulateOwnerSurname();
                }
                else
                {
                    this.OwnerSurname.Text = owner.FirstOrDefault().Surname;
                    this.OwnerAdress.Text = owner.FirstOrDefault().Adderss;
                }
            }
        }

        private void OwnerSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            PopulateOwnerSurname();
        }

        private void PopulateOwnerSurname()
        {
            try
            {
                this.OwnerSurnameCombobox.SelectedIndex = -1;
                _surnamesOwner.Clear();
                FindOwner();

                foreach (var owner in _owners)
                {
                    _surnamesOwner.Add(owner.Surname);
                }
                if (this._surnamesOwner.Count != 0)
                {
                    this.OwnerSurnameCombobox.IsDropDownOpen = true;
                }
                else
                {
                    this.OwnerSurnameCombobox.IsDropDownOpen = false;
                }
                this.OwnerSurnameCombobox.Items.Refresh();
                this.OwnerSurname.Focus();
            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }
        private void OwnerSurnameCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.OwnerSurnameCombobox.SelectedItem != null)
            {
                this.OwnerSurname.Text = this.OwnerSurnameCombobox.SelectedItem.ToString();
                var owner = context.Owners.Where(x => x.Surname == this.OwnerSurname.Text.Trim());
                if (owner.Count() != 1)
                {
                    PopulateOwnerName();
                }
                else
                {
                    this.OwnerName.Text = owner.FirstOrDefault().Name;
                    this.OwnerAdress.Text = owner.FirstOrDefault().Adderss;
                }
            }
        }

        private void OwnerAdress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OwnerAdressCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion
    }
}
