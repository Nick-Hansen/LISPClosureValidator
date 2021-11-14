using System;
using System.Linq;

namespace LISPClosureValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool LISPClosureValid = false;
            string codeString = string.Empty;
            if (args.Length > 0) {
                codeString = args[0];
            }
            else {
                Console.WriteLine("Please enter your LISP code:");
                codeString = Console.ReadLine();
            }
            
            //convert code to parenthesis array
            //remove all characters which are not left parenthesis or right parenthesis
            var codeArray = Util.ConvertCodeStringToCharArray(codeString);

            //calculate whether all parenthesis are closed
            LISPClosureValid = Util.ClosureValid(codeArray);

            //return value of calculation
            Console.WriteLine(LISPClosureValid);
        }
    }
}
