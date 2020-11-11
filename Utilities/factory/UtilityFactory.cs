using MySimpleUtilities.utilities.exceptions;
using MySimpleUtilities.utilities.factory;
using System;

namespace MySimpleUtilities.utilities
{
    class UtilityFactory
    {
        /// <summary>
        /// Factory method to generate Utilities
        /// </summary>
        /// <param name="utilityType"></param>
        /// <returns>
        /// IUtility
        /// </returns>
        public IUtility createUtility(string utilityType)
        {
            switch (utilityType)
            {
                case "XboxControllerAsMouse":
                    return new XboxControllerAsMouse();
                case "FolderReorganizer":
                    return new FolderReorganizer();
                default:
                    throw new UnexistingUtilityException(utilityType);
            }
        }
    }
}
