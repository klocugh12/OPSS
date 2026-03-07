using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPSS;

namespace OPSS_2010
{
    class Program
    {
        public static void Main(string[] args)
        {
            foreach(var t in typeof(ProblemBase).Assembly.GetExportedTypes().Where(t2 => !t2.IsAbstract && typeof(ProblemBase).IsAssignableFrom(t2)))
            {
                var p = Activator.CreateInstance(t) as ProblemBase;
                p.Solve();
            }
        }
    }
}
