using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetShorty
{
    public class PiorityQueue
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
                if (verts[pos].toll < verts[s].toll&& verts[pos].name != verts[s].name)
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
            for(int i=0;i<verts.Count;i++)
            {
                if (verts[i].name == tgtnanme)
                {
                    return i;
                }
            }
            return -1;
        }
        public void update(QueueNode newvalue, int serial)
        {
            verts[serial] = newvalue;
            int parent = serial / 2;
            int leftchild = serial * 2 + 1;
            if (verts[serial].toll < verts[parent].toll)
            {
                swim(serial);
            }
            else if (leftchild<verts.Count-1&&verts[serial].toll > verts[leftchild].toll)
            {
                sink(serial);
            }
        }
        public void add(QueueNode newvalue)
        {
            verts.Add(newvalue);
            swim(verts.Count-1);
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
                    else if (verts[s + 1].toll < verts[position].toll&& verts[s + 1].toll < verts[s].toll)
                    {
                        swap(position, s + 1);
                        s = s + 1;
                    }
                    else if (verts[s + 1].toll < verts[position].toll && verts[s + 1].toll < verts[position].toll)
                    {
                        swap(position, s );
                        
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
    public class QueueNode
    {
        public double toll;
        public int name;
        public QueueNode(double factor, int no, double tsf)
        {
            this.toll = 1-factor*tsf;
            name = no;
        }
        public QueueNode(double toll, int no)
        {
            this.toll = toll;
            name = no;
        }
    }
    public class SimpleGraph
    {
        Dictionary<int,Node> Paths;
        int nodes;
 
        double[] tolls;
        
        List<int> visited = new List<int>();
        
        PiorityQueue unvisiteds = new PiorityQueue();
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
                if (!visited.Contains(neighbors.Key))
                {
                    if(tolls[neighbors.Key] == -1 ||
                    tolls[neighbors.Key] > (1 - ((1 - tolls[name]) * neighbors.Value)))
                    {
                        tolls[neighbors.Key] = (1 - ((1 - tolls[name]) * neighbors.Value));

                        if (unvisiteds.inqueue(neighbors.Key) != -1)
                        {
                            unvisiteds.update(new QueueNode(tolls[neighbors.Key], neighbors.Key), unvisiteds.inqueue(neighbors.Key));
                        }
                        else if (unvisiteds.inqueue(neighbors.Key) == -1)
                        {
                            unvisiteds.add(new QueueNode(tolls[neighbors.Key], neighbors.Key));
                        }
                    }
                }
            }
        }
       
       
        public double GetDistance()
        {
            dijkstra();
            return double.Parse((1-tolls[nodes - 1]).ToString());
        }
        public SimpleGraph(string structure)
        {
            string[] datas = structure.Split(' ');
            int nodecount = int.Parse(datas[0]);
            nodes = nodecount;
            int lines = int.Parse(datas[1]);
          
            tolls = new double[nodecount];

           
            for (int k = 0; k < nodecount;k++)
            {
               
                tolls[k] = -1;


            }
            Paths = new Dictionary<int, Node>();
            int i = 0;
            while (i < lines)
            {
                string line = Console.ReadLine();
                string[] datasu = line.Split(' ');
                int start = int.Parse(datasu[0]);
                int end = int.Parse(datasu[1]);
                double factor = double.Parse(datasu[2]);
                if (Paths.ContainsKey(start))
                {
                    Paths[start].AddNext(end, factor);
                }
                else if (!Paths.ContainsKey(start))
                {
                    Paths.Add(start, new Node(start, end, factor));

                }
                
                if (Paths.ContainsKey(end))
                {
                    Paths[end].AddNext(start, factor);
                }
                else if (!Paths.ContainsKey(end))
                {
                    Paths.Add(end, new Node(end, start, factor));

                }
                i++;
            }
        }
        public class Node
        {

            

            public Dictionary<int,double> nexts;
            public Node(int seiral, int end, double toll)
            {
                nexts = new Dictionary<int, double>();
                nexts.Add(end, toll);

               
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
