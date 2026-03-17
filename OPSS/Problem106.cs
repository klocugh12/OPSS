using System.Text;

namespace OPSS
{
    /* Difficulty: 3/5
     * Consider a markup language OPSSML. Each OPSSML file contains header in first line, followed by
     * one element in each following line. An element can be either simple or complex.
     * A simple element has format <name [attr] />, where name is element name, and [attr] is
     * optional list of attributes.
     * 
     * A complex element has format:
     * <name [attr] >
     * [elements]
     * </name>
     * 
     * Where name and [attr] mean the same things as above, and [elements] is optional list of
     * other elements, either simple or complex ones.
     * 
     * An attribute is defined as follows:
     * name="value"
     * where name is attribute name, and value is its value. All attributes are separated by a
     * single whitespace. All attribute names within a single list must be distinct.
     * 
     * All names and values are strings up to 20 characters long, consisting of English letters 
     * (upper- or lowercase) as well as digits.
     * 
     * Your goal is to write an OPSSML parser. It must sort attributes and elements in ascending order.
     * It must sort elements accordingly at all nesting levels. A lexicographical order is determined using
     * ASCII codes of characters used.
     * 
     * Input
     * A single OPSSML file, no more than 106 characters long.
     * 
     * Output
     * Same OPSSML file after processing by your parser. Sort elements and attributes lexicographically. 
     * Elements with same nesting level and names should appear in the same order they were in the input file.
     * Do not convert complex elements to simple ones or other way round.
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
