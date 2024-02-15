using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace massiv_TPL
{
    class Program
    {
        static object locker = new object();
        static int[] Massiv(int n)
        {
            Random random = new Random();           
            int[] array=new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(10);                
            }
            return array;
            
        }

        static void Print(Task<int[]> task)
        {
            lock (locker)
            {
                int[] array = task.Result;
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write($"{array[i]}  ");
                }
                Console.WriteLine("\n");
            }
        }
        static void PrintS(Task<int> task)
        {
            Console.WriteLine($"\nСумма: {task.Result}");            
        }
        static void PrintMax(Task<int> task) 
        {
                Console.WriteLine($"\nМаксимальное число: {task.Result}");           
        }
        static int Summ (Task<int[]> task)
        {
            int[] array = task.Result;
            int s=0;
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i];
            }
            return s;
        }
        static int MaxNumber(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            for (int i = 0; i < array.Length; i++)
            {                
                if (array[i]>max)
                max = array[i];
            }
            return max;
        }
       
        static void Main(string[] args)
        {    
            Console.Write("Введите размер массива:");
            int n = Convert.ToInt32(Console.ReadLine());
            Task<int[]> massiv = new Task<int[]>(() =>Massiv(n));                       
            Func<Task<int[]>,int> summ = new Func<Task<int[]>,int>(Summ);                      
            Task<int> taskSumm = massiv.ContinueWith<int>(summ);
            Func<Task<int[]>, int> maxNumber = new Func<Task<int[]>, int>(MaxNumber);
            Task<int> taskMaxNumber = massiv.ContinueWith<int>(maxNumber);
            Action<Task<int[]>> action = new Action<Task<int[]>>(Print);
            Task print = massiv.ContinueWith(action);
            Action<Task<int>> action2 = new Action<Task<int>>(PrintS);
            Task prints = taskSumm.ContinueWith(action2);
            Action<Task<int>> action3 = new Action<Task<int>>(PrintMax);
            Task printMax = taskMaxNumber.ContinueWith(action3);
            massiv.Start();
            
            Console.ReadKey();
        }
    }
}
