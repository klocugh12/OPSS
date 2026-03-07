using System.Text;

namespace OPSS
{
    /* 3/5
     * 
Zadanie
Napisz program, który dla zadanego pliku OPSSML:
posortuje leksykograficznie niemalejąco elementy w obrębie każdej listy elementów względem
nazw elementów,
posortuje leksykograficznie niemalejąco atrybuty w obrębie każdej listy atrybutów względem nazw
atrybutów.
Porządek w sortowaniu leksykograficznym określa kolejność kodów ASCII.
Wejście
Na wejściu podana jest zawartość pliku OPSSML, którego rozmiar nie przekracza 106 znaków.
Wyjście
Na wyjściu należy wypisać zawartość pliku OPSSML powstałego z pliku, którego zawartość jest
podana na wejściu, wyłącznie poprzez leksykograficzne posortowanie elementów w obrębie każdej
listy elementów według nazw elementów oraz poprzez leksykograficzne posortowanie atrybutów w
obrębie każdej listy atrybutów według nazw atrybutów. Elementy, które w obrębie jednej listy
elementów mają takie same nazwy elementów, należy wypisać w takiej kolejności, w jakiej podane
są na liście elementów na wejściu. Nie należy zamieniać elementu skróconego na element pełny,
ani elementu pełnego na element skrócony.
     */
    public sealed class OPSSML : ProblemBase
    {
        protected override string Input => "<?xml version=\"11.1\" OPSSML ?>\r\n<root b=\"23\" a=\"fg\">\r\n<yaa>\r\n<ghi x=\"15\">\r\n</ghi>\r\n<abc>\r\n</abc>\r\n<def/>\r\n</yaa>\r\n<x attr1a=\"value\" attr1b=\"value\" attr=\"0\">\r\n<tag/>\r\n</x>\r\n<ya>\r\n</ya>\r\n<x/>\r\n</root>";

        protected override string Output => "<?xml version=\"11.1\" OPSSML ?>\r\n<root a=\"fg\" b=\"23\">\r\n<x attr=\"0\" attr1a=\"value\" attr1b=\"value\">\r\n<tag/>\r\n</x>\r\n<x/>\r\n<ya>\r\n</ya>\r\n<yaa>\r\n<abc>\r\n</abc>\r\n<def/>\r\n<ghi x=\"15\">\r\n</ghi>\r\n</yaa>\r\n</root>";

        class XmlNode
        {
            public bool SelfClosed;
            public string Name = "";
            public XmlNode? Parent;
            public List<XmlNode> Children = [];
            public List<string> Attributes = [];

            public override string ToString()
            {
                Attributes.Sort();
                Children.Sort((a, b) => a.Name.CompareTo(b.Name));
                StringBuilder sb = new();
                sb.Append("<");
                sb.Append(Name);
                foreach (var attr in Attributes)
                    sb.Append($" {attr}"); 
                if (SelfClosed)
                    sb.AppendLine("/>");
                else
                {
                    sb.AppendLine(">");
                    foreach (var c in Children)
                        sb.Append(c.ToString());
                    sb.AppendLine($"</{Name}>");
                }
                return sb.ToString();
            }
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            XmlNode Current = null;
            output.Add(input[0]);
            foreach (string line in input.Skip(1))
            {
                if (line.StartsWith("</"))
                {
                    if(Current!.Parent != null)
                        Current = Current.Parent;
                    continue;
                }
                XmlNode NewNode = new()
                {
                    Parent = Current,
                    SelfClosed = line.EndsWith("/>")
                };
                var splits = line.Substring(1, line.Length - 2).Split(' ');
                NewNode.Name = splits[0];
                if (NewNode.SelfClosed)
                    NewNode.Name = NewNode.Name.Substring(0, NewNode.Name.Length - 1);
                foreach (var s in splits.Skip(1))
                {
                    NewNode.Attributes.Add(s);
                }
                if (Current != null)
                    Current.Children.Add(NewNode);
                else
                    Current = NewNode;
                    
                if (!NewNode.SelfClosed)
                    Current = NewNode;
            }
            output.AddRange(Current.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
