using System;

namespace MealyMachineComment
{
    public enum State
    {
        A,  B, C,  D,  E   
    }

    class Program
    {
        static char Transition(ref State currentState, char input)
        {
            char output = '0';

            switch (currentState)
            {
                case State.A:
                    if (input == '/')
                    {
                        currentState = State.B;
                    }
                    else
                    {
                        currentState = State.E; // invalid start
                    }
                    output = '0';
                    break;

                case State.B:
                    if (input == '*')
                    {
                        currentState = State.C;
                    }
                    else
                    {
                        currentState = State.E; // must have '*' after '/'
                    }
                    output = '0';
                    break;

                case State.C:
                    if (input == '*')
                    {
                        currentState = State.D;
                    }
                    else
                    {
                        currentState = State.C; // '/' or 'a' inside body
                    }
                    output = '0';
                    break;

                case State.D:
                    if (input == '/')
                    {
                        currentState = State.E;
                        output = '1'; // Valid comment closes here!
                    }
                    else if (input == '*')
                    {
                        currentState = State.D; // consecutive stars like /***
                        output = '0';
                    }
                    else
                    {
                        currentState = State.C; // false alarm, back to body
                        output = '0';
                    }
                    break;

                case State.E:
                default:
                    // Any character after closing or after invalid input stays in E with 0
                    currentState = State.E;
                    output = '0';
                    break;
            }

            return output;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("     MEALY MACHINE: C-STYLE COMMENT VALIDATOR     ");
            Console.WriteLine("==================================================");

            while (true)
            {
                Console.Write("\nEnter input string (or press Enter to exit): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Exiting program. Ciao!");
                    break;
                }

                State currentState = State.A;
                string stateTrace = "A";
                string outputTrace = "";
                char lastOutput = '0';

                for (int i = 0; i < input.Length; i++)
                {
                    char ch = input[i];
                    char outChar = Transition(ref currentState, ch);

                    stateTrace += $" -> {currentState}";
                    outputTrace += outChar + " ";
                    lastOutput = outChar;
                }

                Console.WriteLine("\n--- Trace ---");
                Console.WriteLine($"Input:      {input}");
                Console.WriteLine($"States:     {stateTrace}");
                Console.WriteLine($"Outputs:    {outputTrace}");

                // a string is accepted if and only if it completed the comment on its final character
                if (lastOutput == '1' && currentState == State.E)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Result:     ACCEPTED");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Result:     REJECTED");
                }
                Console.ResetColor();

                Console.WriteLine(new string('-', 50));
            }
        }
    }
}