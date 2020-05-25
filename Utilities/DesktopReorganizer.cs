using System;
using System.IO;

namespace MySimpleUtilities.Utilities
{
    class DesktopReorganizer
    {
        public DesktopReorganizer()
        {
        }

        public void Test()
        {
            string newFolder = "abcd1234";

            string path = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
               newFolder
            );

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("General Error: " + e.Message);
                }
            } else
            {
                Directory.Delete(path);
            }
        }

    }
}
