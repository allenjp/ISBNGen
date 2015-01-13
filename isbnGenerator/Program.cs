using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace isbnGenerator
{
    class Program
    {
        /*
         * This function takes a string representing an isbn number
         * and determines if the given number is a valid isbn
         */
        static bool isValidISBN(string isbn)
        {
            // remove all non-numeric characters from given string
            string isbnNums = Regex.Replace(isbn, "[^.0-9]", "");

            // if there aren't 10 numbers, return false
            if (isbnNums.Length != 10)
            {
                return false;
            }

            int result = 0;

            for (int i = 0; i < isbnNums.Length; i++)
            {
                // convert current iteration of isbnString to an int
                int numVal = -1;
                numVal = (int)Char.GetNumericValue(isbnNums[i]);

                result = result + numVal * (10 - i);

            }
            // return whether result divided by 11 gives a remainder
            // if there is no remainder, the number is valid
            return ((result % 11) == 0);
        }

        /*
         * This function will generate a random valid isbn
         */
        static List<string> isbnGenerator()
        {
            Random rnd = new Random();
            List<string> isbnList = new List<string>();
            int tempTotal = 0;

            // add the first 9 digits of the isbn
            // these can be any random numbers and we can still make the isbn valid
            for (int i = 0; i < 9; i++)
            {
                int tempRandom = rnd.Next(0, 10);

                tempTotal += tempRandom * (10 - i);

                isbnList.Add(tempRandom.ToString());
            }

            int modDiff = 11 - (tempTotal % 11);

            // if the modDiff is 10, we cannot store this as a single number,
            // so we instead represent it with 'X'
            if (modDiff == 10)
            {
                isbnList.Add("X");
            }
            // for any other number, we can simply add this number to the end of our isbn
            // to make it valid
            else
            {
                isbnList.Add(modDiff.ToString());
            }
            return isbnList;
        }

        /*
         * TESTING
         */

        static void Main(string[] args)
        {
            List<string> testList = isbnGenerator();

            for (int i = 0; i < testList.Count; i++)
            {
                Console.Write(testList[i]);
            }

            Console.WriteLine();

            string testString = string.Join("", testList.ToArray());
            Console.WriteLine(isValidISBN(testString));
            Console.ReadKey();
        }
    }
}
