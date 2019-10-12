using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Other
{
    public static class Validations
    {

        public static int ValidateSearchString(string inputString)
        {
            
            if (string.IsNullOrEmpty(inputString))
            {
                return  1;
            }
            if (inputString.Length > 50)
            {
                return 2;
            }
            if(HasOnlyDigits(inputString))
            {
                return 3;
            }
            if (HasOnlyDigitsAndSpace(inputString))
            {
                return 3;
            }
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
                if (character < '0' || character > '9')
                {
                    return false;
                }
                else if(Char.IsWhiteSpace(character))
                {
                    return false;
                }
                    
            }
            return true;
        }
    }
}
