// See https://aka.ms/new-console-template for more information
using OPSS;
using System.Diagnostics;

foreach (var t in typeof(ProblemBase).Assembly.GetExportedTypes().Where(t => !t.IsAbstract && typeof(ProblemBase).IsAssignableFrom(t) && t.Namespace == "OPSS"))
{
    var problem = (ProblemBase)Activator.CreateInstance(t)!;
    problem.Solve();
}