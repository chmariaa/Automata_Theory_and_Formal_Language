using System;

// Language: L = { w in {a,b}* | w has an odd number of a's }
// Demonstrates minimization: original 4-state DFA (p0,p1,p2,p3)
// minimizes to just 2 states (EVEN, ODD) since p0=p2 and p1=p3.

class Program
{
    // ---------- ORIGINAL (non-minimal) DFA: 4 states ----------
    enum OriginalState { P0, P1, P2, P3 }

    static bool ProcessOriginal(string input)
    {
        OriginalState current = OriginalState.P0;

        foreach (char symbol in input)
        {
            switch (current)
            {
                case OriginalState.P0:
                    current = (symbol == 'a') ? OriginalState.P1 : OriginalState.P2;
                    break;
                case OriginalState.P1:
                    current = (symbol == 'a') ? OriginalState.P0 : OriginalState.P3;
                    break;
                case OriginalState.P2:
                    current = (symbol == 'a') ? OriginalState.P3 : OriginalState.P0;
                    break;
                case OriginalState.P3:
                    current = (symbol == 'a') ? OriginalState.P2 : OriginalState.P1;
                    break;
            }
        }
        return current == OriginalState.P1 || current == OriginalState.P3;
    }

    // ---------- MINIMIZED DFA: 2 states ----------
    enum MinState { Even, Odd }

    static bool ProcessMinimized(string input)
    {
        MinState current = MinState.Even;

        foreach (char symbol in input)
        {
            if (symbol == 'a')
                current = (current == MinState.Even) ? MinState.Odd : MinState.Even;
            // symbol 'b' does not change the state
        }
        return current == MinState.Odd;
    }

    static void Main()
    {
        string[] testCases = {
            // Accepted (odd number of a's)
            "a",
            "aaa",
            "ba",
            "baaab",
            "bbbab",
            // Rejected (even number of a's, including zero)
            "",
            "b",
            "aa",
            "bbbb",
            "aab"
        };

        Console.WriteLine("==========================================");
        Console.WriteLine("  DFA: Odd Number of 'a's  (Example 2)    ");
        Console.WriteLine("  Comparing Original (4 states) vs         ");
        Console.WriteLine("  Minimized (2 states) -- results match     ");
        Console.WriteLine("==========================================\n");

        foreach (string test in testCases)
        {
            bool originalResult = ProcessOriginal(test);
            bool minimizedResult = ProcessMinimized(test);
            string display = test == "" ? "(empty string)" : test;
            string match = originalResult == minimizedResult ? "OK" : "MISMATCH!";

            Console.WriteLine($"Input: {display,-10} Original: {(originalResult ? "ACCEPT" : "REJECT"),-7} " +
                               $"Minimized: {(minimizedResult ? "ACCEPT" : "REJECT"),-7} [{match}]");
        }
    }
}
