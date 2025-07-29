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
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace TestWPFIconTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<LocalImagePathAndInfo> LocalImagesList { get; set; }

        [DllImport("AvaloniaIconToolDLL.dll", CallingConvention =CallingConvention.StdCall)]
        private static extern int WriteIconFile(int argc, string[] args);

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
                    
                    //Step 1 : Re-do a console c++ command line arguments list
                    List<string> paramsList = new List<string>();

                    paramsList.Add(System.Environment.ProcessPath);
                    paramsList.Add("pack");
                    paramsList.Add(sfd.FileName);

                    foreach (var item in LocalImagesList)
                        paramsList.Add(item.ImagePath);

                    //Step 2 : Export using C++
                    int result = WriteIconFile(paramsList.Count, paramsList.ToArray());

                    MessageBox.Show("Export complete !");
                }
            }
        }
    }
}