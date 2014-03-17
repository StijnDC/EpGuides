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
using System.IO;
using System.Net;
using System.Data;



namespace EpGuides
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Program> _Programs;

        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            checkFile();
            
            OpvullenListBox();

        }
        private void OpvullenListBox()
        {
            lstPrograms.Items.Clear();
            foreach (Program item in _Programs)
            {

                lstPrograms.Items.Add(item);
               
            }
        }

        private void checkFile()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory + "Epguides.csv";

            if (File.Exists(dir))
            {

                OpvullenPrograms();
            }

            else
            {

                MakeFileList();

            }
        }

        private void UpdateFile()
        {
            try
            {
                WebClient Client = new WebClient();
                Client.DownloadFile("http://epguides.com/common/allshows.txt", @AppDomain.CurrentDomain.BaseDirectory + "Epguides2.csv");

                string tempdir = AppDomain.CurrentDomain.BaseDirectory + "EpguidesTemp.csv";
                string dir = AppDomain.CurrentDomain.BaseDirectory + "Epguides.csv";
                string dir2 = AppDomain.CurrentDomain.BaseDirectory + "Epguides2.csv";
                if (FileCompare(dir, dir2))
                {

                    File.Delete(System.Environment.CurrentDirectory + "/Epguides2.csv");
                }

                else
                {

                    Console.WriteLine("update in progress");


                    if (File.Exists(dir) && File.Exists(tempdir) && File.Exists(dir2))
                    {
                        File.Move(dir, tempdir);
                        File.Move(dir2, dir);

                    }


                }
                File.Delete(System.Environment.CurrentDirectory + "/EpguidesTemp.csv");
            }

            catch
            {
                Console.WriteLine("geen internet");
            }
        }

        private void OpvullenPrograms()
        {
            _Programs = Program.InlezenCSV(AppDomain.CurrentDomain.BaseDirectory + "Epguides.csv");
          
        }

        private void MakeFileList()
        {
            try
            {
                WebClient Client = new WebClient();
                Client.DownloadFile("http://epguides.com/common/allshows.txt", @AppDomain.CurrentDomain.BaseDirectory + "Epguides.csv");
                checkFile();
            }

            catch
            {
                MessageBox.Show("je hebt hiervoor internet nodig");
            }
        }
        public bool FileCompare(string file1, String file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;
            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);

        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstPrograms.Items.Clear();

            if (!string.IsNullOrEmpty(txtInput.Text))
            {

                foreach (Program item in _Programs)
                {

                    //now case insensitive
                    if (item.Title.IndexOf(txtInput.Text, StringComparison.OrdinalIgnoreCase) >= 0)

                    //  if (item.Title.Contains(txtInput.Text))
                    {

                        lstPrograms.Items.Add(item);
                    }
                }
            }
        }

     //   private void lstPrograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
     //   {
     ////   Program p =    Program.GetProgramByName(lstPrograms.SelectedItem);

     //       Program p = lstPrograms.SelectedItem as Program;
     //       //this works!
     //       lblCountry.Content = p.Country;
     //       lblDirectory.Content = p.Directory;
     //       lblEnd.Content = p.EndDate;
     //       lblEpnum.Content = p.NumberOfEpisodes;
     //       LblNetwork.Content = p.Network;
     //       lblRage.Content = p.TvRage;
     //       lblRunTime.Content = p.RunTime;
     //       lblStart.Content = p.StartDate;
           


     //   }



    }
}

