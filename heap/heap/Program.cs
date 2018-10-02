using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace april_19
{
    class MaxHeap
    {
        List<int> element;
        public int this[int index]
        {
            get { return element[index + 1]; }
        }
        public void buildheap(List<int> k)
        {
            foreach (int m in k)
            {
                add(m);
            }
        }
        public int count()
        {
            return element.Count;
        }
        public void showvalue()
        {
            foreach (int k in element)
            {
                Console.WriteLine(k);
            }
        }
        public MaxHeap()
        {
            element = new List<int>();

        }
        public void swap(int i, int m)
        {
            int temp = element[i];
            element[i] = element[m];
            element[m] = temp;
        }
        public void swim()
        {
            for (int i = element.Count - 1; i > 0;)
            {
                int s = i / 2;
                if (element[i] > element[s])
                {
                    swap(i, i / 2);

                }
                i = s;

            }
        }
        public void add(int newvalue)
        {
            element.Add(newvalue);
            swim();
        }
        public void heapify()
        {
            List<int> result = new List<int>();
            foreach (int k in element)
            {
                result.Add(k);
            }
            element.Clear();
            buildheap(result);
            sort();
            element.Reverse();

        }
        public void sort()
        {
            int start = 0;
            int end = element.Count - 1;
            while (end > 0)
            {
                swap(start, end);

                end -= 1;


                for (int i = 0; i < end;)
                {
                    int s = 2 * i + 1;
                    if (s < end)
                    {
                        if (element[s] > element[i] && element[s + 1] < element[s])
                        {
                            swap(i, s);
                        }
                        else if (element[s + 1] > element[i] && element[s + 1] > element[s])
                        {
                            swap(i, s + 1);
                            s = s + 1;
                        }

                    }
                    if (s == end)
                    {
                        if (element[s] > element[i])
                        {
                            swap(i, s);
                        }
                    }
                    i = s;
                }

            }
        }
        public void sink()
        {
            for (int i = 0; i < element.Count - 1;)
            {
                int s = 2 * i + 1;
                if (s < element.Count - 1)
                {
                    if (element[s] > element[i] && element[s + 1] < element[s])
                    {
                        swap(i, s);
                    }
                    else if (element[s + 1] > element[i] && element[s + 1] > element[s])
                    {
                        swap(i, s + 1);
                        s = s + 1;
                    }

                }
                if (s == element.Count - 1)
                {
                    if (element[s] > element[i])
                    {
                        swap(i, s);
                    }
                }
                i = s;
            }

        }
        public int poptop()
        {
            int start = 0;
            int end = element.Count - 1;
            int result = element[start];
            swap(start, end);

            element.Remove(element[end]);
            sink();

            return result;
        }
        public void testivairaiant()
        {
            for (int i = 0; i < element.Count - 1; i++)
            {
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                if (left <= element.Count - 1)
                {
                    Debug.Assert(element[i] > element[left]);
                    if (right <= element.Count - 1)
                    {
                        Debug.Assert(element[i] > element[right]);
                    }
                }
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            List<int> m = new List<int> { 100, 2, 40, 30, 1 };
            foreach (int value in m)
            {
                Console.Write(value + ", ");
            }


            MaxHeap k = new MaxHeap();
            Console.WriteLine("\nuse the above list to build a heap");

            k.buildheap(m);
            k.showvalue();
            Console.WriteLine("add");
            k.add(1000);
            k.add(77);
            k.add(88);

            k.add(65535);
            Console.WriteLine("test invariant");
            k.testivairaiant();
            Console.WriteLine("success");
            k.showvalue();
            Console.WriteLine("sort");
            k.sort();
            for (int i = 0; i < k.count() - 2; i++)
                Debug.Assert(k[i] < k[i + 1]);
            k.showvalue();

            Console.WriteLine("reheap");
            k.heapify();
            k.showvalue();
            Console.WriteLine("poptop");
            List<int> poplist = new List<int>();
            while (k.count() > 0)
            {
                int top = k.poptop();
                Console.WriteLine(top);
                poplist.Add(top);

            }
            for (int i = 0; i < poplist.Count - 2; i++)
            {
                Debug.Assert(poplist[i] > poplist[i + 1]);
            }

            Console.ReadLine();


        }
    }
}
