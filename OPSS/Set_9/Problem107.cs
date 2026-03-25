using System.Globalization;

namespace OPSS
{
    /* Difficulty: 2/5
     * In certain city there is a market, which allows you to buy or sell items.
     * To do so, you place a buy or sell order. Each order must contain following information:
     * ○ Item ID
     * ○ Transaction type (buy or sell)
     * ○ Quantity
     * ○ Price limit
     * If you place a buy order, you can only buy items at price below or equal to limit.
     * If you place a sell order, you can only sell them at price above or equal to limit.
     * If there are buy and sell orders with overlapping price ranges, it is a match, and transaction 
     * can happen. Depending on whether there is enough items to sell at sufficiently low price, an 
     * order can be partially or completely fulfilled. Likewise with sell orders.
     * Orders are sorted according to price limits. Buy orders with highest limit are put first,
     * and so are sell orders with lowest limit. If two orders of same type have same limit,
     * they are sorted in order they appeared. Orders are fulfilled according to their sorting.
     * All completely fulfilled orders are removed from the list.
     * A transaction volume is a sum of all quantities for all fulfilled orders for a given item. 
     * A current rate is the price, at which last order was fulfilled for a given item.
     * 
     * Example for a given item "A":
     *   Buy Orders       Sell Orders
     * Quantity Limit Quantity   Limit
     *  200     10.50    300     11.10
     *  100     10.00    200     11.20
     *  
     * Consider next order: item "A", Sell, Qty: 250, Limit: 10.00.
     * In such case there will be two transactions: First, sell 200 items at rate 10.50, then
     * sell 50 items at rate 10.00. After that, list will look as follows:
     *  
     *   Buy Orders       Sell Orders
     * Quantity Limit Quantity   Limit
     *   50     10.00    300     11.10
     *                   200     11.20 
     *                   
     * For item "A" current rate is 10.00 and transaction volume is 250.
     * Consider next order: item "A", Buy, Qty: 310, Limit: 11.10. 
     * In such case 300 items will be sold at rate 11.10, and list will look as follows: 
     * 
     *   Buy Orders       Sell Orders
     * Quantity Limit Quantity   Limit
     *   10     11.10    200     11.20
     *   50     10.00 
     *   
     * Current rate for item A is 11.10, and transaction volume is now 550.
     * Your goal is to determine a volume and rate for a given list of orders.
     * 
     * Input
     * First line contains number of orders N (1 ≤ N ≤ 100000).
     * Each order is described by a single line, in which there are four tokens separated by 
     * a whitespace each. First token is item ID (single uppercase English letter), order type 
     * (a single letter K or S, respectively meaning buy or sell), a positive integer meaning
     * quantity of items in order L (1 ≤ L ≤ 10000), and a price limit  C (0 < C ≤ 10000). 
     * C contains . as decimal separator, has at least one digit before . and exactly two after it.
     * 
     * Output
     * First line contains number X meaning number of items, which had any orders fulfilled (partially or completely).
     * Following X lines each contain three tokens separated by a whitespace each. First token is item ID.
     * Second token is transaction volume, and third one is rate at closing time.
     * Lines should be sorted according to item IDs. Rates should written in the same format as they were in price limits.
     */
    public sealed class GieldaRzeczyWartosciowych : ProblemBase
    {
        protected override string Input => "8\r\nA K 100 10.00\r\nA K 200 10.50\r\nA S 300 11.10\r\nA S 200 11.20\r\nA S 250 10.00\r\nA K 310 11.10\r\nB K 100 10.00\r\nB S 50 10.00";

        protected override string Output => "2\r\nA 550 11.10\r\nB 50 10.00";

        static (int, double) ManageSales(List<(int, double)> buys, List<(int, double)> sales)
        {
            (int, double) result = (0, 0.0);
            while (buys.Count > 0 && sales.Count > 0 && buys[0].Item2 >= sales[0].Item2)
            {
                if (buys[0].Item1 > sales[0].Item1)
                {
                    result = (result.Item1 + sales[0].Item1, sales[0].Item2);
                    buys[0] = (buys[0].Item1 - sales[0].Item1, buys[0].Item2);
                    sales.RemoveAt(0);
                }
                else
                {
                    result = (result.Item1 + buys[0].Item1, sales[0].Item2);
                    sales[0] = (sales[0].Item1 - buys[0].Item1, sales[0].Item2);
                    buys.RemoveAt(0);
                }
            }
            return result;
        }

        static void Insert(List<(int, double)> collection, (int, double) value, bool ascending)
        {
            if (collection.Count == 0)
            {
                collection.Add(value);
                return;
            }
            int a = 0, b = collection.Count;
            while (a != b)
            {
                int c = (a + b) >> 1;
                if (ascending ^ collection[c].Item2 > value.Item2)
                {
                    a = c + 1;
                }
                else
                {
                    b = c;
                }
            }
            collection.Insert(a, value);
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int X = int.Parse(input[0]);
            Dictionary<string, List<(int, double)>> buys = [], sales = [];
            Dictionary<string, (int, double)> results = [];
            for (int i = 1; i <= X; i++)
            {
                var splits = input[i].Split(' ');
                if (!buys.ContainsKey(splits[0]))
                    buys.Add(splits[0], []);
                if (!sales.ContainsKey(splits[0]))
                    sales.Add(splits[0], []);
                if (splits[1] == "K")
                    Insert(buys[splits[0]], (int.Parse(splits[2]), double.Parse(splits[3], CultureInfo.InvariantCulture)), false);
                else
                    Insert(sales[splits[0]], (int.Parse(splits[2]), double.Parse(splits[3], CultureInfo.InvariantCulture)), true);
                if (buys.Count > 0 && sales.Count > 0)
                {
                    var newResult = ManageSales(buys[splits[0]], sales[splits[0]]);
                    if (newResult.Item1 > 0)
                    {
                        if (!results.ContainsKey(splits[0]))
                            results.Add(splits[0], newResult);
                        else
                            results[splits[0]] = (newResult.Item1 + results[splits[0]].Item1, newResult.Item2);
                    }
                }
            }
            output.Add(results.Count.ToString());
            output.AddRange(results.Select(r => $"{r.Key} {r.Value.Item1} {r.Value.Item2.ToString("#.00", CultureInfo.InvariantCulture)}"));
        }
    }
}
