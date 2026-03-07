using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OPSS_2010
{
    public abstract class ProblemBase
    {
        protected abstract string Input { get; }

        protected abstract string Output { get; }

        public void Solve()
        {
            List<string> output = new List<string>();
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                BuildSolution(Input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None), output);
                sw.Stop();
                var result = string.Join("\r\n", output) == Output;
                Debug.WriteLine(GetType().Name + ": " + (result ? sw.ElapsedMilliseconds + " ms" : "NG"));
            }
            finally
            {
                GC.Collect();
            }
        }

        protected abstract void BuildSolution(string[] input, List<string> output);
    }
}
