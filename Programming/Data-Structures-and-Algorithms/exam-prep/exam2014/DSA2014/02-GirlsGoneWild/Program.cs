﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    internal class Program
    {
        static int n;
        static char[] letters;
        static int k;

        private static void Main()
        {
            //Inputs.SetInput("000.001");
            //Inputs.SetInput("001");
            GetInput();
            Solve();
        }

        private static void Solve()
        {
            var numVars = Combinatorics.GetAllCombinationsWithoutRepetitions(n, 0, k-1);
            var l = letters.Length;
            var letVars = Combinatorics.GetAllVariationsWithoutRepetitions(n, 0, l-1);

            var results = new SortedSet<string>();
            foreach (var numVar in numVars)
            {
                foreach (var letVar in letVars)
                {
                    string result = "";
                    for (int i = 0; i < numVar.Length; i++)
                    {
                        //result += $@"{numVar[i]}{letters[letVar[i]]}-";
                        result += string.Format("{0}{1}-", numVar[i], letters[letVar[i]]);
                    }

                    results.Add(result.TrimEnd(new char[] { '-' }));
                }
            }

            Console.WriteLine(results.Count);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

        private static void GetInput()
        {
            k = int.Parse(Console.ReadLine());
            letters = Console.ReadLine().ToCharArray();
            n = int.Parse(Console.ReadLine());
        }


    }

    public static class Combinatorics
    {
        private static List<int[]> allSubsets;
        private static int[] currentSubsetArray;
        private static int countOfNumbersInSubset;
        private static int endSetNumber;

        private static int startSetNumber;

        private static Func<int, int, bool> isSubsetDuplicate;

        public static List<int[]> GetAllVariationsWithRepetitions(int countOfNumbersInVariation, int startNumber, int endNumber)
        {
            isSubsetDuplicate = (previousNumber, currentNumber) => { return false; };

            return GetAllSubsets(countOfNumbersInVariation, startNumber, endNumber);
        }

        public static List<int[]> GetAllVariationsWithoutRepetitions(int countOfNumbersInVariation, int startNumber, int endNumber)
        {
            isSubsetDuplicate = (previousNumber, currentNumber) => { return previousNumber == currentNumber; };

            return GetAllSubsets(countOfNumbersInVariation, startNumber, endNumber);
        }

        public static List<int[]> GetAllCombinationsWithRepetitions(int countOfNumbersInCombination, int startNumber, int endNumber)
        {
            if (endNumber <= startNumber)
            {
                throw new ArgumentException("Invalid set start - end range. Set start number must be smaller than set end number.");
            }

            isSubsetDuplicate = (previousNumber, currentNumber) => { return previousNumber > currentNumber; };

            return GetAllSubsets(countOfNumbersInCombination, startNumber, endNumber);
        }

        public static List<int[]> GetAllCombinationsWithoutRepetitions(int countOfNumbersInCombination, int startNumber, int endNumber)
        {
            isSubsetDuplicate = (previousNumber, currentNumber) => { return previousNumber >= currentNumber; };

            return GetAllSubsets(countOfNumbersInCombination, startNumber, endNumber);
        }

        public static List<int[]> GetAllPermutationsWithoutRepetitions(int startNumber, int endNumber)
        {
            isSubsetDuplicate = (previousNumber, currentNumber) => { return previousNumber == currentNumber; };

            int countOfNumbersInCombination = endNumber - startNumber + 1;
            return GetAllSubsets(countOfNumbersInCombination, startNumber, endNumber);
        }

        /// <summary>
        /// Returns all subsets of countOfNumbersInSubset numbers from a set of numbers, defined by all the integers in the range startSetNumber and endSetNumber inclusive. Uses the isSubsetDuplicate delegate to filter the subsets.
        /// </summary>
        /// <param name="countOfNumbersInSubset">The count of numbers in each subset.</param>
        /// <returns></returns>
        private static List<int[]> GetAllSubsets(int countOfNumbersInSubset, int startNumber, int endNumber)
        {
            Combinatorics.countOfNumbersInSubset = countOfNumbersInSubset;
            startSetNumber = startNumber;
            endSetNumber = endNumber;
            currentSubsetArray = new int[countOfNumbersInSubset];
            allSubsets = new List<int[]>();

            GenerateSubsets(0);
            return allSubsets;
        }

        private static void GenerateSubsets(int currentSubsetIndex)
        {
            if (currentSubsetIndex == countOfNumbersInSubset)
            {
                allSubsets.Add((int[])currentSubsetArray.Clone());
                return;
            }

            for (int currentNumber = startSetNumber; currentNumber <= endSetNumber; currentNumber++)
            {
                bool isDuplicateSubset = false;

                for (int previousNumberIndex = currentSubsetIndex - 1; previousNumberIndex >= 0; previousNumberIndex--)
                {
                    var previousNumber = currentSubsetArray[previousNumberIndex];

                    if (isSubsetDuplicate(previousNumber, currentNumber))
                    {
                        isDuplicateSubset = true;
                        break;
                    }
                }

                if (isDuplicateSubset)
                {
                    continue;
                }

                currentSubsetArray[currentSubsetIndex] = currentNumber;

                GenerateSubsets(currentSubsetIndex + 1);
            }
        }
    }
}