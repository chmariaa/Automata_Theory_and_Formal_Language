using System;

// Language: L = { w in {0,1}* | w ends in "00" }
// Demonstrates minimization: original 4-state DFA (q0,q1,q2,q3)
// minimizes to 3 states (q0,q1,q23) since q2 and q3 are equivalent.

class Program
{
    // ---------- ORIGINAL (non-minimal) DFA: 4 states ----------
    enum OriginalState { Q0, Q1, Q2, Q3 }

    static bool ProcessOriginal(string input)
    {
        OriginalState current = OriginalState.Q0;

        foreach (char symbol in input)
        {
            switch (current)
            {
                case OriginalState.Q0:
                    current = (symbol == '0') ? OriginalState.Q1 : OriginalState.Q0;
                    break;
                case OriginalState.Q1:
                    current = (symbol == '0') ? OriginalState.Q2 : OriginalState.Q0;
                    break;
                case OriginalState.Q2:
                    current = (symbol == '0') ? OriginalState.Q3 : OriginalState.Q0;
                    break;
                case OriginalState.Q3:
                    current = (symbol == '0') ? OriginalState.Q3 : OriginalState.Q0;
                    break;
            }
        }
        return current == OriginalState.Q2 || current == OriginalState.Q3;
    }

    // ---------- MINIMIZED DFA: 3 states ----------
    enum MinState { Q0, Q1, Q23 }

    static bool ProcessMinimized(string input)
    {
        MinState current = MinState.Q0;

        foreach (char symbol in input)
        {
            switch (current)
            {
                case MinState.Q0:
                    current = (symbol == '0') ? MinState.Q1 : MinState.Q0;
                    break;
                case MinState.Q1:
                    current = (symbol == '0') ? MinState.Q23 : MinState.Q0;
                    break;
                case MinState.Q23:
                    current = (symbol == '0') ? MinState.Q23 : MinState.Q0;
                    break;
            }
        }
        return current == MinState.Q23;
    }

    static void Main()
    {
        string[] testCases = {
            // Accepted (ends in "00")
            "00",
            "100",
            "000",
            "1100",
            "010100",
            // Rejected (does not end in "00")
            "",
            "0",
            "01",
            "101",
            "0010"
        };

        Console.WriteLine("==========================================");
        Console.WriteLine("  DFA: Ends in \"00\"  (Example 1)          ");
        Console.WriteLine("  Comparing Original (4 states) vs         ");
        Console.WriteLine("  Minimized (3 states) -- results match     ");
        Console.WriteLine("==========================================\n");

        foreach (string test in testCases)
        {
            bool originalResult = ProcessOriginal(test);
            bool minimizedResult = ProcessMinimized(test);
            string display = test == "" ? "(empty string)" : test;
            string match = originalResult == minimizedResult ? "OK" : "MISMATCH!";

            Console.WriteLine($"Input: {display,-15} Original: {(originalResult ? "ACCEPT" : "REJECT"),-7} " +
                               $"Minimized: {(minimizedResult ? "ACCEPT" : "REJECT"),-7} [{match}]");
        }
    }
}
