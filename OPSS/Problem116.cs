namespace OPSS
{
    /* Difficulty: 1/5
     * 
Ostatnimi czasy Święty Mikołaj trochę przytył i nie mieści się już w niektórych kominach, 
    przez które wchodził do domów, by zostawić prezenty. Co gorsza, przestał mieścić się 
    w niektórych oknach, a nawet drzwiach. Mikołaj zgodnie ze swoimi Zasadami Dyskrecji, 
    jeśli mieści się w kominie, to wchodzi do domu przez komin. Jeśli nie mieści się w kominie 
    i mieści się w oknie, to wybiera wejście przez okno. Przez drzwi wchodzi tylko wtedy, 
    gdy nie mieści się ani w kominie, ani w oknie, a w drzwiach się mieści. W kominie Mikołaj 
    mieści się, gdy obwód w pasie Mikołaja jest mniejszy od obwodu otworu komina. Podobnie, 
    Mikołaj mieści się w oknie (drzwiach), gdy obwód w pasie Mikołaja jest mniejszy od obwodu 
    otworu okna (drzwi). W tym roku Mikołaj ma jeszcze do odwiedzenia wiele domów i szkoda mu 
    tracić czas na różne próby wejścia do nich. Obawia się też, że podczas niektórych prób wejścia 
    mógłby się zaklinować.

Napisz program, który mając podany obwód w pasie Mikołaja, obwody otworów kominów, okien i drzwi 
    domów, wyznaczy sposób wejścia Mikołaja (zgodnie z jego Zasadami Dyskrecji) do każdego z tych domów.
Wejście
Pierwsza linia zawiera oddzielone pojedynczą spacją dwie liczby całkowite N oraz M (1 ≤ N ≤ 1000; 
    80 ≤ M ≤ 200), określające odpowiednio liczbę domów, które ma odwiedzić Mikołaj, oraz obwód 
    w pasie Mikołaja mierzony w centymetrach. W każdej z kolejnych N linii wejścia znajduje się 
    opis jednego domu. Opis domu składa się z trzech liczb całkowitych rozdzielonych pojedynczymi 
    spacjami: Ki, Oi, Di (50 ≤ Ki, Oi, Di ≤ 800), oznaczających mierzone w centymetrach obwody 
    odpowiednio otworu komina, otworu okna i otworu drzwi i-tego domu.
Wyjście
W kolejnych liniach dla każdego opisu domu należy wypisać jeden wyraz oznaczający sposób wejścia 
    Mikołaja, zgodnie z jego Zasadami Dyskrecji, do domu:

    komin - jeśli Mikołaj ma wejść do domu przez komin,
    okno - jeśli ma wejść przez okno,
    drzwi - jeśli ma wejść przez drzwi,
    brak - jeśli Mikołaj nie mieści się ani w kominie, ani w oknie, ani w drzwiach. 
     */
    public sealed class WejscieMikolaja : ProblemBase
    {
        protected override string Input => "6 150\r\n180 600 600\r\n120 130 140\r\n150 155 400\r\n135 140 500\r\n120 150 650\r\n140 200 145";

        protected override string Output => "komin\r\nbrak\r\nokno\r\ndrzwi\r\ndrzwi\r\nokno";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var s = input[0].Split(' ').Select(c => int.Parse(c)).ToArray();
            for (int i = 1; i <= s[0]; i++)
            {
                var s2 = input[i].Split(' ').Select(c => int.Parse(c)).ToArray();
                if (s[1] < s2[0])
                    output.Add("komin");
                else if (s[1] < s2[1])
                    output.Add("okno");
                else if (s[1] < s2[2])
                    output.Add("drzwi");
                else
                    output.Add("brak");
            }
        }
    }
}
