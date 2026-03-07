namespace OPSS
{
    /* 3/5
     * Jasio już od dłuższego czasu gromadzi adresy IP (miejmy nadzieję, że Jasio nie jest spamerem).
Pewnego dnia postanowił uporządkować swoją kolekcję. Okazuje się, że jest ona dość specyficzna,
bowiem w jej skład wchodzą wszystkie możliwości adresów, które zawierają zawsze takie same
liczby. Różnią się jedynie kolejnością występowania tych liczb. Wiadomo również, że nie
występują w niej dwa takie same adresy.
Adres IP w danej wersji protokołu P składa się z dokładnie P liczb całkowitych z zakresu 0..255,
oddzielonych od siebie kropką.
Pomoż Jasiowi w uporządkowaniu jego zbioru adresów IP.
Wejście
W pierwszym wierszu wejścia podana jest liczba P, będąca numerem wersji protokołu IP, 3 ≤ P ≤
10, dla adresów Jasia. Druga linia wejścia składa się z dokładnie P liczb całkowitych, z zakresu
0..255. Są to liczby, z których zbudowane są adresy z kolekcji Jasia.
Wyjście
W oddzielnych liniach wyjścia, należy wypisać w porządku rosnącym adresy IP, pochodzące z
kolekcji Jasia, jakie można utworzyć z zadanych P liczb. Jeżeli adresów IP jest więcej niż 10000,
należy wypisać pierwsze 10000 adresów.
     */
    public sealed class AdresyIP : ProblemBase
    {
        protected override string Input => "4\r\n127 0 0 1";

        protected override string Output => "0.0.1.127\r\n0.0.127.1\r\n0.1.0.127\r\n0.1.127.0\r\n0.127.0.1\r\n0.127.1.0\r\n1.0.0.127\r\n1.0.127.0\r\n1.127.0.0\r\n127.0.0.1\r\n127.0.1.0\r\n127.1.0.0";

        static int total = 0;
        static void IPs(string added, IEnumerable<int> free, List<string> output)
        {
            if (total >= 10000)
                return;
            if (free.Count() == 1)
            {
                output.Add($"{added}.{free.First()}");
                total++;
                return;
            }
            else
                foreach(int c in free.Distinct())
                {
                    var i = free.ToList().IndexOf(c);
                    IPs(added == "" ? $"{c}" : $"{added}.{c}", free.Take(i).Concat(free.Skip(i + 1).Take(free.Count() - i - 1)), output);
                }
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var list = input[1].Split(' ').Select(s => int.Parse(s)).ToList();
            list.Sort((a, b) => a.CompareTo(b));
            IPs("", list, output);
        }
    }
}
