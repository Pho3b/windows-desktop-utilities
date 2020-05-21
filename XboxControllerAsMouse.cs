using System.Windows.Forms;
using System.Drawing;
using System;
using SharpDX.XInput;
using System.Runtime.InteropServices;

namespace MySimpleUtilities
{
    class XboxControllerAsMouse
    {
        // Auto Format code Ctrl + K - Ctrl + F
        public bool isRunning = false;
        private int screenWidth = Screen.GetBounds(new Point(0, 0)).Width;
        private int screenHeight = Screen.GetBounds(new Point(0, 0)).Height;
        private Controller controller;
        private State controllerState;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        private bool buttonPressed = false;


        /*
         * Constructor
         */
        public XboxControllerAsMouse()
        {
            Point screenCoordinates = new Point(screenWidth, screenHeight);
            Cursor.Clip = new Rectangle(new Point(0, 0), new Size(screenCoordinates));  // TODO: check why it doesn't work on application resume.
        }

        /*
         * This is the controller main cycle.
         */
        public void Start()
        {
            controller = new Controller(UserIndex.One);

            while (isRunning)
            {
                Update();
                System.Threading.Thread.Sleep(10);
            }
        }

        /*
        * Body of the update method, here we listen for controller input and execute actions
        * according to the detected event.
        */
        private void Update()
        {
            controller.GetState(out controllerState);

            LeftThumbXAction();
            RightThumbXAction();
            ButtonActions();
        }

        /*
         * Buttons action handling
         */
        private void ButtonActions()
        {
            if (buttonPressed == false)
            {
                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.A)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }
                else
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }

                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.B)
                {
                    Console.WriteLine(controllerState.Gamepad.Buttons);
                }

                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.Back)
                {
                    Stop();
                }

                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.Start)
                {
                    Program.main.PrintUtilitiesList();
                }

                buttonPressed = true;
            }

            if (controllerState.Gamepad.Buttons == GamepadButtonFlags.None && buttonPressed)
            {
                if (buttonPressed)
                    buttonPressed = false;
            }
        }

        /*
         * Handling the Left Thumb actions
         */
        private void LeftThumbXAction()
        {
            short x = controllerState.Gamepad.LeftThumbX;
            short y = controllerState.Gamepad.LeftThumbY;
            double magnitude = Math.Sqrt(x * x + y * y);

            if (magnitude > 7000)
            {
                Cursor.Position = new Point(Cursor.Position.X + x / 3000, Cursor.Position.Y - y / 3000);
            }
        }

        /*
         * Handling the Right Thumb actions
         */
        private void RightThumbXAction()
        {
            short x = controllerState.Gamepad.RightThumbX;
            short y = controllerState.Gamepad.RightThumbY;
            double magnitude = Math.Sqrt(x * x + y * y);

            if (magnitude > 7000)
            {
                if (y > 0)
                {
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 25, 0);
                }
                else
                {
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -25, 0);
                }
            }
        }

        /*
         * Stops the current utility 
         */
        public void Stop()
        {
            isRunning = false;
            Console.WriteLine("Stopped application {0}", Program.UTITILIES_LIST[0]);
            Program.main.StartProgramHub();
        }

        /*
         * Imported mouse_event Method implementation from user32.dll
         */
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}
