using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace Technical_Assignment
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

        //This method will open a new dialog when the create button click. 
        private void Create_Project_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if(response == true)
            {
                string filename = openFileDialog.FileName;

                foreach (string FileName in openFileDialog.FileNames)
                    tbxFiles.Text = FileName;        
            }
                    
        }

        // This method will list the files from the most recently added to the last added date. 
        private void Recent_Project_Button_Click_1(object sender, RoutedEventArgs e)
        {
            var files = FileManager.LoadFiles().OrderBy(f => f.LastWriteTime ).ToList();
            LSFiles.ItemsSource = files;
        }


        //This mothod will open the project window. 
        private void Open_Project_Click_2(object sender, RoutedEventArgs e)
        {
            var projectWindow = new ProjectWindow();
            projectWindow.Show();

            //The code below clears the list view in the main window.
            LSFiles.ItemsSource = new List<FileInfo>();

        }

        /*
           This method creates the storage folder if it doesn't exist. 
           If it doesn't exist, it will create a storage folder, then copies the uploaded file to the storage folder. 
        */
        private void Save_Button__Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(FileManager.FolderPath); //creates directory if it doesnt already exists

            FileInfo fileInfo = new FileInfo(tbxFiles.Text); //Creates a FileInfo object that will be used to copy the file to the storage folder

            //copy the file to storage
            fileInfo.CopyTo(System.IO.Path.Combine(FileManager.FolderPath, fileInfo.Name), true); 
            var files = new List<FileInfo>();
            files.Add(FileManager.LoadFiles().FirstOrDefault(x => x.Name == fileInfo.Name));
            LSFiles.ItemsSource = files;
        }

 
        //This method gets the currently selected file and then tries to load the  file's preview image.
        private void LSFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checks if theres atleast one file selected
            if (e.AddedItems.Count == 0) return;

            //this taked the first selected file and tries to cast it into a FileInfo object
            var fileInfo = (FileInfo) e.AddedItems[0];

            preview.Source = FileManager.LoadPreviewImage(fileInfo.Extension, fileInfo.ToString());
        }
    }
    }
    
        
    

