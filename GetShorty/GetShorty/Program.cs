using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetShorty
{
    public class SimpleGraph
    {
        Dictionary<int,Node> Paths;
        int nodes;
        int[] prevs;
        double[] tolls;
        int[] nexts;
        double[] factors;
        List<int> visited = new List<int>();
        List<int> unvisited = new List<int>();

        public void findexit()
        {

            evaluate(0, 1);
            tolls[0] = 1;
            unvisited.Remove(0);
            while (unvisited.Count!=0)
            {
                int s = getmost();
                evaluate(s,tolls[s]);
                unvisited.Remove(s);
            }            
        }
        public int getmost()
        {
            double largest=tolls[unvisited[0]];
            int s = 0;
            for (int i = 0; i < unvisited.Count; i++)
            {
                if (largest < tolls[unvisited[i]])
                {
                    largest = tolls[unvisited[i]];
                    s = i;
                }
            }
            if (largest == -1)
            {
                return unvisited[0];
            }
            else
            {
                return unvisited[s];
            }
        }
        public void evaluate(int start, double toll)
        {

            foreach (KeyValuePair<int, double> neighbors in this.Paths[start].nexts)
            {
                if (tolls[neighbors.Key] < toll*neighbors.Value)
                {
                    tolls[neighbors.Key] = toll * neighbors.Value;
                }
            }
            

        }
        public double GetDistance()
        {
            findexit();
            return double.Parse(tolls[nodes - 1].ToString());
        }
        public SimpleGraph(string structure)
        {
            string[] datas = structure.Split(' ');
            int nodecount = int.Parse(datas[0]);
            nodes = nodecount;
            int lines = int.Parse(datas[1]);
            prevs = new int[nodecount];
            tolls = new double[nodecount];
            nexts = new int[nodecount];
            factors = new double[nodecount];
            for (int k = 0; k < nodecount;k++)
            {
                prevs[k] = -1;
                tolls[k] = -1;
                nexts[k] = -1;
                factors[k] = -1;
                unvisited.Add(k);

            }
            Paths = new Dictionary<int, Node>();
            int i = 0;
            while ( i < lines)
            {
                string line = Console.ReadLine();
                string[] datasu = line.Split(' ');
                int start = int.Parse(datasu[0]);
                int end = int.Parse(datasu[1]);
                double toll = double.Parse(datasu[2]);
                if (Paths.ContainsKey(start))
                {
                    Paths[start].AddNext(end, toll);
                }
                else
                {
                    Paths.Add(start,new Node(start,end,toll));
                    
                }
                if (Paths.ContainsKey(end))
                {
                    Paths[end].AddNext(start, toll);
                }
                else
                {
                    Paths.Add(end, new Node(end, start, toll));

                }
                i++;
            }
        }
        public class Node
        {
            public int serial;
            public bool visited;
            public double tollsofar;
            public Dictionary<int,double> nexts;
            public Node(int seiral, int end, double toll)
            {
                nexts = new Dictionary<int, double>();
                nexts.Add(end, toll);
                this.serial = seiral;
                tollsofar = -1;
                visited = false;
            }
           
            public void AddNext(int end,double toll)
            {
                
                    this.nexts.Add(end,toll);
                
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            List<double> solutions = new List<double>();
            
            string x = Console.ReadLine();
            while (x != "0 0")
            {
                SimpleGraph graph = new SimpleGraph(x);
                solutions.Add(graph.GetDistance());
                
                x = Console.ReadLine();
            }
            foreach (double answer in solutions)
            {
                Console.WriteLine(String.Format("{0:F4}", answer));
            }
            Console.Read();
        }
    }
}
