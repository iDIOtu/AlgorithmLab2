using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AlgorithmLab2
{
    internal class TSquareRecursive : IExucate
    {
        public void ExucateAlgorithm(int n)
        {
            DrawT(100, 100, 600, n);
        }

        private void DrawT(double centerX, double centerY, double size, int depth)
        {
            if (depth == 0) return;

            double newSize = size / 2;

            DrawT(centerX - size / 2, centerY - size / 2, newSize, depth - 1); // Верхний левый
            DrawT(centerX - size / 2, centerY + size / 2, newSize, depth - 1); // Нижний левый
            DrawT(centerX + size / 2, centerY - size / 2, newSize, depth - 1); // Верхний правый
            DrawT(centerX + size / 2, centerY + size / 2, newSize, depth - 1); // Нижний правый 
        }
    }
}
