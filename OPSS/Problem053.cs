namespace OPSS
{
    /* 2/5
     * 
Ala obchodziła niedawno imieniny i zaprosiła swoich przyjaciół na małe przyjęcie. Każdy ze
znajomych wiedząc, że jest ona wielkim łasuchem, przyniósł jej po tabliczce czekolady. Ala
przygotowała dla swoich przyjaciół dużo innych smakołyków którymi mogła ich poczęstować, więc
słodkie prezenty zostawiła na później.
Na drugi dzień Ala zebrała wszystkie czekolady, z zamiarem podzielenia się nimi ze swoim bratem.
Zaczęła się jednak zastanawiać, czy jeżeli to ona weźmie pierwszy kawałeczek z pierwszej
tabliczki, i będą brać po jednej "cegiełce" na zmianę, to kto zje ostatni kawałeczek ostatniej
tabliczki?... Pomóż Ali rozwikłać ten problem.
Wejście
W pierwszym wierszu wejścia znajduje się liczba D, określająca ilość zestawów danych, 1 ≤ D ≤
20. W kolejnych wierszach wejścia znajdują się zestawy danych. W pierwszej linii jednego zestawu
znajduję się liczba C, 1 ≤ C ≤ 100, określająca liczbę tabliczek czekolady. W kolejnych C liniach
zestawu znajdują się wymiary kolejnych tabliczek. Wymiary opisane są przez dwie liczby
naturalne: a, b, 0 < a, b < 2^31, oddzielone pojedynczą spacją.
Wyjście
Dla każdego zestawu danych, w osobnych liniach wyjścia, należy wypisać jedną z dwóch liczb: 0,
jeśli ostatni kawałek zje Ala, 1 - jeśli ostatni kawałek zje jej brat.
     */
    public sealed class Czekoladka : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n10 20\r\n3 3\r\n36 3\r\n11 99\r\n1\r\n5 7";

        protected override string Output => "1\r\n0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int i = 1;
            while(i < input.Length)
            {
                int c = int.Parse(input[i]);
                i++;
                bool odd = false;
                for(int j = 0; j < c; j++)
                {
                    var splits = input[i].Split(' ').Select(s => int.Parse(s));
                    if (!splits.Any(s => s % 2 == 0))
                        odd = !odd;
                    i++;
                }
                output.Add(odd ? "0" : "1");
            }
        }
    }
}
