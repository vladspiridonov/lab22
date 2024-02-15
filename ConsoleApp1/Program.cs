using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            Console.WriteLine("Massiv start");
            Random random = new Random();
            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(n);
            }
            
            Console.WriteLine("Massiv end");
            Console.ReadKey();
        }
    }
}
