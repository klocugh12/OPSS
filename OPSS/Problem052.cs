namespace OPSS
{
    /* Difficulty: 3/5
     * Your goal is to determine, what shape is on a scanned image.
     * It can be either of three: square, triangle, rectangle. Assume following:
     * All images are black and white.
     * Ellipse's axis and rectangle's sides are always parallel to the edges of the image.
     * A triangle cannot have an obtuse angle.
     * One side of a triangle is parallel to the edges of the image.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 10. 
     * Each data set consists of two rows.
     * First row of each data set consists of two numbers separated by a whitespace H and B,
     * H is equal to height of the image in pixels, B is equal to length of line representing
     * a single row of image, 9 ≤ H ≤ 2000, 3 ≤ B ≤ 500.
     * Second row of each data set consists of HxB hexadecimal digits 0..9A..F.
     * Each digit represents 4 pixels of an image, with black bits set to 1 and white to 0.
     * Therefore, number of pixels in each line of an image is equal to L=4B.
     * First digit represents bottom left corner of an image.
     * 
     * Output
     * D lines, each containing a single number representing an answer:
     * 1 - if image contains a rectangle.
     * 2 - if image contains an ellipse.
     * 3 - if image contains a triangle.
     */
    public sealed class KomputerowaTelepatia : ProblemBase
    {
        protected override string Input => "2\r\n13 4\r\n000003C01FF83FFC3FFC7FFE7FFE7FFE3FFC3FFC1FF803C00000\r\n11 3\r\n00000703F1FFFFF3FF0FF03F00F003000";

        protected override string Output => "2\r\n3";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                var splits = input[j].Split(' ');
                int a = int.Parse(splits[0]), b = int.Parse(splits[1]);
                j++;
                List<string> lines = [];
                string zeros = new string('0', b);
                int k = 0;
                while (k < input[j].Length)
                {
                    string s = input[j].Substring(k, b);
                    if (s != zeros)
                        lines.Add(s);
                    k += b;
                }
                if (lines[0] != lines[lines.Count - 1])
                {
                    output.Add("3");
                }
                else if(lines.All(l => l == lines[0]))
                {
                    output.Add("1");
                }
                else
                {
                    output.Add("2");
                }
                j++;
            }
        }
    }
}
