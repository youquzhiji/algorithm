using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAGS
{
    class PiorityQueue
    {
        public List<QueueNode> verts;
        
        public PiorityQueue()
        {
            verts = new List<QueueNode>();
         
           
        }
        public int getsize()
        {
            return verts.Count;
        }
        public void swap(int n, int m)
        {
            QueueNode temp = verts[n];
            verts[n] = verts[m];
            verts[m] = temp;
        }
        public void swim(int pos)
        {
            while (pos > 0)
            {
                int s = pos / 2; //get its parent
                if (verts[pos].toll < verts[s].toll && verts[pos].name != verts[s].name)
                {
                    swap(pos, pos / 2);

                }
                else if (verts[pos].name == verts[s].name)
                {
                    throw new Exception();

                } //move to the top
                pos = s;

            }
        }
        public int inqueue(int tgtnanme)
        {
            
            for (int i = 0; i < verts.Count; i++)
            {
                if (verts[i].name == tgtnanme)
                {
                    return i;
                }
            }
            return -1 ;
        }
        public void InsertOrUpdate(QueueNode newvalue)
        {
            if (inqueue(newvalue.name) == -1)
            {
                verts.Add(newvalue);
                swim(verts.Count - 1);
            }
            else
            {
                verts[inqueue(newvalue.name)] = newvalue;
                swim(verts.Count - 1);
            }
        }
        public void update(QueueNode newvalue, int serial)
        {
            verts[serial] = newvalue;

            swim(verts.Count - 1);
        }
        public void add(QueueNode newvalue)
        {
            verts.Add(newvalue);
            swim(verts.Count - 1);
           
        }
        public void sink(int position)//sink from the top to the bottom
        {
            while (position < verts.Count - 1)
            {
                int s = 2 * position + 1; //left child
                if (s < verts.Count - 1)
                {
                    if (verts[s].toll < verts[position].toll && verts[s + 1].toll > verts[s].toll)
                    {
                        swap(position, s);
                    }
                    else if (verts[s + 1].toll < verts[position].toll && verts[s + 1].toll < verts[s].toll)
                    {
                        swap(position, s + 1);
                        s = s + 1;
                    }
                    else if (verts[s + 1].toll < verts[position].toll && verts[s + 1].toll < verts[position].toll)
                    {
                        swap(position, s);

                    }

                }
                else if (s == verts.Count - 1)
                {
                    if (verts[s].toll < verts[position].toll)
                    {
                        swap(position, s);
                    }
                }
                position = s;
            }

        }
        public QueueNode poptop()
        {
            int start = 0;
            int end = verts.Count - 1;
            QueueNode result = verts[start];
            swap(start, end);

            verts.Remove(verts[end]);
            sink(0);

            return result;
        }
    }
    class QueueNode
    {
        public double toll;
        public int name;
        
        public QueueNode(double toll, int no)
        {
            this.toll = toll;
            name = no;
        }
    }
    class SimpleGraph
    {
        Dictionary<int, Edge> Paths;
        int nodes;

        double[] tolls;

        List<int> visited = new List<int>();

        PiorityQueue unvisiteds;
        public void dijkstra()
        {
            tolls[0] = 0;
            unvisiteds.add(new QueueNode(0, 0));
            while (unvisiteds.getsize() != 0)
            {
                int nextnode = unvisiteds.poptop().name;
                visited.Add(nextnode);
                calculate(nextnode);
            }
        }
        public void calculate(int name)
        {
            
                foreach (KeyValuePair<int, double> neighbors in this.Paths[name].nexts)
                {
                double s =double.Parse( tolls[neighbors.Key].ToString());
                    if (tolls[neighbors.Key] == -1 ||
                    tolls[neighbors.Key] > (1 - ((1 - tolls[name]) * neighbors.Value)))
                    {
                        tolls[neighbors.Key] = (1 - ((1 - tolls[name]) * neighbors.Value));
                    unvisiteds.InsertOrUpdate(new QueueNode(tolls[neighbors.Key], neighbors.Key));
                    }

                }
            
        }


        public double GetDistance()
        {
            dijkstra();
            return double.Parse((1 - tolls[nodes - 1]).ToString());
        }
        public SimpleGraph(string structure)
        {
           
                string[] datas = structure.Split(' ');

                int nodecount = int.Parse(datas[0]);
                nodes = nodecount;
                int lines = int.Parse(datas[1]);

                tolls = new double[nodecount];
            unvisiteds = new PiorityQueue();
            Paths = new Dictionary<int, Edge>();
            for (int k = 0; k < nodecount; k++)
                {
                tolls[k] = -1;
                Paths.Add(k,new Edge());
                }
               
                int i = 0;
                while (i < lines)
                {
                    string line = Console.ReadLine();
                    string[] datasu = line.Split(' ');

                    int start = int.Parse(datasu[0]);
                    int end = int.Parse(datasu[1]);

                    double factor = double.Parse(datasu[2]);

                if (!Paths[start].nexts.ContainsKey(end))
                {
                    Paths[start].AddNext(end, factor);
                }
                if (!Paths[end].nexts.ContainsKey(start))
                {
                    Paths[end].AddNext(start, factor);
                }
                    i++;
                }
            
        }
    }
    class Edge
    {



        public Dictionary<int, double> nexts;
        
        public Edge()
        {
            nexts = new Dictionary<int, double>();
        }

        public void AddNext(int end, double toll)
        {

            this.nexts.Add(end, toll);

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<double> solutions = new List<double>();

            List<SimpleGraph> problems = new List<SimpleGraph>();

            string x = Console.ReadLine();

            while (x != "0 0")
            {
                SimpleGraph g = new SimpleGraph(x);
                
                solutions.Add(g.GetDistance());
                x =Console.ReadLine();
            }
            foreach (SimpleGraph g in problems)
            {
                
            }
            foreach (double answer in solutions)
            {
                Console.WriteLine(String.Format("{0:F4}", answer));
            }

            Console.Read();
        }
    }
}
