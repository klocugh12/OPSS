namespace OPSS
{
    /* Time limit: 1.5s, Memory limit: 2MB, Difficulty: 3/5
     * 
     * A software company is moving to another office. A move is organized as follows:
     * 1) Each employee packs their things in a box, carries it outside, and puts it
     * at the end of line of boxes carried out by other employees first.
     * 2) All boxes are weighted and divided into K groups, maintaining the order they
     * were carried out in.
     * 3) A truck travels K times between old and new office, each time moving a single group
     * from old to new office.
     * 4) Unboxing happens in the same order as moving out.
     * Cost of moving out is proportional to a load truck can carry, number of travels doesn't matter.
     * Therefore a truck could travel without any boxes. We want to miminze a cost. 
     * Therefore, knowing boxes' weights, find a way to divide them into K groups 
     * to minimize weight of the heaviest group.
     * 
     * Input
     * First line contains two numbers N and K, 1 ≤ N ≤ 1000, 1 ≤ K ≤ 100.
     * N is number of boxes, K is number of groups (and hence travels).
     * Second line contains N numbers separated by a whitespace, 1 ≤ xi ≤ 1000, 
     * equal to weight of consecutive boxes.
     * 
     * Output
     * A single number equal to weight of heaviest group assuming we divide boxes into K groups.
     */
    public sealed class Przeprowadzka : ProblemBase
    {
        protected override string Input => "6 3\r\n1 1 2 3 5 7";

        protected override string Output => "7";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int N = int.Parse(splits[0]), K = int.Parse(splits[1]);
            var boxes = input[1].Split(' ').Select(s => int.Parse(s)).ToList();
            while (boxes.Count > K)
            {
                if (boxes.Count == 2)
                {
                    boxes[0] += boxes[1];
                    boxes.RemoveAt(1);
                }
                else
                {
                    int index = boxes.IndexOf(boxes.Min());
                    if (index == 0)
                    {
                        boxes[0] += boxes[1];
                        boxes.RemoveAt(1);
                    }
                    else if (index == boxes.Count - 1 || boxes[index - 1] < boxes[index + 1])
                    {
                        boxes[index - 1] += boxes[index];
                        boxes.RemoveAt(index);
                    }
                    else
                    {
                        boxes[index + 1] += boxes[index];
                        boxes.RemoveAt(index);
                    }
                }
            }
            output.Add(boxes.Max().ToString());
        }
    }
}
