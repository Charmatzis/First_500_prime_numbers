using System;

namespace First_500_prime_numbers
{
    public static class PrimeTool
    {
        //From dotnetperls.com
        public static bool IsPrime(int candidate)
        {
            
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return candidate != 1;
        }


        static int[] primes_small= new int[]{ 0, 2, 3, 5, 7, 11 };

       public static int nth_prime_upper(int n)
        {
            double fn = (double)n;
            double flogn, flog2n, upper;
            if (n < 6) return primes_small[n];
            flogn = Math.Log(n);
            flog2n = Math.Log(flogn);

            if (n >= 688383)    /* Dusart 2010 page 2 */
                upper = fn * (flogn + flog2n - 1.0 + ((flog2n - 2.00) / flogn));
            else if (n >= 178974)    /* Dusart 2010 page 7 */
                upper = fn * (flogn + flog2n - 1.0 + ((flog2n - 1.95) / flogn));
            else if (n >= 39017)    /* Dusart 1999 page 14 */
                upper = fn * (flogn + flog2n - 0.9484);
            else                    /* Modified from Robin 1983 for 6-39016 _only_ */
                upper = fn * (flogn + 0.6000 * flog2n);

           

            return (int)upper;
        }


    }

}