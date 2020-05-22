using System;
using System.Drawing;
using System.Windows.Forms;
using MySimpleUtilities.Utilities;

namespace MySimpleUtilities
{
    class Program
    {
        private readonly XboxControllerAsMouse xboxControllerAsMouse = new XboxControllerAsMouse();
        private readonly DesktopReorganizer folderReorganizer = new DesktopReorganizer();
        public static bool isUtilityRunning = false;
        public static Program main;
        public static readonly string[] UTITILIES_LIST = { "XboxControllerAsMouse", "FolderReorganizer" };
        public static readonly string[] COMMANDS_LIST =
        {
            "msu ls  -  Shows the list of the utilities",
            "msu start {utitily index}",
            "msu help",
            "msu quit",
            "clear"
        };



        static void Main(string[] args)
        {
            main = new Program();
            main.StartProgramHub();
        }


        public Program()
        {
            PrintColouredMessage("Welcome to MSU App!\nType 'msu help' to show the commands list, 'msu start {0} to start a utility or 'msu quit' to quit the app\n", ConsoleColor.DarkCyan, false);
        }

        public void StartProgramHub()
        {
            string userInput;

            do
            {
                userInput = Console.ReadLine();

                switch (userInput.ToLower().Trim())
                {
                    case "msu ls":
                        PrintUtilitiesList();
                        break;
                    case "msu start":
                        PrintColouredMessage("Type the index of the utility that you want to start", ConsoleColor.White);
                        break;
                    case "msu start 0":
                        PrintColouredMessage("Launched utility " + UTITILIES_LIST[0], ConsoleColor.DarkGreen);
                        ShowBalloon(Program.UTITILIES_LIST[0], "Started");
                        xboxControllerAsMouse.Start();
                        break;
                    case "msu start 1":
                        PrintColouredMessage("Launched utility " + UTITILIES_LIST[1], ConsoleColor.DarkGreen);
                        folderReorganizer.Test();
                        break;
                    case "msu help":
                        PrintCommandsList();
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "":
                        break;
                    default:
                        PrintColouredMessage("Command not found, type 'msu -help' to show the commands list", ConsoleColor.White);
                        break;
                }
            }
            while ((userInput != ":q" && userInput != "quit") || isUtilityRunning);
        }

        /// <summary>
        /// Prints the current list of implemented utilities
        /// </summary>
        public void PrintUtilitiesList()
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
        public void PrintCommandsList()
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
