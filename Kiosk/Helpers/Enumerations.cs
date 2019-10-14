using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiosk.Helpers
{
    public class Enumerations
    {
        public enum StringValidationMessage
        {
           StringIsGood = 0,
           StringIsEmpty = 1,
           StringLengthGreaterThanLimit = 2,
           StringHasOnlyDigits = 3,
           StringHasDigitsAndSpaces = 4

        }
    }
}
