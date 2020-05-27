using System;
using System.IO;

namespace MySimpleUtilities.Utilities
{
    class DesktopReorganizer
    {
        private readonly string desktopPath;
        private readonly string[] desktopFilePaths;



        public DesktopReorganizer()
        {
            desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktopFilePaths = Directory.GetFiles(desktopPath);
        }

        /// <summary>
        /// Reorganizes your current Desktop by creating a new folder (If it doesn't already exists) for every 
        /// different extension type that it finds. Then it moves the according files into the correct named foder.
        /// </summary>
        public void Reorganize()
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
                    Program.PrintColouredMessage("Moving file to: " + destinationFilePath, ConsoleColor.Yellow);
                }
                catch (IOException e)
                {
                    Program.PrintColouredMessage("IO Error: " + e.Message, ConsoleColor.Red);
                }
                catch (Exception e)
                {
                    Program.PrintColouredMessage("General Error: " + e.Message, ConsoleColor.Red);
                }
            }

            Program.ShowBalloon(Program.UTITILIES_LIST[1], "Execution complete");
            Program.PrintColouredMessage("Execution of " + Program.UTITILIES_LIST[1] + " completed", ConsoleColor.DarkGreen);
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
                return word = "_" + char.ToUpper(word[1]) + word.Substring(2);
            }
            catch (Exception e)
            {
                Program.PrintColouredMessage(e.Message, ConsoleColor.Red);
            }

            return word;
        }
    }
}
