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

using System.IO;

namespace FileMove
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string directoryPath = tb1.Text;
            List<string> fileNames = GetFileNames(directoryPath);
            
            new Task(() =>
            {
                int counta = 0;
                foreach (string fileName in fileNames)
                {
                    string[] strings = fileName.Split("-").ToArray();
                    CreatFolder(directoryPath + $@"\{strings[0]}");
                    CreatFolder(directoryPath + $@"\{strings[0]}\{strings[1]}");
                    File.Copy(directoryPath + $@"/{fileName}", directoryPath + @$"\{strings[0]}\{strings[1]}\{fileName}");
                    counta++;
                    if (counta % 10 == 0)
                    {
                        Dispatcher.Invoke(new Action(() => { label.Content = counta.ToString(); }));
                        
                        
                    }
                }

            }).Start();
            
        }

        static List<string> GetFileNames(string directoryPath)
        {
            List<string> fileNames = new List<string>();
            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath);
                foreach (string file in files)
                {
                    fileNames.Add(Path.GetFileName(file));
                }
            }
            return fileNames;
        }

        static void CreatFolder(string folderPath)
        {

            
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("文件夹已创建： " + folderPath);
            }
            else
            {
                Console.WriteLine("文件夹已存在： " + folderPath);
            }
        }
    }
}
