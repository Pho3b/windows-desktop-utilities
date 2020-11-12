using MySimpleUtilities.utilities;
using System;

namespace MySimpleUtilities
{
    class Program
    {
        private readonly UtilityFactory utilityFactory;
        public static bool isUtilityRunning = false;
        public static Program main;



        static void Main(string[] args)
        {
            main = new Program();
            main.StartProgramHub();
        }

        public Program()
        {
            HelperComponent.PrintColouredMessage("Welcome to MSU App!\nType 'msu help' to show the commands list, 'msu start {0}' " +
                "to start a utility or 'msu quit' to quit the app\n", ConsoleColor.Yellow, false);
            utilityFactory = new UtilityFactory();
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
                        HelperComponent.PrintUtilitiesList();
                        break;
                    case "msu start":
                        HelperComponent.PrintColouredMessage("Type the index of the utility that you want to start", ConsoleColor.White);
                        break;
                    case "msu start 0":
                        utilityFactory.createUtility("XboxControllerAsMouse").Start();
                        break;
                    case "msu start 1":
                        utilityFactory.createUtility("XboxControllerAsMouse").Start();
                        break;
                    case "msu -h":
                        HelperComponent.PrintCommandsList();
                        break;
                    case "clear":
                        Console.Clear();
                        break;
                    case "":
                        break;
                    default:
                        HelperComponent.PrintColouredMessage("Command not found, type 'msu -h' to show the commands list", ConsoleColor.White);
                        break;
                }
            }
            while ((userInput != ":q" && userInput != "msu quit") || isUtilityRunning);
        }
    }
}
