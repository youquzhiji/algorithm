using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankQueue
{
    
    class Program
    {
        static void Main(string[] args)
        {
            List<int>[] Customers;
            string x = Console.ReadLine();
            int minutes = int.Parse(x.Split(' ')[1]);
            int customers = int.Parse(x.Split(' ')[0]);
            Customers = new List<int>[minutes];
            for (int s = 0; s < minutes; s++)
            {
                Customers[s] = new List<int>();
            }
            for (int k = 0; k < customers; k++)
            {
                string customer=Console.ReadLine();
                int time = int.Parse(customer.Split(' ')[1]);
                int money = int.Parse(customer.Split(' ')[0]);
                if (Customers[time].Contains(money) == false)
                {
                    Customers[time].Add(money);
                }
            }
            int total = 0;
            int timepass = minutes;
            List<int> cadidates = new List<int>();
            for (int k = timepass - 1; k >= 0; k--)
            {
                foreach (int s in Customers[k])
                {
                    cadidates.Add(s);
                }
                cadidates.Sort();
                cadidates.Reverse();
                if (cadidates.Count != 0)
                {
                    int max = cadidates[0];
                    total += max;
                    cadidates.Remove(max);
                }
            }
           /*
            foreach(List<int>cs in Customers)
            {
                if (cs.Count != 0)
                {
                    cs.Sort();
                    total += cs[cs.Count-1];
                }
                
            }*/
            Console.WriteLine(total);
          //  Console.WriteLine("Done");
            Console.Read();
        }
    }
}
