using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Pedigree_Certification
{
    /// <summary>
    /// Interaction logic for IssueLineageWindow.xaml
    /// </summary>
    public partial class IssueLineageWindow : Window
    {
        private PedigreeDbContext context = new PedigreeDbContext();
        private double A4_HEIGHT_HORIZONTAL = 794;
        private double A4_WIDTH_HORIZONTAL = 1122;

        private double BLOCK_HEIGHT = 25;
        private double BLOCK_WIDTH = 250;
        private double BLOCK_WIDTH_2 = 193.5;

        private double FONT_SIZE_FRONT = 19;
        private double FONT_SIZE_TABLE = 10;
        private double FONT_SIZE_FOTTER = 8;
        private double FONT_SIZE_BROW = 15;
        private double LINE_SEPARATOR = 0.5;

        private double BLOCK_WIDTH_BACK = 770;

        private double COLUMN_1;
        private double COLUMN_2;
        private double COLUMN_3;
        private double COLUMN_4;

        #region coords 1

        private double PCN_TOP = 306.64;
        private double PCN_LEFT = 615.29;

        private double RS_TOP = 492.54;
        private double RS_LEFT = 784.09;

        private double NM_TOP = 528.9;
        private double NM_LEFT = 784.09;

        private double CT_TOP = 564.7;
        private double CT_LEFT = 784.09;

        private double CPTT_TOP = 595.94;
        private double CPTT_LEFT = 784.09;

        private double BD_TOP = 629.18;
        private double BD_LEFT = 784.09;

        private double SX_TOP = 662.43;
        private double SX_LEFT = 784.09;

        private double CD_TOP = 698.26;
        private double CD_LEFT = 265.58;

        private double CN_TOP = 738.6;
        private double CN_LEFT = 182.94;
        #endregion

        #region coords 2

        private double P1_LEFT = 94.294964;
        private double P1_RIGHT = 313.601482;
        private double P2_LEFT = 325.601482;
        private double P2_RIGHT = 560.599202;
        private double P3_LEFT = 570.599202;
        private double P3_RIGHT = 788.965222;
        private double P4_LEFT = 799.965222;
        private double P4_RIGHT = 1069.22634;

        private double P_TOP = 82.21514;
        private double P_BOT = 642.045931;

        private double P11_TOP = 361.170;

        private double P21_TOP = 221.053183;
        private double P22_TOP = 502.56809;

        private double P31_TOP = 151.954069;
        private double P32_TOP = 291.43191;
        private double P33_TOP = 433.468977;
        private double P34_TOP = 573.586624;

        private double P41_TOP = 117.404512;
        private double P42_TOP = 187.783239;
        private double P43_TOP = 258.161966;
        private double P44_TOP = 327.26108;
        private double P45_TOP = 397.639807;
        private double P46_TOP = 467.378727;
        private double P47_TOP = 537.117647;
        private double P48_TOP = 606.856567;

        private double BR_TOP = 668.676068;
        private double BR_LEFT = 156.401938;

        private double OW_TOP = 713.46253;
        private double OW_LEFT = 156.401938;

        private double CNB_TOP = 750.9;
        private double CNB_LEFT = 103.94;

        #endregion

        private Dog dog;

        private Dog Father_1;
        private Dog Mother_1;

        private Dog[] Fathers_2 = { null, null };
        private Dog[] Mothers_2 = { null, null };


        private Dog[] Fathers_3 = { null, null, null, null };
        private Dog[] Mothers_3 = { null, null, null, null };

        private Dog[] Fathers_4 = { null, null, null, null, null, null, null, null };
        private Dog[] Mothers_4 = { null, null, null, null, null, null, null, null };

        public IssueLineageWindow(int Id)
        {
            try
            {
                //this.CurrentDate.DisplayDate = DateTime.Now;

                COLUMN_1 = P1_RIGHT - P1_LEFT;
                COLUMN_2 = P2_RIGHT - P2_LEFT;
                COLUMN_3 = P3_RIGHT - P3_LEFT;
                COLUMN_4 = P4_RIGHT - P4_LEFT;

                var rotate = new RotateTransform(90);
                dog = context.Dogs.Where(x => x.DogId == Id).SingleOrDefault();
                FillPatents();
                InitializeComponent();

                FixedPage page1 = new FixedPage() { Width = A4_WIDTH_HORIZONTAL, Height = A4_HEIGHT_HORIZONTAL };
                FixedPage page2 = new FixedPage() { Width = A4_WIDTH_HORIZONTAL, Height = A4_HEIGHT_HORIZONTAL };

                TextBlock Rase = new TextBlock() { FontSize = FONT_SIZE_FRONT, Text = dog.Rase, Margin = new Thickness(RS_LEFT, RS_TOP, A4_WIDTH_HORIZONTAL - RS_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - RS_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center, FontFamily = new FontFamily("FuturaTEEMed") };
                TextBlock Sex = new TextBlock() { FontSize = FONT_SIZE_FRONT, Text = dog.Sex, Margin = new Thickness(SX_LEFT, SX_TOP, A4_WIDTH_HORIZONTAL - SX_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - SX_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                TextBlock Coat = new TextBlock() { FontSize = FONT_SIZE_FRONT, Text = dog.Coat, Margin = new Thickness(CT_LEFT, CT_TOP, A4_WIDTH_HORIZONTAL - CT_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - CT_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                TextBlock Names = new TextBlock() { FontWeight = FontWeights.Bold, FontSize = FONT_SIZE_FRONT, Text = dog.Name + " " + dog.Nickname, Margin = new Thickness(NM_LEFT, NM_TOP, A4_WIDTH_HORIZONTAL - NM_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - NM_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                TextBlock ChipTat = new TextBlock() { FontSize = FONT_SIZE_FRONT, Text = dog.ChipNumber, Margin = new Thickness(CPTT_LEFT, CPTT_TOP, A4_WIDTH_HORIZONTAL - CPTT_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - CPTT_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                TextBlock BirthDate = new TextBlock() { FontSize = FONT_SIZE_FRONT, Text = dog.BirthDate.Value.Date.ToString("dd. MM. yyyy"), Margin = new Thickness(BD_LEFT, BD_TOP, A4_WIDTH_HORIZONTAL - BD_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - BD_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                TextBlock CertificationNo = new TextBlock() { FontSize = FONT_SIZE_FOTTER, Text = "KP/R/" + dog.CertificationNo.ToString(), Margin = new Thickness(CN_LEFT, CN_TOP, A4_WIDTH_HORIZONTAL - CN_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - CN_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                TextBlock CertificationDate = new TextBlock() { FontSize = FONT_SIZE_FRONT, Text = DateTime.Now.ToString("dd. MM. yyyy"), Margin = new Thickness(CD_LEFT, CD_TOP, A4_WIDTH_HORIZONTAL - CD_LEFT - BLOCK_WIDTH_2, A4_HEIGHT_HORIZONTAL - CD_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                TextBlock PedegreeCertificateNumber = new TextBlock() { FontSize = FONT_SIZE_FRONT, Text = dog.PedegreeCertificateNumber, Margin = new Thickness(PCN_LEFT, PCN_TOP, A4_WIDTH_HORIZONTAL - PCN_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - PCN_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center };


                TextBlock FatherBlock_1 = new TextBlock() { FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P1_LEFT, P_TOP, A4_WIDTH_HORIZONTAL - P1_RIGHT, A4_HEIGHT_HORIZONTAL - P11_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_1, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                TextBlock MotherBlock_1 = new TextBlock() { FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P1_LEFT, P11_TOP, A4_WIDTH_HORIZONTAL - P1_RIGHT, A4_HEIGHT_HORIZONTAL - P_BOT), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_1, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };

                TextBlock[] FathersBlocks_2 =
                {
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P2_LEFT, P_TOP, A4_WIDTH_HORIZONTAL - P2_RIGHT, A4_HEIGHT_HORIZONTAL - P21_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_2, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P2_LEFT, P11_TOP, A4_WIDTH_HORIZONTAL - P2_RIGHT, A4_HEIGHT_HORIZONTAL - P22_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_2, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center }
                };

                TextBlock[] MothersBlocks_2 =
                {
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P2_LEFT, P21_TOP, A4_WIDTH_HORIZONTAL - P2_RIGHT, A4_HEIGHT_HORIZONTAL - P11_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_2, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P2_LEFT, P22_TOP, A4_WIDTH_HORIZONTAL - P2_RIGHT, A4_HEIGHT_HORIZONTAL - P_BOT), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_2, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center }
                };

                TextBlock[] FathersBlocks_3 =
                {
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P31_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P21_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P32_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P11_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P33_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P22_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P34_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center }
                };

                TextBlock[] MothersBlocks_3 =
                {
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P31_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P21_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P32_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P11_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P33_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P22_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P3_LEFT, P34_TOP, A4_WIDTH_HORIZONTAL - P3_RIGHT, A4_HEIGHT_HORIZONTAL - P_BOT), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_3, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center }
                };

                TextBlock[] FathersBlocks_4 =
                {
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P41_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P31_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P42_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P21_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P43_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P32_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P44_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P11_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P45_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P33_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P46_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P22_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P47_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P34_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P48_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center }
                };
                TextBlock[] MothersBlocks_4 =
                {
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P41_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P31_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P42_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P21_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P43_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P32_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P44_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P11_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P45_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P33_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P46_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P22_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P47_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P34_TOP), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center },
                    new TextBlock() {LineStackingStrategy = LineStackingStrategy.BlockLineHeight ,LineHeight = FONT_SIZE_TABLE + LINE_SEPARATOR ,FontSize = FONT_SIZE_TABLE, Margin = new Thickness(P4_LEFT, P48_TOP, A4_WIDTH_HORIZONTAL - P4_RIGHT, A4_HEIGHT_HORIZONTAL - P_BOT), TextWrapping = TextWrapping.Wrap, MaxWidth = COLUMN_4, TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center }
                };

                TextBlock CertificationNoBack = new TextBlock() { FontSize = FONT_SIZE_FOTTER, Text = "KP/R/" + dog.CertificationNo.ToString(), Margin = new Thickness(CNB_LEFT, CNB_TOP, A4_WIDTH_HORIZONTAL - CNB_LEFT - BLOCK_WIDTH, A4_HEIGHT_HORIZONTAL - CNB_TOP - BLOCK_HEIGHT), TextAlignment = TextAlignment.Left, VerticalAlignment = VerticalAlignment.Center };

                var pc1 = new PageContent();
                var pc2 = new PageContent();

                #region page 1

                var sourceFront = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/KP_RODOWOD_1str.jpg"));
                Image front = new Image() { Source = sourceFront, Stretch = Stretch.UniformToFill };
                Grid grid1 = new Grid() { };

                grid1.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(page1.Height) });
                grid1.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(page1.Width) });
                grid1.Children.Add(front);
                grid1.Children.Add(Rase);
                grid1.Children.Add(PedegreeCertificateNumber);
                grid1.Children.Add(Names);
                grid1.Children.Add(Sex);
                grid1.Children.Add(Coat);
                grid1.Children.Add(ChipTat);
                grid1.Children.Add(BirthDate);
                grid1.Children.Add(CertificationDate);
                grid1.Children.Add(CertificationNo);

                page1.Children.Add(grid1);
                pc1.Child = page1;

                #endregion

                #region page 2

                var sourceBack = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/KP_RODOWOD_2str.jpg"));
                Image back = new Image() { Source = sourceBack, Stretch = Stretch.UniformToFill };
                Grid grid2 = new Grid() { };
                grid2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(page2.Height) });
                grid2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(page2.Width) });

                grid2.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(page2.Height) });
                grid2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(page2.Width) });
                grid2.Children.Add(back);
                grid2.Children.Add(CertificationNoBack);

                if (dog.Breeder != null)
                {
                    TextBlock BreederText = new TextBlock() { VerticalAlignment = VerticalAlignment.Center, FontSize = FONT_SIZE_BROW, Text = dog.Breeder.Name + " " + dog.Breeder.Surname, Margin = new Thickness(BR_LEFT, BR_TOP, A4_WIDTH_HORIZONTAL - BR_LEFT - BLOCK_WIDTH_BACK, A4_HEIGHT_HORIZONTAL - BR_TOP - BLOCK_HEIGHT) };
                    grid2.Children.Add(BreederText);
                }

                if (dog.Owner != null)
                {
                    TextBlock OwnerText = new TextBlock() { VerticalAlignment = VerticalAlignment.Center, FontSize = FONT_SIZE_BROW, Text = dog.Owner.Name + " " + dog.Owner.Surname + " " + dog.Owner.Adderss, Margin = new Thickness(OW_LEFT, OW_TOP, A4_WIDTH_HORIZONTAL - OW_LEFT - BLOCK_WIDTH_BACK, A4_HEIGHT_HORIZONTAL - OW_TOP - BLOCK_HEIGHT) };
                    grid2.Children.Add(OwnerText);
                }

                if (Father_1 != null)
                {
                    FatherBlock_1.Text = FillGeneration1(Father_1);
                }

                if (Mother_1 != null)
                {

                    MotherBlock_1.Text = FillGeneration1(Mother_1);
                }

                grid2.Children.Add(FatherBlock_1);
                grid2.Children.Add(MotherBlock_1);

                for (int i = 0; i < 2; i++)
                {
                    if (Fathers_2[i] != null)
                    {
                        FathersBlocks_2[i].Text = FillGeneration2(Fathers_2[i]);
                        grid2.Children.Add(FathersBlocks_2[i]);
                    }

                    if (Mothers_2[i] != null)
                    {
                        MothersBlocks_2[i].Text = FillGeneration2(Mothers_2[i]);
                        grid2.Children.Add(MothersBlocks_2[i]);
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (Fathers_3[i] != null)
                    {
                        FathersBlocks_3[i].Text = FillGeneration3(Fathers_3[i]);
                        grid2.Children.Add(FathersBlocks_3[i]);
                    }

                    if (Mothers_3[i] != null)
                    {
                        MothersBlocks_3[i].Text = FillGeneration3(Mothers_3[i]);
                        grid2.Children.Add(MothersBlocks_3[i]);
                    }
                }

                for (int i = 0; i < 8; i++)
                {
                    if (Fathers_4[i] != null)
                    {
                        FathersBlocks_4[i].Text = FillGeneration4(Fathers_4[i]);
                        grid2.Children.Add(FathersBlocks_4[i]);
                    }

                    if (Mothers_4[i] != null)
                    {
                        MothersBlocks_4[i].Text = FillGeneration4(Mothers_4[i]);
                        grid2.Children.Add(MothersBlocks_4[i]);
                    }
                }

                page2.Children.Add(grid2);
                pc2.Child = page2;

                #endregion

                this.Printout.Pages.Add(pc1);
                this.Printout.Pages.Add(pc2);

            }
            catch (Exception x)
            {
                ShowError(x.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window browseWindow = new BrowseSpecimensWindow();
            browseWindow.Left = this.Left;
            browseWindow.Top = this.Top;
            browseWindow.Show();
            this.Close();
        }

        private void ShowError(string Message)
        {
            System.Windows.MessageBox.Show(Message, "Błąd", System.Windows.MessageBoxButton.OK);
        }

        private void FillPatents()
        {
            Father_1 = context.Dogs.Where(x => x.DogId == dog.FatherId).SingleOrDefault();
            Mother_1 = context.Dogs.Where(x => x.DogId == dog.MotherId).SingleOrDefault();
            Dog temp_dog = null;

            if (Father_1 != null)
            {
                Fathers_2[0] = context.Dogs.Where(x => x.DogId == Father_1.FatherId).SingleOrDefault();
                Mothers_2[0] = context.Dogs.Where(x => x.DogId == Father_1.MotherId).SingleOrDefault();
            }

            if (Mother_1 != null)
            {
                Fathers_2[1] = context.Dogs.Where(x => x.DogId == Mother_1.FatherId).SingleOrDefault();
                Mothers_2[1] = context.Dogs.Where(x => x.DogId == Mother_1.MotherId).SingleOrDefault();
            }

            for (int i = 0; i < 2; i++)
            {
                if (Fathers_2[i] != null)
                {
                    temp_dog = Fathers_2[i];
                    Fathers_3[i * 2] = context.Dogs.Where(x => x.DogId == temp_dog.FatherId).SingleOrDefault();
                    Mothers_3[i * 2] = context.Dogs.Where(x => x.DogId == temp_dog.MotherId).SingleOrDefault();
                }
            }

            for (int i = 0; i < 2; i++)
            {
                if (Mothers_2[i] != null)
                {
                    temp_dog = Mothers_2[i];
                    Fathers_3[(i * 2) + 1] = context.Dogs.Where(x => x.DogId == temp_dog.FatherId).SingleOrDefault();
                    Mothers_3[(i * 2) + 1] = context.Dogs.Where(x => x.DogId == temp_dog.MotherId).SingleOrDefault();
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (Fathers_3[i] != null)
                {
                    temp_dog = Fathers_3[i];
                    Fathers_4[i * 2] = context.Dogs.Where(x => x.DogId == temp_dog.FatherId).SingleOrDefault();
                    Mothers_4[i * 2] = context.Dogs.Where(x => x.DogId == temp_dog.MotherId).SingleOrDefault();
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (Mothers_3[i] != null)
                {
                    temp_dog = Mothers_3[i];
                    Fathers_4[(i * 2) + 1] = context.Dogs.Where(x => x.DogId == temp_dog.FatherId).SingleOrDefault();
                    Mothers_4[(i * 2) + 1] = context.Dogs.Where(x => x.DogId == temp_dog.MotherId).SingleOrDefault();
                }
            }
        }

        string FillGeneration1(Dog dog)
        {
            string block = "";

            block = dog.Name + " " + dog.Nickname + "\n";
            block += dog.PedegreeCertificateNumber + "\n";
            if (dog.Coat.Length != 0)
            {
                block += "Maść: " + dog.Coat + "\n";
            }

            if (dog.Dysplasia.Length != 0)
            {
                block += "Dysplazja: " + dog.Dysplasia + "\n";
            }

            if (dog.Training.Length != 0)
            {
                block += "Wyszkolenie: " + dog.Training + "\n";
            }

            if (dog.ExhibitionTitles.Length != 0)
            {
                block += "Tytuły: " + dog.ExhibitionTitles + "\n";
            }

            if (dog.DNA.Length != 0)
            {
                block += "DNA: " + dog.DNA;
            }

            return block;
        }

        string FillGeneration2(Dog dog)
        {
            string block = "";

            block = dog.Name + " " + dog.Nickname + "\n";
            block += dog.PedegreeCertificateNumber + "\n";
            if (dog.Coat.Length != 0)
            {
                block += "Maść: " + dog.Coat + "\n";
            }

            if (dog.Dysplasia.Length != 0)
            {
                block += "Dysplazja: " + dog.Dysplasia + "\n";
            }

            if (dog.Training.Length != 0)
            {
                block += "Wyszkolenie: " + dog.Training + "\n";
            }

            if (dog.ExhibitionTitles.Length != 0)
            {
                block += "Tytuły: " + dog.ExhibitionTitles + "\n";
            }

            if (dog.DNA.Length != 0)
            {
                block += "DNA: " + dog.DNA;
            }

            return block;
        }

        string FillGeneration3(Dog dog)
        {
            string block = "";

            block = dog.Name + " " + dog.Nickname + "\n";
            block += dog.PedegreeCertificateNumber;

            bool first_coma = false;
            bool second_coma = false;

            if (dog.DNA.Length != 0)
            {
                block += ", DNA: " + dog.DNA;
            }
            block += "\n";
            if (dog.Coat.Length != 0)
            {
                first_coma = true;
                block += "Maść: " + dog.Coat;
            }

            if (dog.Dysplasia.Length != 0)
            {
                if (first_coma)
                {
                    block += ", ";
                }
                block += ", Dysplazja: " + dog.Dysplasia;
            }

            block += "\n";

            if (dog.Training.Length != 0)
            {
                second_coma = true;
                block += "Wyszkolenie: " + dog.Training;
            }

            if (dog.ExhibitionTitles.Length != 0)
            {
                if (second_coma)
                {
                    block += ", ";
                }
                block += "Tytuły: " + dog.ExhibitionTitles;
            }
            return block;
        }

        string FillGeneration4(Dog dog)
        {
            string block = "";

            block = dog.Name + " " + dog.Nickname + ", ";
            block += dog.PedegreeCertificateNumber + "\n";
            bool first_coma = false;
            bool second_coma = false;
            if (dog.Coat.Length != 0)
            {
                block += "Maść: " + dog.Coat;
                first_coma = true;
            }

            if (dog.Dysplasia.Length != 0)
            {
                if (first_coma)
                {
                    block += ", ";
                }
                block += "Dysplazja: " + dog.Dysplasia;
                first_coma = true;
            }
            if (dog.DNA.Length != 0)
            {
                if (first_coma)
                {
                    block += ", ";
                }
                block += "DNA: " + dog.DNA;
            }                           
            block += "\n";

            if (dog.Training.Length != 0)
            {
                block += "Wyszkolenie: " + dog.Training;
                second_coma = true;
            }

            if (dog.ExhibitionTitles.Length != 0)
            {
                if (second_coma)
                {
                    block += ", ";
                }
                block += "Tytuły: " + dog.ExhibitionTitles;
            }

            return block;
        }
    }
}
