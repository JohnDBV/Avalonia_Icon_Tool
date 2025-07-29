using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestWPFIconTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<LocalImagePathAndInfo> LocalImagesList { get; set; }

        public MainWindow()
        {
            DataContext = this;

            LocalImagesList = new ObservableCollection<LocalImagePathAndInfo>();

            //LocalImagesList.Add(new LocalImagePathAndInfo("test", "bmp"));
            //LocalImagesList.Add(new LocalImagePathAndInfo("JustAVeryLongStringToBeHereForTestingPurposes_JustAVeryLongStringToBeHereForTestingPurposes_JustAVeryLongStringToBeHereForTestingPurposes", "png"));

            InitializeComponent();

            listView.ItemsSource = LocalImagesList;
        }

        private void btnAddPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.bmp;*.png";
            ofd.Multiselect = false;

            bool? ok = ofd.ShowDialog();

            if (ok != null)//because of the hellish nullable types
            {
                if(((bool)ok))//make sure we press the OK button, not Cancel or X
                    LocalImagesList.Add(new LocalImagePathAndInfo(ofd.FileName,
                        ofd.FileName.Substring(ofd.FileName.Length - 3)));
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (LocalImagesList.Count == 0)
            {
                MessageBox.Show("Add some pictures to export, first !!!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Icon Files|*.ico";

            bool? ok = sfd.ShowDialog();

            if (ok != null)//because of the hellish nullable types
            {
                if ((bool)ok)//make sure we press the OK button, not Cancel or X
                {
                    MessageBox.Show("Export(C++ side)!\nOutput file :\n" + sfd.FileName);

                    //todo : Call C++ code

                    MessageBox.Show("Export complete !");
                }
            }
        }
    }
}