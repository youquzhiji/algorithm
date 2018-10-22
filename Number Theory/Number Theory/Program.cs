using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_Theory
{
    class Program
    {
        public static long get_gcd(long a, long b)
        {

            while (a != 0 && b != 0)
            {
                long temp = long.Parse(b.ToString());
                b = a % b;
                a = temp;
            }
            if (a == 0)
            {
                return b;
            }
            if (b == 0)
            {
                return a;
            }
            else
            {
                return -1;
            }
        }
        public static List<long> to_binary(long b)
        {
            long counter = 0;
            List<long> x = new List<long>();
            while (b != 0)
            {
                if (b % 2 == 1)
                {
                    x.Add(IntPow(2, counter));
                }
                b = b / 2;
                counter++;
            }
            return x;

        }
        static public long IntPow(long x, long pow)//overflow impossible
        {
            long ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
        public static long get_exp(long a, long b, long n)
        {
            List<long> exps = to_binary(b);
            long start = 1;
            foreach (long x in exps)
            {
                start = (start * get_expjr(a, x, n)) % n; //possible overflow
            }
            return start;
        }
        public static long get_expjr(long a, long b, long n)
        {
            long rm = a % n;
            while (b != 1)
            {
                b = b / 2;
                rm = (rm * rm) % n; //possible overflow
            }
            return rm;
        }
        public static long inverse(long a, long n)
        {
            if (get_gcd(a, n) != 1)
            {
                Console.WriteLine("none");
                return 0;
            }


            long n0 = n;
            long y = 0, x = 1;

            if (n == 1)
            {
                return 0;
            }

            while (a > 1)
            {
                // q is quotient
                long q = a / n;
                long t = n;

                // m is remainder now, process same as
                // Euclid's algo
                n = a % n;
                a = t;
                t = y;

                // Update y and x
                y = x - q * y;
                x = t;
            }

            // Make x positive
            if (x < 0)

                x += n0;

            return x;

        }
        static bool miillerTest(long d, long n)
        {
            // Pick a random number in [2..n-2]
            // Corner cases make sure that n > 4
            Random k = new Random();
            long a = 2 + (long)((k.NextDouble()) * n - 2);

            // Compute a^d % n
            long x = get_exp(a, d, n);

            if (x == 1 || x == n - 1)
                return true;

            // Keep squaring x while one of the following doesn't
            // happen
            // (i)   d does not reach n-1
            // (ii)  (x^2) % n is not 1
            // (iii) (x^2) % n is not n-1
            while (d != n - 1)
            {
                x = (x * x) % n;
                d *= 2;

                if (x == 1) return false;
                if (x == n - 1) return true;
            }

            // Return composite
            return false;
        }

        public static bool isprime(long a)
        {
            int k = 3;//times
            // Corner cases
            if (a <= 1 || a == 4)
            {
                Console.WriteLine("no");
                return false;
            }
            if (a <= 3)
            {
                Console.WriteLine("yes");
                return true;
            }

            // Find r such that n = 2^d * r + 1 for some r >= 1
            long d = a - 1;
            while (d % 2 == 0)
                d /= 2;

            // Iterate given nber of 'k' times
            for (int i = 0; i < k; i++)
            {
                if (miillerTest(d, a) == false)
                {
                    Console.WriteLine("no");
                    return false;
                }
            }
            Console.WriteLine("yes");
            return true;


        }
        public static void key(long a, long b)
        {
            long n = a * b;
            long r = (a - 1) * (b - 1);
            int i;
            for (i = 2; i < r; i++)
            {
                if (get_gcd(i, r) == 1)
                {
                    break;
                }
            }
            long e = long.Parse(i.ToString());
            long d = inverse(e, r);
            Console.WriteLine(n + " " + e + " " + d);

        }
        static void Main(string[] args)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                string[] input = line.Split(' ');
                switch (input[0])
                {
                    case "gcd":
                        Console.WriteLine(get_gcd(long.Parse(input[1]), long.Parse(input[2])));
                        break;
                    case "exp":
                        Console.WriteLine(get_exp(long.Parse(input[1]), long.Parse(input[2]), long.Parse(input[3])));
                        break;
                    case "inverse":
                        if (inverse(long.Parse(input[1]), long.Parse(input[2])) != 0)
                        {
                            Console.WriteLine(inverse(long.Parse(input[1]), long.Parse(input[2])));
                        }

                        break;
                    case "isprime":
                        isprime(long.Parse(input[1]));
                        break;
                    case "key":
                        key(long.Parse(input[1]), long.Parse(input[2]));
                        break;
                    case "tob":
                        foreach (long x in to_binary(long.Parse(input[1])))
                        {
                            Console.WriteLine(x);
                        }
                        break;
                    case "expjr":
                        Console.WriteLine(get_expjr(long.Parse(input[1]), long.Parse(input[2]), long.Parse(input[3])));
                        break;

                    default:

                        return;
                }

            }


        }
    }
}
