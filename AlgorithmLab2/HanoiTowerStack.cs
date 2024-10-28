using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab2
{
    internal class HanoiTowerStack : IExucate
    {
        public void ExucateAlgorithm(int n)
        {
            Move(n, 1, 3);
        }
        public void Move(int i, int m, int n)
        {
            Stack<Tuple<int, int, int>> stack = new Stack<Tuple<int, int, int>>();

            stack.Push(new Tuple<int, int, int>(i, m, n));

            while (stack.Count > 0)
            {
                var top = stack.Pop();
                int j = top.Item1;
                int p = top.Item2;
                int q = top.Item3;

                if (j == 1)
                {
                    continue;
                    //Console.WriteLine($"Сделать ход {p} -> {q}");
                }
                else
                {
                    int s = 6 - p - q;
                                       
                    stack.Push(new Tuple<int, int, int>(j - 1, s, q));
                    stack.Push(new Tuple<int, int, int>(1, p, q));
                    stack.Push(new Tuple<int, int, int>(j - 1, p, s));
                }
            }
        }
    }
}