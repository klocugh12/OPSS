using System.Diagnostics;

namespace OPSS
{
    public abstract class ProblemBase
    {
        protected abstract string Input { get; }

        protected abstract string Output { get; }

        public void Solve()
        {
            List<string> output = [];
            if (GC.TryStartNoGCRegion(1L << 26))
                try
                {
                    Stopwatch sw = new();
                    sw.Start();
                    BuildSolution(Input.Split(Environment.NewLine), output);
                    sw.Stop();
                    var result = string.Join("\r\n", output) == Output;
                    Debug.WriteLine($"{this.GetType().Name}: {(result ? $"{sw.ElapsedMilliseconds} ms" : "NG")}");
                }
                finally
                {
                    GC.EndNoGCRegion();
                    GC.Collect();
                }
        }

        protected abstract void BuildSolution(string[] input, List<string> output);
    }
}
