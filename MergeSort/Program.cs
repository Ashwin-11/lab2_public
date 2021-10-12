using System;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Collections.Generic;


namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {

            int ARRAY_SIZE = 24;
            int num_threads=4;
            int[] arraySingleThread = new int[ARRAY_SIZE];
            int num_processors = Environment.ProcessorCount;
            Console.WriteLine(num_processors);//12 processors
            



            // TODO : Use the "Random" class in a for loop to initialize an array

            Random randNum = new Random();

            for (int i=0; i < ARRAY_SIZE; i++)
            {
                arraySingleThread[i] = randNum.Next(0, ARRAY_SIZE);
            }
      

            // copy array by value.. You can also use array.copy()
            int[] arrayMultiThread= new int[ARRAY_SIZE];
            arraySingleThread.CopyTo(arrayMultiThread, 0);
           

          //   PrintArray(arraySingleThread);

            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
               it takes to sort an array using one-thread merge sort and
               multi-thead merge sort
            */

            //TODO :start the stopwatch
            Stopwatch SingleThreadTime = new Stopwatch();
            SingleThreadTime.Start();

            MergeSort(arraySingleThread);
            // Thread.Sleep(224);

            //TODO :Stop the stopwatch
            SingleThreadTime.Stop();

            PrintArray(arraySingleThread);
            PrintTime(SingleThreadTime.Elapsed);  // Get the elapsed time as a TimeSpan value, passes it to the print time function



            //TODO: Multi Threading Merge Sort
            // List<List<int>> subArrays = new List<List<int>>();

            Stopwatch MultiThreadTime = new Stopwatch();
            

            int[][] subArrays = new int[num_threads][];
              int maxsubArr_len =ARRAY_SIZE/num_threads;
            int k = 0;

            Console.Write("Sub array of {0} total elements and {1} threads looks like:\n",ARRAY_SIZE,num_threads);

            for (int i = 0; i < num_threads; i++)
            {
                k = i * maxsubArr_len;
                if (i == (num_threads - 1))
                {
                    maxsubArr_len = ARRAY_SIZE - (maxsubArr_len * (num_threads - 1));
                }
                subArrays[i] = new int[maxsubArr_len];

                for (int j=0; j < maxsubArr_len; j++)
                {
                    int x = j + k;
                    subArrays[i][j] = arrayMultiThread[x];
                    
                    Console.Write("{0} ", subArrays[i][j]);
                }
                Console.Write("\n");
            }
            MultiThreadTime.Start();
            Thread[] sub_threads = new Thread[num_threads];
            Console.Write("\n");

            for (int x = 0; x < num_threads; x++)
            {
                int m = x;
                sub_threads[x] = new Thread(() => MergeSort(subArrays[m]));
                sub_threads[x].Start();
            }

            for (int x = 0; x < num_threads; x++)
            {
                sub_threads[x].Join();
            }
            MultiThreadTime.Stop();

            //printing 2d array
            Console.Write("SORTED subarray of {0} total elements and {1} threads looks like:\n", ARRAY_SIZE, num_threads);
            maxsubArr_len = ARRAY_SIZE / num_threads;

            for (int i = 0; i < num_threads; i++)
            {
              //  k = i * maxsubArr_len;
                if (i == (num_threads - 1))
                {
                    maxsubArr_len = ARRAY_SIZE - (maxsubArr_len * (num_threads - 1));
                }
               // subArrays[i] = new int[maxsubArr_len];

                for (int j = 0; j < maxsubArr_len; j++)
                {
                    Console.Write("{0} ", subArrays[i][j]);
                }
                Console.Write("\n");
            }
            //end of printing 2d array

            Queue<int[]> Final_Arr = new Queue<int[]>();
            for (int x = 0; x < num_threads; x++)
            {
                Final_Arr.Enqueue(subArrays[x]);
            }
            int[] temp1 = new int[ARRAY_SIZE];
            while (Final_Arr.Count() > 1)
            {
                arrayMultiThread =Merge(Final_Arr.Dequeue(), Final_Arr.Dequeue(), temp1);
                Final_Arr.Enqueue(arrayMultiThread);
            }

            /*********************** Methods **********************
             *****************************************************/
            /*
            implement Merge method. This method takes two sorted array and
            and constructs a sorted array in the size of combined arrays
            */

            static int[] Merge(int[] LA, int[] RA, int[] A)
               {
                    int i = 0, j = 0, k = 0;

                    while (i < LA.Length && j < RA.Length)
                    {
                        if (LA[i] < RA[j])
                        {
                            A[k] = LA[i];
                            k++;
                            i++;
                        }
                        else
                        {
                            A[k] = RA[j];
                            k++;
                            j++;
                        }
                    }

                    if (i < LA.Length)
                    {
                        for (; i < LA.Length; i++)
                        {
                            A[k] = LA[i];
                            k++;
                        }
                    }
                    else if (j < RA.Length)
                    {
                        for (; j < RA.Length; j++)
                        {
                            A[k] = RA[j];
                            k++;
                        }
                    }
                return A;
               }



            /*
            implement MergeSort method: takes an integer array by reference
            and makes some recursive calls to intself and then sorts the array
            */
            static int[] MergeSort(int[] A)
               {
                if (A.Length < 2)
                    return A;

                int mid = A.Length / 2;
                int[] left = new int[mid];
                int[] right = new int[A.Length-mid];

                for (int i = 0; i < (mid); i++)
                    left[i] = A[i];
                int j = 0;
                for (int i = mid; i < (A.Length); i++)
                {
                    right[j] = A[i];
                    j++;
                }
                MergeSort(left);
                MergeSort(right);
                Merge(left,right,A);

                return A;
               }

   
            // a helper function to print your array
            static void PrintArray(int[] myArray)
            {
                Console.Write("[");
                for (int i = 0; i < myArray.Length; i++)
                {
                    Console.Write("{0} ", myArray[i]);

                }
                Console.Write("]");
                Console.WriteLine();

            }

            static void PrintTime(TimeSpan ts)
            {
                Console.WriteLine("RunTime: {0:00} min {1:00} sec {2:00} milisecond", ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.WriteLine();

            }

            // a helper function to confirm your array is sorted
            // returns boolean True if the array is sorted
            static bool IsSorted(int[] a)
            {
                int j = a.Length - 1;
                if (j < 1) return true;
                int ai = a[0], i = 1;
                while (i <= j && ai <= (ai = a[i])) i++;
                return i > j;
            }
        }


    }
}
