using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarrowArtGallery
{
    class Gallery
    {
       public int numofrows;
        int[,,] maxresults;
        public int closecount;
        int[,] maxvalues;
        List<int> values;
        public Gallery(int rowcount,int closedoor)
        {
            numofrows = rowcount;
            maxresults = new int[rowcount+1,3,closedoor+1];
            closecount = closedoor;
            values = new List<int>();
            maxvalues = new int[rowcount, 3];
        }
        public void AddValue(int value)
        {
            values.Add(value);
        }
        public int GetSum(int row)
        {
            int result = 0;
            for (int k = row * 2; k < values.Count; k++)
            {
                result += values[k];
            }
            return result;
        }
        public int MaxValue(int row, int uncloseable, int closeneed)
        {
            if (maxresults[row, uncloseable + 1, closeneed] != 0)
            {
               // Console.WriteLine("cached!");
                return maxresults[row, uncloseable + 1, closeneed];
            }
          //  Console.WriteLine(row + " " + uncloseable + " " + closeneed);

            if (closeneed == 0)
            {
                return GetSum(row);
            }
            else if (closeneed == this.numofrows - row)
            {

                switch (uncloseable)
                {
                    case 0:
                        maxresults[row,uncloseable+1,closeneed]=
                            values[row * 2] + MaxValue(row + 1, uncloseable, closeneed - 1);
                        return maxresults[row, uncloseable + 1, closeneed];
                    case 1:
                        maxresults[row, uncloseable + 1, closeneed]=
                            values[row * 2 + 1] + MaxValue(row + 1, uncloseable, closeneed - 1);
                        return maxresults[row, uncloseable + 1, closeneed];
                    case -1:
                        maxresults[row, uncloseable + 1, closeneed]=
                            Math.Max(values[row * 2] + MaxValue(row + 1, 0, closeneed - 1), 
                            values[row * 2 + 1] + MaxValue(row + 1, 1, closeneed - 1));
                        return maxresults[row, uncloseable + 1, closeneed];
                }
            }
            else if (closeneed < this.numofrows - row)
            {
                switch (uncloseable)
                {
                    case 0:
                        maxresults[row, uncloseable + 1, closeneed]= Math.Max(values[row * 2] + MaxValue(row + 1, uncloseable, closeneed - 1),
                           values[row * 2 + 1] + values[row * 2] + MaxValue(row + 1, -1, closeneed));
                        return maxresults[row, uncloseable + 1, closeneed];
                    case 1:
                        maxresults[row, uncloseable + 1, closeneed]= Math.Max(values[row * 2 + 1] + MaxValue(row + 1, uncloseable, closeneed - 1),
                        values[row * 2 + 1] + values[row * 2] + MaxValue(row + 1, -1, closeneed));
                        return maxresults[row, uncloseable + 1, closeneed];
                    case -1:
                        maxresults[row, uncloseable + 1, closeneed]= Math.Max(Math.Max(values[row * 2] + MaxValue(row + 1, 0, closeneed - 1),
                            values[row * 2 + 1] + MaxValue(row + 1, 1, closeneed - 1)),
                            values[row * 2 + 1] + values[row * 2] + MaxValue(row + 1, -1, closeneed));
                        return maxresults[row, uncloseable + 1, closeneed];
                }

            }
            //prototype
            throw new ArgumentException();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string data=Console.ReadLine();
            string[] rowclose = data.Split();
            Gallery gallery=new Gallery(int.Parse(rowclose[0]),int.Parse(rowclose[1]));
            int a = 0;
            while ( a!=gallery.numofrows)
            {
                data = Console.ReadLine();
                    string[] datas = data.Split();
                    gallery.AddValue(int.Parse(datas[0]));
                    gallery.AddValue(int.Parse(datas[1]));
                  
                a++;

            }
            int k=gallery.MaxValue(0,-1,gallery.closecount);
            Console.WriteLine(k);

                       Console.Read();

        }
    }
}
