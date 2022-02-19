using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Technical_Assignment
{
    internal class FileManager
    {

        /// <summary>
        /// This is the application storage path, this is where all the files are stored. 
        /// </summary>
        public static string FolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Technical");

        /// <summary>
        /// This loads all the files from the storage folder.
        /// </summary>
        /// <returns> This reurn a list of files info instances. </returns>
        public static List<FileInfo> LoadFiles()
        {
            var path = FolderPath;
            var files = Directory.GetFiles(path);
            var FilesInfos = new List<FileInfo>();
            foreach (var file in files)
            {
                FilesInfos.Add(new FileInfo(file));

            }
            return FilesInfos;
        }


        /// <summary>
        /// It checks if the files ca be preview base on its extension. 
        /// If it cannot be preview, it checks if there will be a default image to preview.
        /// Else the returns will be null.
        /// </summary>
        /// <param name="extension"> This is the files extension. e.g .mp3.</param>
        /// <param name="path"> This is the path to the file. </param>
        /// <returns> The return type is going to be an image source instance or null. </returns>
        public static ImageSource LoadPreviewImage(string extension, string path)
        {
            switch (extension)
            {
                case ".jpg":
                case ".png":
                case ".jpeg":
                    return BitmapFromUri(path);
                    break;
                case ".pdf":
                    return BitmapFromUri("pack://application:,,,/Resources/pdf.png");
                    break;
                case ".txt":
                    return BitmapFromUri("pack://application:,,,/Resources/text.png");
                    break;
                case ".docx":
                    return BitmapFromUri("pack://application:,,,/Resources/docx.png");
                default:
                    return null;
                    break;
            }
        }
        /// <summary>
        ///     This function Creates and returns a new ImageSource. This was coded by Rita 
        /// </summary>
        /// <param name="path"> The path to the image</param>
        /// <returns> Returns an instance of imageSource</returns>
        public static ImageSource BitmapFromUri(string path)
        {
            var source = new Uri(path);
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

    }
}
