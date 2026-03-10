namespace OPSS
{
    /* Difficulty: 4/5
     * Dwaj koledzy grają w grę polegającą na dowiązywaniu kolejnych kawałków sznurka do siebie, aż
do momentu, gdy taka konstrukcja urwie się pod własnym ciężarem. Pierwszy sznurek
dowiązywany jest przez gracza rozpoczynającego grę do specjalnego haczyka o nieskończonej
wytrzymałości, a kolejne sznurki dowiązywane na zmianę przez obu graczy, zawsze do sznurka
który był dowiązywany przez poprzednika.
Gracze mają do dyspozycji wiele rodzajów sznurków, o różnej wadze i różnej wytrzymałości.
Wytrzymałość sznurka jest wprost proporcjonalna do jego wagi, a współczynnik wytrzymałości taki
sam dla wszystkich wag sznurków. Zapasy sznurków każdej wagi są nieskończone. Gracze na
zmianę dowiązują do sznurka położonego najniżej sznurek o wybranej wadze (tym samym i
wytrzymałości), a przegrywa ten, po czyim ruchu któryś ze sznurków się zerwie.
Współczynnik wytrzymałości określa krotność ciężaru jaki wytrzymuje sznurek w odniesieniu do
jego własnej wagi. Sznurek musi udźwignąć także własny ciężar. Sznurek zrywa się, gdy waga
sznurków przywiązanych niżej od niego, wraz z jego wagą, przekracza wagę sznurka pomnożoną
przez współczynik wytrzymałości.
Twoim zadaniem jest stwierdzenie, dla zadanego zbioru różnych wag dostępnych w grze sznurków
oraz współczynnika wytrzymałości, czy gracz który rozpoczyna grę wybierając pierwszy sznurek,
ma strategię wygrywającą. To znaczy, czy może tak wybrać pierwszy i następne sznurki, aby
niezależnie od wyborów przeciwnika wygrać grę. Gracze na zmianę nie tylko dowiązują wybrane
sznurki, ale także dokonują wyborów, znając wcześniejsze ruchy przeciwnika.
Wejście
W pierwszym wierszu wejścia znajduje się liczba C,1 ≤ C ≤ 100, oznaczająca ilość zestawów
danych. W kolejnych wierszach znajdują się zestawy danych. W pierwszym wierszu każdego
zestawu danych znajduje się liczba N,1 ≤ N < 100 - jest to liczba różnych wag sznurków. W
kolejnym wierszu znajduje się N liczb całkowitych dodatnich, mniejszych od 1000, posortowanych
rosnąco, oddzielonych pojedynczymi spacjami. Są to wagi sznurków dostępnych w grze. W
ostatnim wierszu każdego zestawu znajduje się liczba naturalna - współczynnik wytrzymałości -
WW, 1 ≤ WW ≤ 100, WW=Wytrzymałość/Waga, dla każdego sznurka.
Wyjście
Dla każdego zestawu danych należy wydać na standardowe wyjście linię zawierającą słowo "tak",
jeśli gracz rozpoczynający grę ma strategię wygrywającą, a słowo "nie" w przeciwnym wypadku.
     */
    public sealed class Sznurki : ProblemBase
    {
        protected override string Input => "4\r\n3\r\n1 2 3\r\n4\r\n4\r\n1 2 4 10\r\n12\r\n5\r\n90 91 92 93 999\r\n100\r\n4\r\n450 900 901 902\r\n4";

        protected override string Output => "tak\r\nnie\r\ntak\r\nnie";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                j++;
                int[] vals = input[j].Split(' ').Select(s => int.Parse(s)).ToArray();
                j++;
                int WW = int.Parse(input[j]) - 1;
                j++;
                int[] results = new int[WW * vals.Max() + 1];
                foreach (var v in vals)
                    results[v] = 1;
                foreach (var v in vals)
                {
                    int limit = v * WW;
                    if (results[limit] > 0)
                        continue;
                    for (int k = 0; k <= limit; k++)
                    {
                        for (int l = 0; l < vals.Length; l++)
                        {
                            if (results[k] % 2 == 0)
                            {
                                if (k >= vals[l] && results[k] % 2 == 0)
                                    results[k] = results[Math.Min(k - vals[l], vals[l] * WW)] + 1;
                            }
                        }
                    }
                }
                output.Add(vals.Any(v => results[v * WW] % 2 == 0) ? "tak" : "nie");
            }
        }
    }
}
