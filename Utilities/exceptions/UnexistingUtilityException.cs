
namespace MySimpleUtilities.utilities.exceptions
{
    class UnexistingUtilityException : System.Exception
    {
        public UnexistingUtilityException() : base()
        {

        }

        public UnexistingUtilityException(string utilityName) 
            : base(string.Format("Invalid utility Name: {0}", utilityName))
        {

        }
    }
}
