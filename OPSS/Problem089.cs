namespace OPSS
{
    /* Difficulty: 3/5
     * You are running a store containing some items. Items are stored in batches. Each batch can be
     * described with 3 integers A, B, C, representing certain quality parameters of items.
     * 
     * Customers can place an order for merchandise with specified quality of parameters, or may
     * be satisfied with any batch. If they placed an order with specific requirements, they cannot
     * be given items which don't meet those requirements. Your goal is to determine, 
     * whether all customer orders can be fulfilled, given current store.
     * 
     * Input
     * First line contains number of data sets Z, 1 ≤ Z ≤ 100.
     * First line of each data set contains two numbers separated by a whitespace.
     * First number S is number of batches. Second number R is number of customer orders.
     * Both numbers are positive and satisfy 2 ≤ S+R ≤ 500; S, R > 0.
     * Following S lines each contain 4 numbers each separated by a single whitespace.
     * They are, respectively I - quantity of items in a batch (1 ≤ I ≤ 1000) and values of parameters
     * A, B and C (1 ≤ A, B, C ≤ 10^9).
     * Following R lines each also contain 4 numbers each separated by a single whitespace.
     * They are, respectively i - quantity ordered by a customer (1 ≤ i ≤ 1000) and values of parameters
     * a, b and c expected by a customer (0 ≤ a, b, c ≤ 10^9).
     * Value 0 for a parameter means that parameter does not concern a given customer. In particular,
     * if all 3 parameters are 0, any batch is acceptable and order can be fulfilled using multiple batches
     * if needed.
     * 
     * Output
     * Z lines, each containing an answer for respective data set: "tak", if all orders can be fulfilled,
     * "nie" otherwise
     */
    public sealed class Rezerwacje : ProblemBase
    {
        protected override string Input => "2\r\n2 2\r\n2 1 1 1\r\n2 2 3 1\r\n2 0 0 0\r\n1 2 0 0\r\n1 1\r\n100 1 2 3\r\n101 0 0 0";

        protected override string Output => "tak\r\nnie";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int Z = int.Parse(input[0]);
            int j = 1;
            for(int i = 1; i <= Z; i++)
            {
                var splits = input[j].Split(' ');
                int S = int.Parse(splits[0]), R = int.Parse(splits[1]);
                j++;
                List<int[]> stores = [];
                for (int k = 0; k < S; k++)
                {
                    var enumerable = input[j].Split(' ').Select(s => int.Parse(s));
                    stores.Add(enumerable.Concat([enumerable.Count(s => s == 0)]).ToArray());
                    j++;
                }
                List<int[]> conditions = [];
                for (int k = 0; k < R; k++)
                {
                    var enumerable = input[j].Split(' ').Select(s => int.Parse(s));
                    conditions.Add(enumerable.Concat([ enumerable.Count(s => s == 0) ]).ToArray());
                    j++;
                }
                stores.Sort((a, b) => a[4].CompareTo(b[4]));
                conditions.Sort((a, b) => a[4].CompareTo(b[4]));
                while(conditions.Any())
                {
                    var c = conditions[0];
                    int k = 0;
                    while (k < stores.Count)
                    {
                        if ((stores[k][1] == c[1] || c[1] == 0) && (stores[k][2] == c[2] || c[2] == 0) && (stores[k][3] == c[3] || c[3] == 0))
                        {
                            if (c[0] > stores[k][0])
                            {
                                c[0] -= stores[k][0];
                                stores.RemoveAt(k);
                            }
                            else
                            {
                                stores[k][0] -= c[0];
                                c[0] = 0;
                                break;
                            }
                        }
                        else
                            k++;
                    }
                    if (c[0] > 0)
                    {
                        output.Add("nie");
                        break;
                    }
                    else
                        conditions.RemoveAt(0);
                }
                if(!conditions.Any())
                    output.Add("tak");
            }
        }
    }
}
