/*
 * FILE             :           Program.cs
 * PROJECT          :           PROG3126
 * PROGRAMMER       :           Mohamed Halbouni
 * FIRST VERSION    :           2022-01-28
 * DESCRIPTION      :           This is the main execution point of the entire system.
 */





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace passwordCrack
{
    internal class Program
    {
        static Password bruteForce = new Password();


        static void Main(string[] args)
        {

            bruteForce.GetUserInput();         

            bruteForce.ParallelIteration();

            bruteForce.SequentialIteration(); 

            bruteForce.ShowResult();

            Console.ReadKey();

        }
        

    }
}
