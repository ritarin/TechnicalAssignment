using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
namespace Technical_Assignment
{
    /// <summary>
    /// Interaction logic for OpenProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        private string selectedFilePath = string.Empty;
        public ProjectWindow()
        {
            InitializeComponent();
            Display();
        }


        /// <summary>
        /// Loads the fils to the list view
        /// </summary>
        private void Display()
        {
           
            LSFiles.ItemsSource = FileManager.LoadFiles();

        }
        //This method gets the currently selected file and then tries to load the  file's preview image.
        private void LSFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checks if theres atleast one file selected
            if (e.AddedItems.Count == 0) return;

            var fileInfo = (FileInfo)e.AddedItems[0];
            if (fileInfo == null) return;

            //Set the the selcted file's path to the class scoped property
            selectedFilePath = fileInfo.ToString();

            //Enable delete button.
            btnDelete.IsEnabled = true;

            //Set the preview image.
            preview.Source = FileManager.LoadPreviewImage(fileInfo.Extension, fileInfo.ToString());

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Checks if selected File path is not empty
            if (selectedFilePath == string.Empty) return;

            //Create a new file info instance based on the path
            var fileInfo = new FileInfo(selectedFilePath);

            //Deletes the currently selected file
            fileInfo.Delete();

            //Clears the preview image

            preview.Source = null;

            //disables the delete button again
            btnDelete.IsEnabled = false;

            Thread.Sleep(1000);

            //Loads the fils to the list view
            Display();

            //Clears the selected Items list
            LSFiles.SelectedItems.Clear();
        }

    
    }
}
