/*
 * FILE             :           Password.cs
 * PROJECT          :           PROG3126
 * PROGRAMMER       :           Mohamed Halbouni
 * FIRST VERSION    :           2022-01-28
 * DESCRIPTION      :           this is a class containing
 *                              1) ShowResult() to print out all the result.
 *                              2) ValidateUserInput() to validate the userInput for password.
 *                              3) ValidateNumOfSeconds() to validate the userInput for number of milliseconds.
 *                              4) SequentialIteration() to execute the Sequential loop to find the password.
 *                              5) ParallelIteration() to partition the data for the parallel loop function.
 *                              6) GetUserPassword() to prompt the user for input.
 *                              7) ParallelIterationSolver() contains the parallel loop
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
 

namespace passwordCrack
{
    internal class Password
    {
        //PRIVATE DATA MEMBERS.
        private string userPassword;
        private int sequentialCount;
        private int numOfSeconds;
        private int parallelCount;
        private double parallelTimer;
        private double sequentialTimer;



        //GLOBAL VARIABLES
        public bool tmFlag = false;
        public bool isSolved = false;
        public int timerFlag = 0;           //1 if timer exceeded for Sequential loop
                                            


        //SETTERS AND GETTERS
        public string UserPassword { get { return userPassword; } set { userPassword = value; } }
        public int SequentialCount { get { return sequentialCount; } set { sequentialCount = value; } }
        public int NumOfSeconds { get { return numOfSeconds; } set { numOfSeconds = value; } }
        public int ParallelCount { get { return parallelCount; } set { parallelCount = value; } }
        public double ParallelTimer { get { return parallelTimer;} set { parallelTimer = value; } }
        public double SequentialTimer { get { return sequentialTimer;} set { sequentialTimer = value; } }






        // FUNTION          :           ValidateUserInput() 
        // DESCRIPTION      :           This function is to validate the user input for password.
        //                              make sure the user input is between 6 and 16 digits long.
        //                              make sure the user input is all digits from 0 to 9.
        // PARAMETERS       :           argUserInput - userInput for password.
        // RETURNS          :           bool true if valid false if invalid
        public bool ValidateUserInput(string argUserInput)
        {
            bool isValid = true;

            int isDigit;
            int howManyChar;

            howManyChar = argUserInput.Length;
            if (howManyChar >= 6 && howManyChar <= 18) //if the userinput is between 6 and 18 characters long
            {
                //then validate and check if they are all integers.
                foreach (var c in argUserInput)
                {
                    if (!int.TryParse(c.ToString(), out isDigit))
                    {
                        isValid = false;
                        break;
                    }
                }
                
            }
            else
            {
                isValid = false;
                
            }

            if(isValid == true)
            {
                UserPassword = argUserInput; //if the password is valid then save it.
            }

            return isValid;
        }



        // FUNTION          :           ValidateNumOfSeconds() 
        // DESCRIPTION      :           This function is to validate the user input for milliseconds.
        //                              make sure the user input is digits.
        // PARAMETERS       :           argUserInput - the user input for milliseconds.
        // RETURNS          :           bool true if valid false if invalid
        public bool ValidateNumOfSeconds(string argUserInput)
        {
            bool isDigit = false;
            int value;
            //validating if the seconds are integers.
            if (int.TryParse(argUserInput, out value))
            {
                isDigit = true;
            }
            return isDigit;
        }


        // FUNTION          :           GetUserPassword() 
        // DESCRIPTION      :           This function is to prompt the user for password.
        // PARAMETERS       :           None
        // RETURNS          :           String userInput - the user Input.
        public string GetUserPassword()
        {
            string userInput;

            Console.WriteLine("Enter your password");
            Console.WriteLine("1. Must be digits from 0 to 9.");
            Console.WriteLine("2. Must be within 6 to 18 digits long.");

            Console.Write("Password: ");
            userInput = Console.ReadLine();

            //Console.Clear();
            return userInput;
        }



        // FUNTION          :           GetNumOfSecond() 
        // DESCRIPTION      :           This function is to prompt the user for milliseconds.
        // PARAMETERS       :           None
        // RETURNS          :           String userInput - the user Input.
        public string GetNumOfSecond()
        {
            string userInput;

            Console.Write("Please enter time limit in milliseconds: ");
            userInput = Console.ReadLine();

            //Console.Clear();
            return userInput;
        }



        // FUNTION          :           GetUserInput() 
        // DESCRIPTION      :           This function is to pass the user input to the appropriate functions
        //                              to be validated.
        // PARAMETERS       :           None
        // RETURNS          :           None
        public void GetUserInput()
        {
            bool isValid;
            bool isDigit;

            string passwordTemp;
            string seconds;

            passwordTemp = GetUserPassword();
            isValid = ValidateUserInput(passwordTemp);
            while (isValid != true) //keep getting the user input until it is valid.
            {
                //Console.Clear();
                passwordTemp = GetUserPassword();
                isValid = ValidateUserInput(passwordTemp);               
            }
            userPassword = passwordTemp;

            seconds = GetNumOfSecond();
            isDigit = ValidateNumOfSeconds(seconds);
            while (isDigit == false) //keep getting the user input until it is valid.
            {
               // Console.Clear();
                seconds = GetNumOfSecond();
                isDigit = ValidateNumOfSeconds(seconds);             
            }
            NumOfSeconds = Int32.Parse(seconds);
            Console.Clear();
        }




        // FUNTION          :           SequentialIteration() 
        // DESCRIPTION      :           This function to do the sequential iteration to find the password.
        // PARAMETERS       :           None
        // RETURNS          :           None
        public void SequentialIteration()
        {
            long crackPassword = 0;
            int minLen = 6;
            int maxLen = 18;
            string temp;
            int tempLen;
            string currentPassword;
            bool passwordCracked = false;
            int sumLen;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for(crackPassword = 0; crackPassword < long.MaxValue; crackPassword++)
            {
                //Every time the crackPassword increment.
                //save it as a string into the currentPassword.
                currentPassword = crackPassword.ToString();
                //temporary length of the currentPassword to decide how many 0 to add.
                tempLen = currentPassword.Length;
                sumLen = minLen - tempLen;


                
                for(int i = sumLen; i <= maxLen - tempLen; i++)
                {
                    //temp string to contain the digit as a string.
                    temp = currentPassword;
                    //loop for adding 0 at the begining of the string.
                    for(int j = 0; j < i; j++)
                    {
                        temp = temp.Insert(0, "0");
                    }

                    if(temp == UserPassword)  // if password is found break from the inner loop
                    {
                        passwordCracked = true; 
                        break;
                    }

                    if(passwordCracked == true)
                    {
                        break;
                    }
                    if(sw.ElapsedMilliseconds >= NumOfSeconds) //if the sw milliseconds exceeded the seconds from user entry
                    {
                        break;  //break from the loop.
                    }

                    SequentialCount++; //counter to count the Sequential loop tries
                }
                if(passwordCracked == true) // break from the outer loop if the condition is true
                {
                    break;
                }
                if (sw.ElapsedMilliseconds >= NumOfSeconds) // break if it exceeds the time limit and set the flag to 2
                {
                    timerFlag = 2;  // this flag is to decide on what to print in the showResult().
                    break;
                }

            }
            sw.Stop();
            SequentialTimer = sw.ElapsedMilliseconds;

        }



        // FUNTION          :           ParallelIteration() 
        // DESCRIPTION      :           This function is to do the partition between threads
        //                              and start the parallel loop
        // PARAMETERS       :           None
        // RETURNS          :           None
        public void ParallelIteration()
        {
            bool solved = false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Parallel.For(0, 11, (i, state) =>
            {
                //diving the task for each threads depending on the length of guessing iteration
                //if any of them found the password first then State.stop() all of the threads.
                switch(i)
                {
                    case 0:
                    case 1:
                    case 2:
                        solved = ParallelIterationSolver(6, 8); //case 0 1 2, will try to find the password in between the length
                                                                // 6 and 8 long
                        if (solved == true)
                        {
                            state.Stop();
                        }
                        break;
                    case 3:
                    case 4:
                    case 5:
                        solved = ParallelIterationSolver(9, 11);//case 3 4 5, will try to find the password in between the length
                                                                // 9 and 11 long
                        if (solved == true)
                        {
                            state.Stop();
                        }
                        break;
                    case 6:
                    case 7:
                    case 8:
                        solved = ParallelIterationSolver(12, 14);//case 6 7 8, will try to find the password in between the length
                                                                 // 12 and 14 long

                        if (solved == true)
                        {
                            state.Stop();
                        }
                        break;

                    case 9:
                    case 10:
                    case 11:
                        solved = ParallelIterationSolver(15, 18);//case 9 10 11, will try to find the password in between the length
                                                                 // 15 and 18 long
                        if (solved == true)
                        {
                            state.Stop();
                        }
                        break;
                }
            });
            sw.Stop();
            ParallelTimer = sw.ElapsedMilliseconds;

        }





        // FUNTION          :           ParallelIterationSolver() 
        // DESCRIPTION      :           This function is to do the iteration to find the password for parallel loop.
        // PARAMETERS       :           int minLen - the minimin number of 0 to be added.
        //                              int maxLen - the maximum number of 0 to be added.
        // RETURNS          :           bool - true if password is found and false if password is not found.
        public bool ParallelIterationSolver(int minLen, int maxLen)
        {
            long crackPassword = 0;             //incrementing the digits in the string.
            string temp;                        //storing the current password string.
            int tempLen = 0;                    //calculating the current password length
            string currentPassword;

            int x;                              

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (crackPassword = 0; crackPassword < long.MaxValue; crackPassword++)
            {
                //Every time the crackPassword increment.
                //save it as a string into the currentPassword.
                currentPassword = crackPassword.ToString();
                //temporary length of the currentPassword to decide how many 0 to add.
                tempLen = currentPassword.Length;
                x = minLen - tempLen;


                for (int i = x; i <= maxLen - tempLen; i++)
                {
                    //temp string to contain the digit as a string.
                    temp = currentPassword;
                    //loop for adding 0 at the begining of the string.
                    for (int j = 0; j < i; j++)
                    {
                        temp = temp.Insert(0, "0"); //insert 0 to the begining of the string depend on the min
                    }

                    if (temp == UserPassword) // if password is found set the isSolved to true
                    {
                        isSolved = true;
                    }

                    if (isSolved == true) // if password is found, break from the inner loop.
                    {
                        break;
                    }

                    if (sw.ElapsedMilliseconds >= NumOfSeconds) //Exit the inner loop as soon as the timer is more than number of seconds.
                    {
                        tmFlag = true;
                        break;
                    }
                    ParallelCount++;
                }

                if (isSolved == true) //if password is solved break from the outer loop.          
                {
                    break;
                }
                if (sw.ElapsedMilliseconds >= NumOfSeconds) //Exit the loop as soon as the timer is more than number of seconds.
                {
                    tmFlag = true;
                    break;
                }
            }
            return isSolved;
        }



        // FUNTION          :           ShowResult() 
        // DESCRIPTION      :           This function is to show the result at the end of both iterations.
        // PARAMETERS       :           None
        // RETURNS          :           None
        public void ShowResult()
        {
            //Console.Clear();
            Console.WriteLine("The password you have entered: \t\t" + UserPassword);
            Console.WriteLine("The time limit you have entered: \t" + NumOfSeconds);
            for(int i = 0; i < 30; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Brute Force for Parallel loop");
            if(tmFlag == true)
            {
                Console.WriteLine("Time limit exceeded before finding the password");
            }
            else
            {
                Console.WriteLine("Time it took to find the password: \t" + ParallelTimer + " ms");
                Console.WriteLine("Number of trials to find the password:  " + ParallelCount);
            }
            

            for (int i = 0; i < 30; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Brute Force for Sequential loop");
            if(timerFlag == 2)
            {
                Console.WriteLine("Time limit exceeded before finding the password");
            }
            else
            {
                Console.WriteLine("Time it took to find the password: \t" + SequentialTimer + " ms");
                Console.WriteLine("Number of trials to find the password:  " + SequentialCount);
            }
            


            Console.WriteLine();
        }

    }
}
