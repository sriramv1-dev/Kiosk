using static Kiosk.Helpers.Enumerations;

namespace Kiosk.Helpers
{
    public static class ValidationHelper
    {

        public static StringValidationMessage ValidateSearchString(string inputString)
        {

            if (string.IsNullOrEmpty(inputString.Trim()))
                return StringValidationMessage.StringIsEmpty;
            if (inputString.Trim().Length > 50)
                return StringValidationMessage.StringLengthGreaterThanLimit;
            if (HasOnlyDigits(inputString))
                return StringValidationMessage.StringHasOnlyDigits;
            if (HasOnlyDigitsAndSpace(inputString))
                return StringValidationMessage.StringHasDigitsAndSpaces;

            return StringValidationMessage.StringIsGood;
        }

        public static bool HasOnlyDigits(string inputString)
        {            
            foreach (char character in inputString)
            {
                if (character < '0' || character > '9')
                    return false;
            }
            return true;
        }

        public static bool HasOnlyDigitsAndSpace(string inputString)
        {
            foreach (char character in inputString)
            {
                if (character < '0' || character > '9')
                {
                    if(char.IsWhiteSpace(character))
                    {

                    }
                    else
                    {
                        return false;
                    }                    
                }                    
            }
            return true;
        }
    }
}
