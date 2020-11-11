using MySimpleUtilities.utilities.factory;
using System;
using System.IO;

namespace MySimpleUtilities.utilities
{
    class FolderReorganizer : AbstractUtility, IUtility
    {
        private readonly string desktopPath;
        private readonly string[] desktopFilePaths;



        public FolderReorganizer()
        {
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktopFilePaths = Directory.GetFiles(desktopPath);
        }

        // TODO:Implement an overload constructor where the user can pass the chosen folder path

        /// <summary>
        /// Reorganizes your current Desktop by creating a new folder (If it doesn't already exists) for every 
        /// different extension type that it finds. Then it moves the according files into the correct named foder.
        /// </summary>
        public void Start()
        {
            string currentFileName;
            string currentFileExtension;
            string destinationFolderPath;
            string destinationFilePath;

            foreach (string filePath in desktopFilePaths)
            {
                try
                {
                    currentFileName = Path.GetFileNameWithoutExtension(filePath);
                    currentFileExtension = Path.GetExtension(filePath);
                    destinationFolderPath = Path.Combine(desktopPath, SetFirstLetterToUpper(currentFileExtension));

                    if (!Directory.Exists(destinationFolderPath))
                    {
                        Directory.CreateDirectory(destinationFolderPath);
                    }

                    destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(filePath));

                    File.Move(filePath, destinationFilePath);
                    HelperComponent.PrintColouredMessage("Moving file to: " + destinationFilePath, ConsoleColor.Yellow);
                }
                catch (IOException e)
                {
                    HelperComponent.PrintColouredMessage("IO Error: " + e.Message, ConsoleColor.Red);
                }
                catch (Exception e)
                {
                    HelperComponent.PrintColouredMessage("General Error: " + e.Message, ConsoleColor.Red);
                }
            }

            Stop();
        }

        /// <summary>
        /// Prints default stop utility messages
        /// </summary>
        public void Stop()
        {
            stopNotification();
        }

        /// <summary>
        /// Takes a string as parameter and returns it with the first capital letter
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private string SetFirstLetterToUpper(string word)
        {
            try
            {
                return word = char.ToUpper(word[1]) + word.Substring(2);
            }
            catch (Exception e)
            {
                HelperComponent.PrintColouredMessage(e.Message, ConsoleColor.Red);
            }

            return word;
        }
    }
}
