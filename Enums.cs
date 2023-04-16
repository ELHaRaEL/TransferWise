using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferWise
{
    internal class Enums
    {

        internal enum TypeOfAccount
        {
            STANDARD,
            SAVINGS
        }
        internal enum TypeOfRequestStatement
        {
            COMPACT,
            FLAT
        }
        internal enum TypeOfGenerateStatement
        {
            json,
            csv,
            pdf
        }
    }
}
