using System;
using System.Diagnostics;
using System.Threading;


namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {

            int ARRAY_SIZE = 100;
            int[] arraySingleThread = new int[ARRAY_SIZE];
            int num_processors = Environment.ProcessorCount;




            // TODO : Use the "Random" class in a for loop to initialize an array

            Random randNum = new Random();

            for (int i=0; i < ARRAY_SIZE; i++)
            {
                arraySingleThread[i] = randNum.Next(0, ARRAY_SIZE);
            }
      

            // copy array by value.. You can also use array.copy()
            int[] arrayMultiThread= new int[ARRAY_SIZE];
            arraySingleThread.CopyTo(arrayMultiThread, 0);

            //      PrintArray(arrayMultiThread);

            /*TODO : Use the  "Stopwatch" class to measure the duration of time that
               it takes to sort an array using one-thread merge sort and
               multi-thead merge sort
            */

            //TODO :start the stopwatch
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            MergeSort(arraySingleThread);

            //TODO :Stop the stopwatch
            stopWatch.Stop();
            PrintTime(stopWatch.Elapsed);  // Get the elapsed time as a TimeSpan value, passes it to the print time function

            

            //TODO: Multi Threading Merge Sort







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

                for (int i = 0; i < mid; i++)
                    left[i] = A[i];
                int j = 0;
                for (int i = mid; i < (A.Length - mid); i++)
                {
                    right[j] = A[i];
                    j++;
                }
                MergeSort(left);
                MergeSort(right);
                Merge(left,right,A);
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
