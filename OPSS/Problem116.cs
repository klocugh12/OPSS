namespace OPSS
{
    /* Problemset: 10, Difficulty: 1/5
     * Lately a Santa Claus has gained weight and he cannot fit through some chimneys he used to
     * enter houses through. In some cases he is no longer able to fit through some windows
     * or even doors. Santa always prefers going through a chimney if he can. If not, he tries to 
     * go through the window. As last resort, he tries to go through the door. Santa will fit through 
     * any opening, if his waist circumference is strictly smaller than circumference of a given
     * opening. Help Santa determine, which openings to use.
     * 
     * Input
     * First line contains two numbers separated by a whitespace N and M (1 ≤ N ≤ 1000; 80 ≤ M ≤ 200).
     * They are, respectively, number of houses to visit and Santa's waist circumference.
     * Following N lines each contain three numbers, each separated by a whitespace.
     * They are, respectively, chimney circumference Ki, window circumference Oi, and door circumference Di 
     * (50 ≤ Ki, Oi, Di ≤ 800) for i-th house.
     * 
     * Output
     * N lines, each containing a single word corresponding to opening to use for i-th home:
     * komin - if to use chimney.
     * okno - if to use window.
     * drzwi - if to use door.
     * brak - if none of the openings are large enough.
     */
    public sealed class WejscieMikolaja : ProblemBase
    {
        protected override string Input => "6 150\r\n180 600 600\r\n120 130 140\r\n150 155 400\r\n135 140 500\r\n120 150 650\r\n140 200 145";

        protected override string Output => "komin\r\nbrak\r\nokno\r\ndrzwi\r\ndrzwi\r\nokno";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var s = input[0].Split(' ').Select(c => int.Parse(c)).ToArray();
            for (int i = 1; i <= s[0]; i++)
            {
                var s2 = input[i].Split(' ').Select(c => int.Parse(c)).ToArray();
                if (s[1] < s2[0])
                    output.Add("komin");
                else if (s[1] < s2[1])
                    output.Add("okno");
                else if (s[1] < s2[2])
                    output.Add("drzwi");
                else
                    output.Add("brak");
            }
        }
    }
}
