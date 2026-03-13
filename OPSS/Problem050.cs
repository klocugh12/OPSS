namespace OPSS
{
    /* Difficulty: 3/5
     * A certain hamster colony is particularly prolific at breeding.
     * Each pair of hamsters births two hamsters every month.
     * Newborn hamsters are capable of breeding after one month.
     * Colony starts with two pairs of hamsters. For some reason, in the first month only two hamsters were born.
     * Same thing happened next month. In the third month things went back to normal - 
     * three mature pairs of hamster gave birth to 3 pairs of young ones.
     * To recap: first month results in three pairs, second - four pairs, third - seven pairs.
     * After one year we had 521 pairs. Your task is to track the growth after given number
     * of years, assuming longevity is not the issue. 10 most significant numbers of result are enough.
     * 
     * Input
     * First line contains number of data sets D, 1 ≤ D ≤ 1000.
     * Each data set consists of a single line containin a single number L, 1 ≤L ≤ 5000, meaning number of years to track.
     * 
     * Output
     * D lines, each containing two numbers separated by a whitespace. 
     * First number is a number of digits N of resulting number of hamsters C.
     * Second number is first 10 digits of number C, or, if N < 10, all of them.
     */
    public sealed class ChomikiEdka : ProblemBase
    {
        protected override string Input => "2\r\n1\r\n4";

        protected override string Output => "3 521\r\n11 1739379600";

        static List<int> Add(List<int> shorter, List<int> longer)
        {
            longer = new(longer);
            bool carry = false;
            int k;
            for(k = 0; k < shorter.Count; k++)
            {
                longer[longer.Count - k - 1] += shorter[shorter.Count - k - 1] + (carry ? 1 : 0);
                int a = longer[longer.Count - k - 1];
                carry = a >= 10;
                if (carry)
                    longer[longer.Count - k - 1] %= 10;
            }
            k = longer.Count - shorter.Count - 1;
            while(carry && k >= 0)
            {
                longer[k]++;
                carry = longer[k] >= 10;
                if(carry)
                    longer[k] %= 10;
            }
            if (k < 0 && carry)
                longer.Insert(0, 1);
            return longer;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]) * 12;
                int m = 2;
                List<int> n1 = [3], n2 = [4]; 
                int c = a;
                while(m < a)
                {
                    List<int> temp = n2;
                    n2 = Add(n1, n2);
                    n1 = temp;
                    m++;
                }
                output.Add($"{n2.Count} {string.Join("", n2.Take(Math.Min(n2.Count, 10)))}");
            }
        }
    }
}
