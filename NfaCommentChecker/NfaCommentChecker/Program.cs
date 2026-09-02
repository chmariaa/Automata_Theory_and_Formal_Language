using System;

class Program
{
    // NFA states
    const int Q0 = 1 << 0; // Start 
    const int Q1 = 1 << 1; // Slash '/'
    const int Q2 = 1 << 2; // Inside comment body
    const int Q3 = 1 << 3; // Slash '*' inside comment
    const int Q4 = 1 << 4; // Accepting 

    static bool ProcessNfa(string input)
    {
        int currentStates = Q0;

        foreach (char symbol in input)
        {
            int nextStates = 0;

            if ((currentStates & Q0) != 0)
            {
                if (symbol == '/') nextStates |= Q1;
            }
            if ((currentStates & Q1) != 0)
            {
                if (symbol == '*') nextStates |= Q2;
            }
            if ((currentStates & Q2) != 0)
            {
                if (symbol == '*') nextStates |= Q3;
                else nextStates |= Q2; 
            }
            if ((currentStates & Q3) != 0)
            {
                if (symbol == '/') nextStates |= Q4;
                else if (symbol == '*') nextStates |= Q3;
                else nextStates |= Q2; 
            }
            if ((currentStates & Q4) != 0)
            {

            }

            currentStates = nextStates;
            if (currentStates == 0)
            {
                break;
            }
        }

        return (currentStates & Q4) != 0;
    }

    static void Main()
    {
        string[] testCases = {
            // accepted cases (Valid comments)
            "/*b*/",
            "/**/",
            "/***/",
            "/*bbb*bbb*/",
            "/*b/b*/",
            "/*116_GulengTest*/",
            "/* kri * zi */",

            // Rejected cases (Invalid comments)
            "/**",
            "/**/b/*bb*/",
            "bbb/**/bb",
            "/*/",
            "/**b/",
            "//bbbb",
            "/*b*/b"
        };

        Console.WriteLine("========================================");
        Console.WriteLine("   NFA C-STYLE COMMENT EVALUATION       ");
        Console.WriteLine("========================================\n");

        foreach (string test in testCases)
        {
            bool accepted = ProcessNfa(test);
            Console.WriteLine($"\nInput: {test,-20} -> {(accepted ? "[ACCEPTED]" : "[REJECTED]")}");
        }
    }
}