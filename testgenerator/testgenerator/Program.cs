using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testgenerator
{
    class Graph
    {
        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        public Graph()
        {
            graph = new Dictionary<int, List<int>>();
        }
    }
    class Program
    {
        static HashSet<int> GetPair(int min, int max, Random r)
        {
            int a = r.Next(min, max);
            int b = r.Next(min, max);
            while (a == b)
            {
                b = r.Next(min, max);
            }
            HashSet<int> result = new HashSet<int>();
            result.Add(a);
            result.Add(b);
            return result;
        }
        static bool compareset(HashSet<int> x, HashSet<int> y)
        {
            foreach (int k in x)
            {
                if (!y.Contains(k))
                {
                    return false;
                }
            }
            return true;
        }
        static bool corhas(List<HashSet<int>> x, HashSet<int> pair)
        {
            for (int i = 0; i < x.Count; i++)
            {
                if (compareset(x[i], pair) == true)
                {
                    return true;
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            Random x = new Random();
            int k = 100;

            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;

            ostrm = new FileStream("./Redirect" + k.ToString() + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(ostrm);
            Console.SetOut(writer);
            while (k>0)
            {
                int intersects = x.Next(2,100);
                int corridors = x.Next(intersects-1, (intersects * (intersects - 1)) / 2);
                
                List<int> inters = new List<int>();
                List<HashSet<int>> cors = new List<HashSet<int>>();
               
                for (int i = 0; i < corridors; i++)
                {
                    HashSet < int > pair= GetPair(0, intersects,x);
                    while (corhas(cors,pair))
                    {
                        pair = GetPair(0, intersects, x);
                    }
                    cors.Add(pair);
                }
                 HashSet<int> musthave = new HashSet<int>();
                musthave.Add(0);
                musthave.Add(intersects - 1);
                if (!corhas(cors, musthave))
                {
                    cors.Add(musthave);
                    corridors += 1;
                }
                


                Console.Write(intersects);
                Console.Write(' ');
                Console.WriteLine(corridors);
              
                foreach (HashSet<int> par in cors)
                {
                    foreach (int s in par)
                    {
                        Console.Write(s);
                        Console.Write(' ');
                    }
                    Console.WriteLine(String.Format("{0:F4}",x.NextDouble()));
                }
                
                k--;
            }
            Console.WriteLine("0 0");
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("done");
            Console.Read();
            
        }
    }
}
