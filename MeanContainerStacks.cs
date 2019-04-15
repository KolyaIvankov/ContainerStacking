using System;
using System.Collections.Generic;
using System.Text;

namespace Containers
{
    /**
     * Created by Nikolay Ivankov on 15.04.2019
     * 
     * This code is a supplement to the article
     * It is desigend to calculate the expected number of container stacks when reloading containers
     * from a train of length N to a ship in stacks up to k containers heigh by placing lighter on heivaier.
     * 
     * The code is free to use and modify. The autor will be grateful if his name is at least mentioned, though. 
     **/
    class MeanContainerStacks
    {
        static void Main(string[] args)
        {
            DateTime start = System.DateTime.Now;
            int containers_total_num = 30000;
            int containers_max_stack = 15;
            decimal[,] subevents = Subevent(containers_total_num, containers_max_stack);
            decimal[] allstacksmeans = new decimal[containers_total_num];
            decimal[] kstackmeans = new decimal[containers_total_num];
            for (int i = 0; i < containers_max_stack; i++)
            {
                kstackmeans[i] = 0;
                allstacksmeans[i] = 1;
                for (int j = 0; j < i; j++)
                {
                    allstacksmeans[i] += allstacksmeans[i - j - 1] * subevents[i, j];
                }
            }
            kstackmeans[containers_max_stack - 1] = (decimal)1 / factorial(containers_max_stack);
            for (int i = containers_max_stack; i < containers_total_num; i++)
            {

                decimal nonevent = 0;
                allstacksmeans[i] = 1;
                for (int j = 0; j < containers_max_stack - 1; j++)
                {
                    allstacksmeans[i] += allstacksmeans[i - j - 1] * subevents[i, j];
                    nonevent += subevents[i, j];
                }
                allstacksmeans[i] += (1 - nonevent) * allstacksmeans[i - containers_max_stack];
                kstackmeans[i] = (1 - nonevent) * (1 + kstackmeans[i - containers_max_stack]) + nonevent * kstackmeans[i - 1];
 //               Console.Write("Numer of containers " + (i + 1) + "; expected number of stacks " + Math.Round(allstacksmeans[i]) +
 //                   "; expected number of k-stacks " + Math.Round(kstackmeans[i]) + "\n");
              
            }
            Console.WriteLine("Completed in " + (System.DateTime.Now.Subtract(start).TotalMilliseconds) + " s. Press any key");
            Console.Read();
        }
    
    

    static decimal[,] Subevent(int N, int k)
        {
            decimal[,] result = new decimal[N + 1, k];
            result[0, 0] = 1;
            for (int i = 1; i < N + 1; i++)
            {
                result[i, 0] = (decimal)(1) / (i + 1);
            }
            for (int i = 1; i < k; i++)
            {
                result[i, i] = result[i - 1, i - 1] / (i + 1);
            }
            for (int j = 1; j < k; j++)
            {
                for (int i = 1; i < N + 1; i++)
                {
                    result[i, j] = result[i - 1, j] + ((decimal)(1) / (i + 1)) * (result[i - 1, j - 1] - result[i - 1, j]);
                }
            }
            return result;
        }

        static decimal factorial(int k)
        {
            decimal result = 1;
            for (int i = 1; i < k + 1; i++)
            {
                result *= i;
            }
            return result;
        }


    }
}
