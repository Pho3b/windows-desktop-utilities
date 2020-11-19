using System;

namespace MySimpleUtilities.utilities.factory
{
    abstract class AbstractUtility
    {
        /// <summary>
        /// Returns the name of the current utility as string
        /// </summary>
        /// <returns>
        /// The name of the class
        /// </returns>
        protected string getName()
        {
            return GetType().Name;
        }

        /// <summary>
        /// Prints a console formatted message that notifies the utility launch
        /// if "withBaloon" is true it lanches also a Windows system notification
        /// </summary>
        /// <param name="withBaloon"></param>
        protected void startNotification(bool withBaloon = false)
        {
            HelperComponent.PrintColouredMessage("Launched utility " + getName(), ConsoleColor.DarkGreen);

            if (withBaloon)
            {
                HelperComponent.ShowBalloon(getName(), "Started");
            }
        }

        /// <summary>
        /// Prints a console formatted message that notifies the utility launch
        /// if "withBaloon" is true it lanches also a Windows system notification
        /// </summary>
        /// <param name="withBaloon"></param>
        // TODO: Implement decorator pattern on this class to make it print 'Execution completed'
        protected void stopNotification(bool withBaloon = false)
        {
            HelperComponent.PrintColouredMessage("Concluded application " + getName(), ConsoleColor.DarkRed);

            if (withBaloon)
            {
                HelperComponent.ShowBalloon(getName(), "Stopped");
            }
        }
    }
}
