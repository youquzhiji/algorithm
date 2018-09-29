using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RumorMill
{



    class NameTable
    {
        Dictionary<string, int> dic = new Dictionary<string, int>();
        public void AddEntry(string name, int toll)
        {
            dic.Add(name, toll);
        }
        public int GetValue(string name)
        {
            return dic[name];
        }
    }
    class Program
    {
        class SimpleGraph
        {
            public SimpleGraph()
            {
                // 初始化边表
                edges = new Dictionary<string, string[]>();
            }

            public Dictionary<string, string[]> edges;

            public string[] neighbors(string id)
            {
                return edges[id];
            }
        }


        static void Main(string[] args)
        {


            List<string> inputs = new List<string>();
            string line;
            NameTable students = new NameTable();
            List<string> serial = new List<string>();

            string line1 = Console.ReadLine();


            int count = int.Parse(line1.ToString());
            int s = 0;
            int[,] matrix = new int[count, count];
            while ((line = Console.ReadLine()) != null && s != count)
            {

                serial.Add(line);


                s++;
            }
            s = 0;
            serial.Sort();
            for (int i = 0; i < serial.Count; i++)
            {
                students.AddEntry(serial[i], i);
            }
            long friendspair = Int64.Parse(line.ToString());
            while ((line = Console.ReadLine()) != null && s != friendspair)
            {

                string[] entry = line.Split(new char[] { ' ' });
                matrix[students.GetValue(entry[0]), students.GetValue(entry[1])] = 1;
                matrix[students.GetValue(entry[1]), students.GetValue(entry[0])] = 1;
                s++;
            }
            s = 0;
            long starters = Int64.Parse(line.ToString());
            List<string> starter = new List<string>();
            while ((line = Console.ReadLine()) != null && s != starters)
            {

                starter.Add(line);
                s++;
            }
            s = 0;


            SimpleGraph example_graph = new SimpleGraph();
            for (int i = 0; i < count; i++)
            {
                List<string> friends = new List<string>();
                for (int k = 0; k < count; k++)
                {
                    if (matrix[i, k] == 1)
                    {
                        friends.Add(serial[k]);
                    }
                }
                example_graph.edges.Add(serial[i], friends.ToArray());
            }

            foreach (string k in starter)
            {

                breadthFirstSearch(example_graph, k, serial);
            }

            // 防止退出
            Console.ReadKey();
        }

        private static void breadthFirstSearch(SimpleGraph graph, string start, List<string> nodes)
        {
            // 初始化队列
            List<string> names = new List<string>(nodes);
            Dictionary<string, int> distance = new Dictionary<string, int>();
            foreach (string k in nodes)
            {
                distance.Add(k, -1);
            }
            Dictionary<int, List<string>> result = new Dictionary<int, List<string>>();
            Queue queue = new Queue();
            queue.Enqueue(start);
            Dictionary<string, bool> visited = new Dictionary<string, bool>();
            visited[start] = true;

            while (queue.Count != 0)
            {
                string current = (string)queue.Dequeue();

                if (result.ContainsKey(distance[current]) == true)
                {

                    result[distance[current]].Add(current);
                }
                if (result.ContainsKey(distance[current]) == false)
                {
                    result.Add(distance[current], new List<string>());
                    result[distance[current]].Add(current);
                }
                names.Remove(current);

                foreach (string next in graph.neighbors(current))
                {
                    if (!visited.ContainsKey(next))
                    {
                        queue.Enqueue(next);
                        visited[next] = true;

                        distance[next] = distance[current] + 1;
                    }
                }

            }

            foreach (KeyValuePair<int, List<string>> k in result)
            {
                k.Value.Sort();
                // Console.WriteLine(k.Key);
                foreach (string s in k.Value)
                {

                    Console.Write(s + " ");
                }
            }
            names.Sort();
            foreach (string k in names)
            {
                Console.Write(k + " ");
            }
            Console.WriteLine();
        }



    }
}