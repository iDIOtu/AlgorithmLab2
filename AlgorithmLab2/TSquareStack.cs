using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AlgorithmLab2
{
    internal class TSquareStack : IExucate
    {
        public void ExucateAlgorithm(int n)
        {
            DrawTSquare(100, 100, 600, n);
        }

        private void DrawTSquare(float x, float y, float size, int depth)
        {
            // Используем стек для хранения параметров
            Stack<(float x, float y, float size, int depth)> stack = new Stack<(float, float, float, int)>();
            stack.Push((x, y, size, depth));

            while (stack.Count > 0)
            {
                var (currentX, currentY, currentSize, currentDepth) = stack.Pop();

                if (currentDepth == 0) continue;

                float newSize = currentSize / 2;

                stack.Push((currentX - newSize / 2, currentY - newSize / 2, newSize, currentDepth - 1)); // верхний левый
                stack.Push((currentX - newSize / 2, currentY + newSize / 2, newSize, currentDepth - 1)); // нижний левый
                stack.Push((currentX + newSize / 2, currentY - newSize / 2, newSize, currentDepth - 1)); // верхний правый
                stack.Push((currentX + newSize / 2, currentY + newSize / 2, newSize, currentDepth - 1)); // нижний правый
            }
        }
    }
}
