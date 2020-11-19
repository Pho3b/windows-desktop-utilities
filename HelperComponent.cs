using System;
using System.Drawing;
using System.Windows.Forms;

namespace MySimpleUtilities.utilities
{
    class HelperComponent
    {
        public static readonly string[] UTITILIES_LIST = { "XboxControllerAsMouse", "FolderReorganizer" };
        public enum utilities { XboxControllerAsMouse, FolderReorganizer };
        public static readonly string[] COMMANDS_LIST =
        {
            "msu -l  :  Shows the list of the utilities and their indexes",
            "msu {utitily index}",
            "msu -h",
            "msu quit or :q",
            "clear"
        };



        /// <summary>
        /// Prints the current list of implemented utilities
        /// </summary>
        public static void PrintUtilitiesList()
        {
            Console.WriteLine("\n");

            for (short i = 0; i < UTITILIES_LIST.Length; i++)
            {
                Console.WriteLine("{0} = {1}", i, UTITILIES_LIST[i]);
            }

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Prints the commands list
        /// </summary>
        public static void PrintCommandsList()
        {
            Console.WriteLine("\n");

            foreach (string command in COMMANDS_LIST)
            {
                Console.WriteLine(command);
            }

            Console.WriteLine("\n");
        }

        /// <summary>
        /// Prints a console message in the choosen color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        /// <param name="newLine"></param>
        public static void PrintColouredMessage(string message, ConsoleColor color, bool newLine = true)
        {
            string nLine = newLine ? "\n" : "";

            Console.ForegroundColor = color;
            Console.WriteLine(nLine + message);
            Console.ResetColor();
        }

        /// <summary>
        /// Show a Windows balloon notification
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="body"></param>
        public static void ShowBalloon(string title, string body, int time = 5000)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;

            if (title != null && body != null)
            {
                notifyIcon.BalloonTipTitle = title;
                notifyIcon.BalloonTipText = body;
            }

            notifyIcon.ShowBalloonTip(time);
            notifyIcon.Dispose();
        }
    }
}
