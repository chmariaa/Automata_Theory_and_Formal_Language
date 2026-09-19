using System;

// Language: L = { w in {0,1}* | w contains exactly one '1' }
// This is the minimized version of the board example:
// Original states A,B,C,D,E,F  -->  Minimized states AB, CDE, F

class Program
{
    enum State { AB, CDE, F }

    static bool ProcessDfa(string input)
    {
        State current = State.AB; // start state

        foreach (char symbol in input)
        {
            switch (current)
            {
                case State.AB:
                    current = (symbol == '0') ? State.AB : State.CDE;
                    break;
                case State.CDE:
                    current = (symbol == '0') ? State.CDE : State.F;
                    break;
                case State.F:
                    current = State.F; // trap state, stays forever
                    break;
            }
        }

        return current == State.CDE; // only CDE is accepting
    }

    static void Main()
    {
        string[] testCases = {
            // Accepted (exactly one '1')
            "1",
            "01",
            "10",
            "0001000",
            "000100000",
            // Rejected (zero ones, or two or more ones)
            "",
            "0000",
            "11",
            "101",
            "010101"
        };

        Console.WriteLine("==========================================");
        Console.WriteLine("  DFA: Exactly One '1'  (Board Example)   ");
        Console.WriteLine("==========================================\n");

        foreach (string test in testCases)
        {
            bool accepted = ProcessDfa(test);
            string display = test == "" ? "(empty string)" : test;
            Console.WriteLine($"Input: {display,-20} -> {(accepted ? "[ACCEPTED]" : "[REJECTED]")}");
        }
    }
}
