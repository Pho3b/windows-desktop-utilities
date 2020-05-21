using System.Windows.Forms;
using System.Drawing;
using System;
using SharpDX.XInput;
using System.Runtime.InteropServices;

namespace MySimpleUtilities
{
    class XboxControllerAsMouse
    {
        private readonly Controller controller;
        private State controllerState;
        public bool isRunning = false;
        private bool isPaused = false;
        private bool buttonPressed = false;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_WHEEL = 0x0800;




        public XboxControllerAsMouse()
        {
            controller = new Controller(UserIndex.One);
        }

        /// <summary>
        /// This is the controller main cycle
        /// </summary>
        public void Start()
        {
            try
            {
                isRunning = CheckControllerConnection();

                while (isRunning)
                {
                    Update();
                    System.Threading.Thread.Sleep(10);
                }
            }
            catch (SharpDX.SharpDXException e)
            {
                Program.PrintColouredMessage(e.Message, ConsoleColor.DarkRed);
                isRunning = false;
            }
        }

        /// <summary>
        /// Body of the update method, here we listen for controller input and execute actions
        /// according to the detected event.
        /// </summary>
        private void Update()
        {
            controller.GetState(out controllerState);

            LeftThumbXAction();
            RightThumbXAction();
            ButtonActions();
        }

        /// <summary>
        /// Handling buttons action
        /// </summary>
        private void ButtonActions()
        {
            if (controllerState.Gamepad.Buttons != GamepadButtonFlags.None && !buttonPressed)
            {
                buttonPressed = true;

                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.A)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    System.Threading.Thread.Sleep(10);
                    mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }

                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.B)
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                    System.Threading.Thread.Sleep(10);
                    mouse_event(MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                }

                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.Back)
                {
                    Stop();
                }

                if (controllerState.Gamepad.Buttons == GamepadButtonFlags.Start)
                {
                    Pause();
                }
            }

            if (controllerState.Gamepad.Buttons == GamepadButtonFlags.None && buttonPressed)
            {
                if (buttonPressed)
                    buttonPressed = false;
            }
        }

        /// <summary>
        /// Handling the Left Thumb actions
        /// </summary>
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

        /// <summary>
        /// Handling the Right Thumb actions
        /// </summary>
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

        private void StartBackgroundListener()
        {
            Program.PrintColouredMessage("...now listenint in background, hold (A + B + X + Y) to resume the utility", ConsoleColor.Yellow, false);

            while(isPaused)
            {
                controller.GetState(out controllerState);

                if (controllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A) &&
                    controllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B) &&
                    controllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X) &&
                    controllerState.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y))
                {
                    Resume();
                }

                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Pause()
        {
            isPaused = true;
            isRunning = false;
            Program.PrintColouredMessage("Paused application " + Program.UTITILIES_LIST[0], ConsoleColor.DarkYellow);
            StartBackgroundListener();
        }

        /// <summary>
        /// TODO: Find a good buttons combination to bind to this method.
        /// Hint: All 4 buttons pressed toghether
        /// Implement a BackgroundUpdate method that listens only to this combination with a very long threading time
        /// </summary>
        private void Resume()
        {
            isPaused = false;
            isRunning = true;
            Program.PrintColouredMessage("Resumed application " + Program.UTITILIES_LIST[0], ConsoleColor.DarkYellow);
            Start();
        }

        /// <summary>
        /// Stops the current utility 
        /// </summary>
        public void Stop()
        {
            isRunning = false;
            Program.PrintColouredMessage("Stopped application " + Program.UTITILIES_LIST[0], ConsoleColor.DarkRed);
        }

        /// <summary>
        /// Whether the software finds a connected XBOX controller or not
        /// </summary>
        /// <returns>
        /// True if it finds a connected controller
        /// </returns>
        private bool CheckControllerConnection()
        {
            if (controller.IsConnected)
            {
                return true;
            }

            Program.PrintColouredMessage("Controller not found, please connect one", ConsoleColor.DarkRed);
            return false;
        }

        /// <summary>
        ///  Imported mouse_event Method implementation from user32.dll
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
    }
}
