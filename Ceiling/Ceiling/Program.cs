
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ceiling
{
    class Node
    {
        public int value;
        public Node left;
        public Node right;
        public int pos;
        public Node(int f)
        {
            value = f;
            left = null;
            right = null;
        }
        public Node()
        {
            value = -1;
            left = null;
            right = null;
        }
    }
    class Tree
    {
        public List<int> shape = new List<int>();
        public Node root = new Node();
        public string getShape()
        {
            string result = "";
            shape.Sort();
            foreach (int x in shape)
            {
                result = result + x.ToString();
            }
            return result;
        }

        public Node insert(Node root, int v, int pos)
        {
            if (root == null)
            {
                root = new Node();
                root.value = v;
                root.pos = pos;
                shape.Add(pos);
            }
            else if (v < root.value)
            {
                root.left = insert(root.left, v, root.pos * 2);
            }
            else
            {
                root.right = insert(root.right, v, root.pos * 2 + 1);
            }

            return root;
        }

        public Tree()
        {

        }

    }
    class Program
    {

        static void Main(string[] args)
        {


            List<string> dictionary = new List<string>();
            HashSet<string> solutions = new HashSet<string>();

            string line;

            string line1 = Console.ReadLine();
            string[] result = line1.Split(new char[] { ' ' });
            while ((line = Console.ReadLine()) != null && dictionary.Count != Int32.Parse(result[0].ToString()))
            {
                dictionary.Add(line);

            }
            foreach (string x in dictionary)
            {
                string[] numberstr = x.Split(new char[] { ' ' });
                List<int> nums = new List<int>();
                foreach (string k in numberstr)
                {
                    nums.Add(Int32.Parse(k.ToString()));
                }
                Tree bst = new Tree();
                Node root = null;

                for (int i = 0; i < nums.Count(); i++)
                {
                    root = bst.insert(root, nums[i], 1);
                }
                solutions.Add(bst.getShape());


            }

            Console.WriteLine(solutions.Count);
            Console.Read();

        }
    }
}

