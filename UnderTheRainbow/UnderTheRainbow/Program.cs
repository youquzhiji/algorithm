using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderTheRainbow
{
    class Program
    {
        static int Calculate(int start, int mid, List<int> distance, List<int> penalties)
        {
            int distancetraveled = distance[mid] - distance[start];
            int penalty = Math.Abs(400 - distancetraveled) * Math.Abs(400 - distancetraveled);
            return penalty+penalties[mid];
        }
        static List<int> FindPenalty(int index, List<int> distance, List<int> penalties) 
        {
            for (int k = index+1; k < distance.Count; k++)
            {
                int penalty = penalties[index];
            
                if (penalty > Calculate(index, k, distance, penalties))
                {
                    penalties[index] = Calculate(index, k, distance, penalties);
                }
            }
            return penalties;
        }
       
        static void Main(string[] args)
        {
            string line;
            int i = -1;
            string line1 = Console.ReadLine();
            int size = int.Parse(line1);
            List<int> hotels = new List<int>();
            List<int> penalties = new List<int>();
            while ((line = Console.ReadLine()) != null && i < size)
            {
                hotels.Add(int.Parse(line));
                penalties.Add(int.MaxValue);
                i++;
            }
            penalties[size] = 0;
            for (int s = size - 1; s >= 0; s--)
            {
                penalties = FindPenalty(s, hotels, penalties);
            }
          Console.WriteLine(penalties[0]);

            Console.Read();

        }
    }
}
