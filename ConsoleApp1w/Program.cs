using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1w
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Задача о Ханойских башнях");
            Console.Write("Введите количество колец: ");

            int k;
            while (!int.TryParse(Console.ReadLine(), out k) || k <= 0)
            {
                Console.Write("Пожалуйста, введите положительное целое число: ");
            }

            int h = Hanoj(k, 'A', 'C', 'B');
            Console.WriteLine($"\nКоличество перекладываний равно {h}");
            Console.ReadLine();
        }

        // Функция перемещения колец с A на C через B
        static int Hanoj(int n, char A, char C, char B)
        {
            int num;
            if (n == 1)
            {
                Console.WriteLine($"   {A} -> {C}");
                num = 1;
            }
            else
            {
                num = Hanoj(n - 1, A, B, C);
                Console.WriteLine($"   {A} -> {C}");
                num++;
                num += Hanoj(n - 1, B, C, A);
            }
            return num;
        }
    }
}
