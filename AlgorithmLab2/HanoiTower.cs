using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab2
{
    public class HanoiTower
    {
        public static void MoveDisks(int start, int temp, int end, int disks)
        {
            if (disks > 1)
            {
                MoveDisks(start, end, temp, disks - 1);
            }

            if (disks > 1)
            {
                MoveDisks(temp, start, end, disks - 1);
            }
        }
    }
}
