using System;
using System.Collections.Generic;

namespace MinimizationDfa
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("            DFA MINIMIZATION DEMONSTRATION & TESTER              ");
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("  [1] Example 1: Language Ends in \"00\"");
                Console.WriteLine("  [2] Example 2: Language Has Odd Number of 'a's");
                Console.WriteLine("  [3] Run ALL Demonstrations & Test Suites");
                Console.WriteLine("  [4] Interactive Custom String Tester");
                Console.WriteLine("  [0] Exit");
                Console.Write("\nEnter your choice (0-4): ");

                string? choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        RunExampleEndsIn00();
                        break;
                    case "2":
                        RunExampleOddNumberOfAs();
                        break;
                    case "3":
                        RunAllExamples();
                        break;
                    case "4":
                        RunCustomTester();
                        break;
                    case "0":
                        Console.WriteLine("Exiting program. Adios");
                        return;
                    default:
                        Console.WriteLine("Invalid option! Press any key to try again...");
                        Console.WriteLine();
                        continue;
                }

                Console.WriteLine("\nPress Enter to return to the main menu...");
                Console.ReadLine();
                Console.WriteLine();
            }
        }

        static void RunAllExamples()
        {
            RunExampleEndsIn00();
            Console.WriteLine("\n" + new string('-', 65) + "\n");
            RunExampleOddNumberOfAs();
        }

        // ex 1 ends in "00"
        enum Ex1OriginalState { Q0, Q1, Q2, Q3 }
        enum Ex1MinState { Q0, Q1, Q23 }

        static bool ProcessEx1Original(string input)
        {
            Ex1OriginalState current = Ex1OriginalState.Q0;
            foreach (char symbol in input)
            {
                switch (current)
                {
                    case Ex1OriginalState.Q0:
                        current = (symbol == '0') ? Ex1OriginalState.Q1 : Ex1OriginalState.Q0;
                        break;
                    case Ex1OriginalState.Q1:
                        current = (symbol == '0') ? Ex1OriginalState.Q2 : Ex1OriginalState.Q0;
                        break;
                    case Ex1OriginalState.Q2:
                        current = (symbol == '0') ? Ex1OriginalState.Q3 : Ex1OriginalState.Q0;
                        break;
                    case Ex1OriginalState.Q3:
                        current = (symbol == '0') ? Ex1OriginalState.Q3 : Ex1OriginalState.Q0;
                        break;
                }
            }
            return current == Ex1OriginalState.Q2 || current == Ex1OriginalState.Q3;
        }

        static bool ProcessEx1Minimized(string input)
        {
            Ex1MinState current = Ex1MinState.Q0;
            foreach (char symbol in input)
            {
                switch (current)
                {
                    case Ex1MinState.Q0:
                        current = (symbol == '0') ? Ex1MinState.Q1 : Ex1MinState.Q0;
                        break;
                    case Ex1MinState.Q1:
                        current = (symbol == '0') ? Ex1MinState.Q23 : Ex1MinState.Q0;
                        break;
                    case Ex1MinState.Q23:
                        current = (symbol == '0') ? Ex1MinState.Q23 : Ex1MinState.Q0;
                        break;
                }
            }
            return current == Ex1MinState.Q23;
        }

        static void RunExampleEndsIn00()
        {
            Console.WriteLine(" \nEXAMPLE 1: Language L = { w in {0,1}* | w ends in \"00\" }");
            Console.WriteLine(" Minimization: Original 4 states (Q0,Q1,Q2,Q3) -> 3 states (Q0,Q1,Q23)");


            string[] testCases = { "00", "100", "000", "1100", "010100", "", "0", "01", "101", "0010" };

            foreach (string test in testCases)
            {
                bool orig = ProcessEx1Original(test);
                bool min = ProcessEx1Minimized(test);
                string display = test == "" ? "(empty string)" : test;
                string match = (orig == min) ? "OK" : "MISMATCH!";

                Console.WriteLine($"Input: {display,-15} | Original: {(orig ? "ACCEPT" : "REJECT"),-7} | Minimized: {(min ? "ACCEPT" : "REJECT"),-7} [{match}]");
            }
        }

        // ex 2 Odd Number of 'a's
        enum Ex3OriginalState { P0, P1, P2, P3 }
        enum Ex3MinState { Even, Odd }

        static bool ProcessEx3Original(string input)
        {
            Ex3OriginalState current = Ex3OriginalState.P0;
            foreach (char symbol in input)
            {
                switch (current)
                {
                    case Ex3OriginalState.P0:
                        current = (symbol == 'a') ? Ex3OriginalState.P1 : Ex3OriginalState.P2;
                        break;
                    case Ex3OriginalState.P1:
                        current = (symbol == 'a') ? Ex3OriginalState.P0 : Ex3OriginalState.P3;
                        break;
                    case Ex3OriginalState.P2:
                        current = (symbol == 'a') ? Ex3OriginalState.P3 : Ex3OriginalState.P0;
                        break;
                    case Ex3OriginalState.P3:
                        current = (symbol == 'a') ? Ex3OriginalState.P2 : Ex3OriginalState.P1;
                        break;
                }
            }
            return current == Ex3OriginalState.P1 || current == Ex3OriginalState.P3;
        }

        static bool ProcessEx3Minimized(string input)
        {
            Ex3MinState current = Ex3MinState.Even;
            foreach (char symbol in input)
            {
                if (symbol == 'a')
                {
                    current = (current == Ex3MinState.Even) ? Ex3MinState.Odd : Ex3MinState.Even;
                }
            }
            return current == Ex3MinState.Odd;
        }

        static void RunExampleOddNumberOfAs()
        {
            Console.WriteLine(" \nEXAMPLE 2: Language L = { w in {a,b}* | w has odd number of 'a's }");
            Console.WriteLine(" Minimization: Original 4 states (P0,P1,P2,P3) -> 2 states (Even, Odd)");
            Console.WriteLine(" Equivalent sets: {P0,P2} = Even 'a's, {P1,P3} = Odd 'a's (Accept)");

            string[] testCases = { "a", "aaa", "ba", "baaab", "bbbab", "", "b", "aa", "bbbb", "aab" };

            foreach (string test in testCases)
            {
                bool orig = ProcessEx3Original(test);
                bool min = ProcessEx3Minimized(test);
                string display = test == "" ? "(empty string)" : test;
                string match = (orig == min) ? "OK" : "MISMATCH!";

                Console.WriteLine($"Input: {display,-15} | Original: {(orig ? "ACCEPT" : "REJECT"),-7} | Minimized: {(min ? "ACCEPT" : "REJECT"),-7} [{match}]");
            }
        }

        // input tester
        static void RunCustomTester()
        {
            Console.WriteLine("               \nINTERACTIVE CUSTOM TESTER                 ");
            Console.WriteLine("\nSelect DFA to test:");
            Console.WriteLine("  1. Ends in \"00\" (Alphabet: 0, 1)");
            Console.WriteLine("  2. Odd Number of 'a's (Alphabet: a, b)");
            Console.Write("Choice: ");
            string? dfaChoice = Console.ReadLine()?.Trim();

            Console.Write("Enter custom input string: ");
            string input = Console.ReadLine() ?? "";

            Console.WriteLine();
            if (dfaChoice == "1")
            {
                bool orig = ProcessEx1Original(input);
                bool min = ProcessEx1Minimized(input);
                Console.WriteLine($"[DFA: Ends in \"00\"] Input: \"{input}\"");
                Console.WriteLine($"  Original DFA (4 states):  {(orig ? "ACCEPT" : "REJECT")}");
                Console.WriteLine($"  Minimized DFA (3 states): {(min ? "ACCEPT" : "REJECT")}");
            }
            else if (dfaChoice == "2")
            {
                bool orig = ProcessEx3Original(input);
                bool min = ProcessEx3Minimized(input);
                Console.WriteLine($"[DFA: Odd Number of 'a's] Input: \"{input}\"");
                Console.WriteLine($"  Original DFA (4 states):  {(orig ? "ACCEPT" : "REJECT")}");
                Console.WriteLine($"  Minimized DFA (2 states): {(min ? "ACCEPT" : "REJECT")}");
            }
            else
            {
                Console.WriteLine("Invalid selection!");
            }
        }
    }
}
