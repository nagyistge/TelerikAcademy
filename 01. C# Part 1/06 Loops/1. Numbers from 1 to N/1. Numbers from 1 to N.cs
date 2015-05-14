﻿using System;

//Problem 1. Numbers from 1 to N
//• Write a program that enters from the console a positive integer 
//n  and prints all the numbers from  1  to  n , on a single line, separated by a space.

//Examples:
//n       output
//3       1 2 3 
//5       1 2 3 4 5 

class Numbers_from_1_to_N
{
    static void Main()
    {
        Console.Write("Enter an integer (0 to 4,294,967,295): ");
        uint n = uint.Parse(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            Console.Write(i + " ");
        }
    }
}

