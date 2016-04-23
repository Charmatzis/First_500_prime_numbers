using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace First_500_prime_numbers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Prime Numbers Console App");

            //Set Watch
            var stopWatch = new Stopwatch();
            int N = 500;
            
            Console.WriteLine($"Ceilling {PrimeTool.nth_prime_upper(N)}");


            //Find the first 500 prime numbers from PLINQ and class
            Console.WriteLine("");
            Console.WriteLine("RAW");
            stopWatch.Restart();
            Console.WriteLine(Raw.numbers);
            stopWatch.Stop();

            string[] numbers = Raw.numbers.Split(' ');
            List<int> numb = new List<int>();
            for (int i = 0; i < numbers.Count(); i++)
            {
                numb.Add(int.Parse(numbers[i]));
            }
            Console.WriteLine($"The count is {numb.Count()}");
            Console.WriteLine("Seq time:" + stopWatch.Elapsed.TotalMilliseconds / 1000);

            //Find the fisrt 500 prime numbers with class
            Console.WriteLine("");
            Console.WriteLine("First 500 prime numbers with class");
            stopWatch.Restart();
            Console.WriteLine(FirstPrimeNumbers(500));
            stopWatch.Stop();
            Console.WriteLine("Seq time:" + stopWatch.Elapsed.TotalMilliseconds / 1000);

            //Find the first 500 prime numbers from PLINQ
            Console.WriteLine("");
            Console.WriteLine("First 500 prime numbers with PLINQ");

            stopWatch.Restart();
            IEnumerable<int> results = FirstPrivateNumbers(500);
            stopWatch.Stop();
            int count = results.Count();
            Console.WriteLine($"Count is {count}");
            stopWatch.Start();
            Console.WriteLine(CreateStringFromEnum(results, count));
            stopWatch.Stop();
            Console.WriteLine("Seq time:" + stopWatch.Elapsed.TotalMilliseconds / 1000);

            //Find the first 500 prime numbers Eratosthenes Sieve
            Console.WriteLine("");
            Console.WriteLine("Eratosthenes Sieve");
            stopWatch.Restart();
            Console.WriteLine(EratoshenesSieveString(500));
            stopWatch.Stop();
            Console.WriteLine("Seq time:" + stopWatch.Elapsed.TotalMilliseconds / 1000);

            //Find the first 500 prime numbers Euler Sieve
            Console.WriteLine("");
            Console.WriteLine("Euler Sieve");
            stopWatch.Restart();
            Console.WriteLine(EulerSieveString(500));
            stopWatch.Stop();
            Console.WriteLine("Seq time:" + stopWatch.Elapsed.TotalMilliseconds / 1000);

            Console.ReadKey();
        }

        private static string FirstPrimeNumbers(int count)
        {
            StringBuilder sb = new StringBuilder();
            int i = 2;
            int k;
            for (k = 1; k <= count;)
            {
                bool prime = PrimeTool.IsPrime(i);
                if (prime)
                {
                    sb.Append(" " + i);
                    k++;
                }
                i++;
            }
            Console.WriteLine($"The count is {k - 1}");
            return sb.ToString();
        }

        private static IEnumerable<int> FirstPrivateNumbers(int N)
        {
            return Enumerable.Range(2, PrimeTool.nth_prime_upper(N))
                            .AsParallel()
                            .WithDegreeOfParallelism(Environment.ProcessorCount)     
                            .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                            .WithMergeOptions(ParallelMergeOptions.NotBuffered) // remove order dependancy
                            .Where(x => PrimeTool.IsPrime(x))
                            .TakeWhile((n, index) => index < N)
                            .OrderBy(x => x).Take(500);
        }

        private static string CreateStringFromEnum(IEnumerable<int> enumm, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(" " + enumm.ElementAt(i));
            }
            return sb.ToString();
        }

        private static string EratoshenesSieveString(int values)
        {
            StringBuilder sb = new StringBuilder();
            int maxNumber = PrimeTool.nth_prime_upper(values);
            bool[] is_prime = Eratosthenes.MakeSieve(maxNumber);
            int count = 1;
            for (int i = 2; i < maxNumber; i++)
            {
                if (is_prime[i] && count<= values)
                {
                    sb.Append(" " + i);
                    count++;
                }
            }
            Console.WriteLine($"Count is {count - 1}");
            return sb.ToString();
        }

        private static string EulerSieveString(int values)
        {
            StringBuilder sb = new StringBuilder();
            int maxNumber = PrimeTool.nth_prime_upper(values);
            bool[] is_prime = Euler.MakeSieve(maxNumber);
            int count = 1;
            for (int i = 2; i < maxNumber; i++)
            {
                if (is_prime[i] && count <= values)
                {
                    sb.Append(" " + i);
                    count++;
                }
            }
            Console.WriteLine($"Count is {count - 1}");
            return sb.ToString();
        }
    }
}