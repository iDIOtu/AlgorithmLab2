using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab2
{
    public class HanoiTowerRecursive : IExucate
    {
        public void ExucateAlgorithm(int n)
        {
            MoveDisks(n, 1, 3, 2);
        }
        public static void MoveDisks(int disks, int start, int end, int temp)
        {
            if (disks > 0)
            {
                MoveDisks(disks - 1, start, temp, end);
                MoveDisks(disks - 1, temp, end, start);
            }
        }
    }
}
