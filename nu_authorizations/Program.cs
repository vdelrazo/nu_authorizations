using System;

namespace nu_authorizations
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = string.Empty;
            string outputString = string.Empty;

        Input:
            line = Console.ReadLine();

            if (line != string.Empty)
            {
                outputString += line;
                goto Input;

            }
            else
            {
                Console.WriteLine("OutputString:{0}", outputString);
            }

            Console.ReadKey();
        }
    }
}
