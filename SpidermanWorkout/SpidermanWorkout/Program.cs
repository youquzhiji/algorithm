using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpidermanWorkout
{
    class Program
    {
       static int [,]level=new int[41,1001];
      static  int[] dist=new int[41];
        
      static  int ndist;


       static void printsol()
        {
            string k="";
            int h = 0;
            int step;
            for (step = 1; step <= ndist; ++step)
            {
                /*    fprintf(stderr, " %d", h); */

                if (h - dist[step] >= 0 && level[step,(h - dist[step])]!=0)
                {
                    k+='D';
                    h -= dist[step];
                }
                else
                {
                    k+='U';
                    h += dist[step];
                }
            }
            Console.WriteLine(k);
            /*  fprintf(stderr, " %d\n", h); */
        }

       static int canclimb(int maxh)
        {
            int i;
            int h;

            for (i = 0; i <= ndist; ++i)
                for (h = 0; h <= 1000; ++h)
                    level[i,h] = 0;

            level[ndist,0] = 1;

            for (i = ndist; i > 0; --i)
                for (h = 0; h <= maxh; ++h)
                    if (level[i,h]!=0)
                    {
                        if (h - dist[i] >= 0)
                        {
                            level[i - 1, h - dist[i]] = 1;
                        }
                        if (h + dist[i] <= maxh)
                        {
                            level[i - 1, h + dist[i]] = 1;
                        }
                    }

            return level[0,0];

        }
       static void solve(int cnt)
        {
           
           
            int lo = 0;
            int hi = 1000;
            int mid;

            ndist = int.Parse(Console.ReadLine ());
            string s = Console.ReadLine();
            string[] distances = s.Split();
            for (int i=1; i<=distances.Count();i++)
            {
                dist[i] = int.Parse(distances[i-1]);
            }
            
            while (hi > lo + 1)
            {
                mid = (lo + hi) / 2;
                if (canclimb(mid) != 0)
                {
                    hi = mid;
                }
                else
                {
                    lo = mid;
                }
            }
            if (canclimb(hi)!=0)
            {
                printsol();
                return;
            }


            /*else*/
            Console.WriteLine("IMPOSSIBLE");

        }

        static void Main(string[] args)
        {
            int i, n;
            n = int.Parse(Console.ReadLine());
            for (i = 0; i < n; ++i)
            {
                solve(i);
            }
            Console.Read();
        }
    }
}
