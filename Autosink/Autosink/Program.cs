using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autosink
{

    class City
    {
        public int tollsofar;
        public int toll;
        public string name;
        public int order;
        public LinkedList<City> nexts;
        public bool visited;
        public City(string name, int toll)
        {
            this.name = name;
            this.toll = toll;

            nexts = new LinkedList<City>();
            order = 0;
            visited = false;
        }

    }

    class Program
    {

        static Dictionary<string, City> readcity(int s, Dictionary<string, int> tolls)
        {
            Dictionary<string, City> result = new Dictionary<string, City>();
            for (int k = 0; k < s; k++)
            {
                string a = Console.ReadLine();
                string[] citydata = a.Split(new char[] { ' ' });
                result[citydata[0]] = new City(citydata[0], int.Parse(citydata[1]));
                tolls[citydata[0]] = int.Parse(citydata[1]);
            }
            return result;
        }
        static void readroads(Dictionary<string, City> cities, int count, Dictionary<string, int> tollist)
        {
            for (int k = 0; k < count; k++)
            {
                string a = Console.ReadLine();
                string[] citydata = a.Split(new char[] { ' ' });
                cities[citydata[0]].nexts.AddLast(new City(citydata[1], tollist[citydata[1]]));
            }
        }
        static void pot(Dictionary<string, int> tolllist, Dictionary<string, City> citylist)
        {
            int clock = 0;
            foreach (KeyValuePair<string, City> pair in citylist)
            {

            }
        }
        static void explore(LinkedList<City> iterate, City x, Dictionary<string, City> citylist, int clock)
        {

            x.visited = true;


            foreach (City next in x.nexts)
            {

                if (citylist[next.name].visited == false)
                {
                    explore(iterate, citylist[next.name], citylist, clock);
                }
            }
            iterate.AddFirst(citylist[x.name]);


        }
        static City[] getpotlist(string begin, string end, Dictionary<string, int> tolllist,
            int citycount, Dictionary<string, City> citylist)
        {
            City[] result = new City[citycount];
            foreach (KeyValuePair<string, City> pair in citylist)
            {

            }
            return result;
        }
        static void readpath(LinkedList<City> list, int count, Dictionary<string, City> citylist)
        {
            for (int i = 0; i < count; i++)
            {
                string pathdata = Console.ReadLine();
                string[] path = pathdata.Split(' ');
                GetPath(list, path[0], path[1], citylist);
            }
        }
        static void GetPath(LinkedList<City> list, string start, string end, Dictionary<string, City> citylist)
        {
            list = new LinkedList<City>();
            foreach (KeyValuePair<string, City> pair in citylist)
            {

                pair.Value.visited = false;
                pair.Value.tollsofar = 0;
            }
            explore(list, citylist[start], citylist, 0);
            if (list.Contains(citylist[end]) == false)
            {
                Console.WriteLine("NO");
                return;
            }

            City begin = list.Find(citylist[start]).Value;
            begin.tollsofar = 0;
            foreach (City x in citylist[start].nexts)
            {

            }
            while (list.Find(citylist[begin.name]).Next != null)
            {
                foreach (City x in begin.nexts)
                {
                    if (citylist[x.name].tollsofar == 0)
                    {
                        citylist[x.name].tollsofar = begin.tollsofar + x.toll;
                    }
                    else if (citylist[x.name].tollsofar != 0 && citylist[x.name].tollsofar > (begin.tollsofar + x.toll))
                    {
                        citylist[x.name].tollsofar = begin.tollsofar + x.toll;
                    }
                }
                begin = list.Find(citylist[begin.name]).Next.Value;
            }

            Console.WriteLine(list.Find(citylist[end]).Value.tollsofar);
        }
        static string findroot(Dictionary<string, City> cities)
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, City> pair in cities)
            {
                list.Add(pair.Key);
            }
            foreach (KeyValuePair<string, City> pair in cities)
            {
                foreach (City x in pair.Value.nexts)
                {
                    list.Remove(x.name);
                }
            }
            return list[0];
        }
        static void Main(string[] args)
        {
            LinkedList<City> iterate = new LinkedList<City>();
            Dictionary<string, int> tolllist = new Dictionary<string, int>();
            int citycount = int.Parse(Console.ReadLine());
            Dictionary<string, City> citylist = readcity(citycount, tolllist);
            int roadcount = int.Parse(Console.ReadLine());
            readroads(citylist, roadcount, tolllist);
            //to do
            //get root
            //check if there is actually a path
            string rootname = findroot(citylist);
            // explore(iterate,citylist["Weston"],citylist,0);
            //explore(iterate,citylist[rootname],citylist,0);
            int pathcount = int.Parse(Console.ReadLine());
            readpath(iterate, pathcount, citylist);
            Console.Read();
        }
    }
}

