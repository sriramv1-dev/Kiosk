namespace Kiosk.Helpers
{
    public static class ValidationHelper
    {
        public enum StringValidation
        {
            EmptyString = 1,
            StringLengthGreaterThanRequired = 2,
            HasOnlyDigits = 3,
            HasOnlyDigitsAndSpace =4
        }

        public static int ValidateSearchString(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))            
                return  1;            
            if (inputString.Length > 50)           
                return 2;            
            if(HasOnlyDigits(inputString))            
                return 3;            
            if (HasOnlyDigitsAndSpace(inputString))            
                return 4;
            
            return 0;
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
                if (char.IsWhiteSpace(character))
                {
                    return true;
                }
                if (character < '0' || character > '9')
                {
                    return false;
                }                      
            }
            return true;
        }
    }
}
