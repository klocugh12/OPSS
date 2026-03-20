namespace OPSS
{
    /* Difficulty: 2/5
     * Consider following programing language names OPS:
     * Commands are executed left to right.
     * Each command is either an inteeger or one of 4 tokens: 'O', 'P', 'S', '.'
     * Each integer is put on a stack. Other commands are executed as follows:
     * 'O' - take two numbers from a stack L1, then L2. Put L1-L2 on a stack.
     * 'P' - take two numbers from a stack. Put their product on a stack.
     * 'S' - take two numbers from a stack. Put their sum on a stack.
     * '.' - clear a stack, displaying all numbers put on it in order they were removed from it, and halt.
     * Write an OPS interpreter.
     * 
     * Input
     * First line contains number of scripts to execute, 0 < C ≤ 100.
     * Following C lines each contain a script to execute. Scripts are made of commands of OPS language
     * separated by a whitespace each. Absolute value on a stack will never exceed 2^31 - 1.
     * Each script is no more than 500000 characters long.
     * 
     * Output
     * C lines, each containing result of executing respective OPS script ;-)
     */
    public sealed class OPS : ProblemBase
    {
        protected override string Input => "3\r\n6502 68000 6502 68000 6502 O P S S 2005 4 2 .\r\n11 22 33 44 O S P .\r\n10 9 O -1 S 9 8 7 6 5 4 3 2 1 1 P .";

        protected override string Output => "2 4 2005 399934498\r\n121\r\n1 2 3 4 5 6 7 8 9 0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int C = int.Parse(input[0]);
            for(int i = 1; i <= C; i++)
            {
                var splits = input[i].Split(' ');
                Stack<int> stack = new();
                foreach (string s in splits) 
                {
                    int number;
                    if(int.TryParse(s, out number))
                        stack.Push(number);
                    else
                    {
                        switch(s)
                        {
                            case "O":
                                int c = stack.Pop();
                                stack.Push(stack.Pop() - c);
                                break;

                            case "P":
                                stack.Push(stack.Pop() * stack.Pop()); 
                                break;

                            case "S":
                                stack.Push(stack.Pop() + stack.Pop());
                                break;
                        }
                    }
                }
                output.Add(string.Join(" ", stack));
            }
        }
    }
}
