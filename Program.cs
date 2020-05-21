using System;


namespace MySimpleUtilities
{
    class Program
    {
        private XboxControllerAsMouse xboxControllerAsMouse = new XboxControllerAsMouse();
        public static bool isUtilityRunning = false;
        public static Program main;
        public static readonly string[] UTITILIES_LIST = { "XboxControllerAsMouse", "RearrangeDownloadFolder" };
        public static readonly string[] COMMANDS_LIST =
        {
            "msu ls  -  Shows the list of the utilities",
            "msu start-utility {utitily index}",
            "msu help",
            "msu quit",
            "clear"
        };

        /*
         * Program Entry Point
         */
        static void Main(string[] args)
        {
            main = new Program();
            main.StartProgramHub();
        }

        /*
         * Constructor
         */
        public Program()
        {
            Console.WriteLine("Welcome to MSU App. Type 'msu ls' to show the current utilities list, type 'msu quit' or ':q' to quit the application");
            Console.WriteLine("\n");
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
                    case "msu start-utility 0":
                        Console.WriteLine("Launched application {0}", UTITILIES_LIST[0]);
                        xboxControllerAsMouse.isRunning = true;
                        xboxControllerAsMouse.Start();
                        break;
                    case "msu start-utility":
                        Console.WriteLine(" !Type the index of the utility that you want to start");
                        break;
                    case "msu start-utility 1":
                        Console.WriteLine("Launched application {0}", UTITILIES_LIST[1]);
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
                        Console.WriteLine("Command not found, type 'msu -help' to show the commands list");
                        break;
                }
            }
            while ((userInput != ":q" && userInput != "quit") || isUtilityRunning);
        }

        /*
         * Prints the current list of implemented utilities
         */
        public void PrintUtilitiesList()
        {
            Console.WriteLine("\n");

            for (short i = 0; i < UTITILIES_LIST.Length; i++)
            {
                Console.WriteLine("{0} = {1}", i, UTITILIES_LIST[i]);
            }

            Console.WriteLine("\n");
        }

        /*
         * Prints the commands list
         */
        public void PrintCommandsList()
        {
            Console.WriteLine("\n");

            foreach (string command in COMMANDS_LIST)
            {
                Console.WriteLine(command);
            }

            Console.WriteLine("\n");
        }
    }
}
