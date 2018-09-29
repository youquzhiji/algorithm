using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy2
{
    class Galaxy
    {

        public long[] center;
        long range;//sauqred
        long size;
        public Galaxy(long range)
        {
            this.range = range;
            size = 0;
        }
        public long getsize()
        {
            return size;
        }
        public Galaxy(long[] centerloc, long range)
        {
            center = centerloc;
            size = 1;
            this.range = range;

        }
        public long GetDistance(Galaxy x)
        {
            return (x.center[0] - center[0]) * (x.center[0] - center[0]) + (x.center[1] - center[1]) * (x.center[1] - center[1]);
        }
        public bool CanMerge(Galaxy x)
        {
            if (GetDistance(x) > range)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

    }

    class Program
    {

        static Galaxy MooreCount(List<Galaxy> Universe)
        {
            long count = 0;
            Galaxy result = Universe[0];
            for (long i = 0; i < (long)Universe.Count(); i++)
            {
                if (count == 0)
                {
                    result = Universe[(int)i];
                    count = 1;
                }
                else if (result.CanMerge(Universe[(int)i]) == true)
                {
                    count++;
                }
                else count = count - 1;
            }
            return result;
        }
        static void Main(string[] args)
        {

            string line;
            string line1 = Console.ReadLine();
            string[] result = line1.Split(new char[] { ' ' });
            long count = Int64.Parse(result[1].ToString());
            long i = 0;
            long diametersq = Int64.Parse(result[0]) * Int64.Parse(result[0]);
            List<Galaxy> Universe = new List<Galaxy>();
            while ((line = Console.ReadLine()) != null && i != count)
            {
                string[] input = line.Split(new char[] { ' ' });
                long[] vertx = new long[2];
                vertx[0] = Int64.Parse(input[0].ToString());
                vertx[1] = Int64.Parse(input[1].ToString());
                Galaxy s = new Galaxy(vertx, diametersq);
                Universe.Add(s);
                i++;
            }
            Galaxy Prime = MooreCount(Universe);

            long printout = 0;
            foreach (Galaxy s in Universe)
            {
                if (Prime.CanMerge(s) == true)
                {
                    printout += 1;
                }
            }
            if (printout > Universe.Count / 2)
            {
                Console.WriteLine(printout + "\n");

            }
            else
            {
                Console.WriteLine("NO\n");
            }

            Console.Read();
        }
    }
}