using System;

namespace MealyMachineComment
{
    // states
    public enum State
    {
        A,  B,  C, D   
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
                        currentState = State.A;
                    }
                    output = '0';
                    break;

                case State.B:
                    if (input == '*')
                    {
                        currentState = State.C;
                    }
                    else if (input == '/')
                    {
                        currentState = State.B;
                    }
                    else
                    {
                        currentState = State.A;
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
                        currentState = State.C; 
                    }
                    output = '0';
                    break;

                case State.D:
                    if (input == '/')
                    {
                        currentState = State.A;
                        output = '1'; 
                    }
                    else if (input == '*')
                    {
                        currentState = State.D; 
                        output = '0';
                    }
                    else
                    {
                        currentState = State.C; 
                        output = '0';
                    }
                    break;
            }

            return output;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("       MEALY MACHINE: C-STYLE COMMENT DETECTOR    ");
            Console.WriteLine("==================================================");

            while (true)
            {
                Console.Write("\nEnter input string (or press Enter to exit): ");
                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Exiting program. Goodbye!");
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

                // in a sequence detector, the comment is accepted if the last transition emitted '1'
                if (lastOutput == '1')
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