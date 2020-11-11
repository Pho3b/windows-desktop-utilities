
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
        public string getName()
        {
            return GetType().Name;
        }
    }
}
