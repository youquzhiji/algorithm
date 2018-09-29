using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Mr.Agna
{
    class Program
    {
        static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        static void Main(string[] args)
        {
            List<string> dictionary = new List<string>();
            HashSet<string> solutions = new HashSet<string>();
            HashSet<string> rejected = new HashSet<string>();

            string line;

            string line1 = Console.ReadLine();
            string[] result = line1.Split(new char[] { ' ' });
            while ((line = Console.ReadLine()) != null && dictionary.Count != Int32.Parse(result[0].ToString()))
            {
                dictionary.Add(line);


            }
            foreach (string word in dictionary)
            {
                string sortedword = SortString(word);
                if (solutions.Remove(sortedword) == true)
                {
                    rejected.Add(sortedword);
                }
                else if (rejected.Contains(sortedword) == false)
                {
                    solutions.Add(sortedword);
                }
            }
            Console.WriteLine(solutions.Count);
            Console.Read();

        }
    }
}