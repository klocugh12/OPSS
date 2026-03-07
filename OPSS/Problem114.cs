namespace OPSS
{
    /* 3/5
     * W Opsslandii zbliżają się wybory parlamentarne. Każdy z tamtejszych polityków jest bardzo
stanowczy i zdecydowany: albo popiera drugiego polityka, albo jest jemu przeciwny. W dodatku
każdy polityk zawsze odwzajemnia swoje poparcie: popiera tylko tych polityków, którzy go
popierają, oraz jest przeciwny tylko tym politykom, którzy są jemu przeciwni. Wielu polityków nie
należy jeszcze do żadnej partii, więc myślą o założeniu nowej. W tym celu spotykają się z innymi
politykami i wymieniając swoje poglądy rozpatrują ewentualny skład nowej partii. Prawo
opsslandzkie wymaga, aby do partii należały co najmniej dwie osoby. Jednak wielkim zagrożeniem
dla istnienia partii jest sytuacja rozłamowa, czyli taka, w której polityk spoza partii jest niektórym
jej członkom przeciwny, a niektórych członków popiera. Sytuacja rozłamowa grozi rozpadem partii
i jest powszechnie niepożądana. W celu zlikwidowania sytuacji rozłamowej należy przyjąć do partii
polityków, którzy tę sytuację powodują. Wówczas, dzięki przynależności do tej samej partii,
osobiste spory jej członków przestają być istotne i sytuacja rozłamowa przestaje istnieć. Życie w
Opsslandii pokazuje, że im partia jest większa, tym ma większe trudności w stanowczym działaniu,
więc politycy są bardziej skłonni tworzyć partie mniej liczne. Niektórzy częściowo kierują się
również rankingiem zaufania do polityków i chcą wiedzieć, jaką najwyższą i jaką najniższą pozycję
w rankingu zajmują politycy należący do ich partii (w rankingu zaufania znajdują się wszyscy
politycy Opsslandii i nigdy nie ma w nim pozycji ex aequo). Wszystkie te kryteria powodują, że
podczas spotkania dwóch polityków, którzy chcą razem utworzyć nową partię, niezwykle trudno
jest im wyznaczyć jej skład.
Zadanie
Napisz program, który na podstawie danych o wzajemnym poparciu polityków oraz ich pozycjach
w rankingu zaufania wyznaczy dla danych dwóch polityków najmniejszą możliwą liczbę członków
partii, do której należeliby obaj politycy i która nie byłaby w sytuacji rozłamowej, a następnie poda
najwyższą i najniższą pozycję w rankingu zaufania, jaką zajmowaliby politycy tej partii.
Wejście
Pierwsza linia zawiera liczbę całkowitą N (2 ≤ N ≤ 800) oznaczającą liczbę polityków w
Opsslandii. Druga linia zawiera liczbę całkowitą K (0 ≤ K ≤ N*(N-1)/2). W każdej z kolejnych K
linii znajdują się dwie różne oddzielone spacją liczby całkowite A i B (1 ≤ A, B ≤ N) oznaczające,
że politycy znajdujący się w rankingu zaufania na pozycjach A i B wzajemnie się popierają. Każda
z K par liczb A i B określa inną parę polityków. W następnej linii znajduje się liczba całkowita S (1
≤ S ≤ N*(N-1)/2) oznaczająca liczbę spotkań. Każda z kolejnych S linii zawiera dwie różne
oddzielone spacją liczby całkowite: X i Y (1 ≤ X, Y ≤ N) oznaczające spotkanie dwóch polityków
znajdujących się w rankingu zaufania na pozycjach X i Y.
Wyjście
Dla każdego spotkania należy wypisać w jednej linii trzy liczby rozdzielone spacjami: najmniejszą
możliwą liczbę członków partii, do której należeliby obaj spotykający się politycy i która nie
byłaby w sytuacji rozłamowej, oraz najwyższą i najniższą pozycję w rankingu zaufania, jaką
zajmowaliby politycy tej partii.
     */
    public sealed class Partie : ProblemBase
    {
        protected override string Input => "11\r\n13\r\n1 2\r\n2 3\r\n3 4\r\n4 5\r\n5 6\r\n3 5\r\n4 6\r\n7 8\r\n7 9\r\n7 10\r\n8 10\r\n9 10\r\n10 11\r\n4\r\n4 5\r\n3 6\r\n9 11\r\n6 7";

        protected override string Output => "2 4 5\r\n6 1 6\r\n4 7 11\r\n11 1 11";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[1]);
            List<int>[] supports = Enumerable.Range(0, N).Select(i => new List<int>()).ToArray();
            for (int i = 0; i < N; i++)
            {
                var supps = input[i + 2].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                supports[supps[0]].Add(supps[1]);
                supports[supps[1]].Add(supps[0]);
            }
            int k = int.Parse(input[N + 2]);
            for(int i = 0; i < k; i++)
            {
                var meeting = input[N + i + 3].Split(' ').Select(s => int.Parse(s) - 1).ToArray();
                List<int> party = new(meeting);
                int[] toAdd;
                do
                {
                    toAdd = party.SelectMany(s => supports[s]).Where(s => !party.Contains(s)).GroupBy(g => g).Where(g => g.Count() < party.Count).Select(g => g.First()).ToArray();
                    party.AddRange(toAdd);
                }
                while (toAdd.Length > 0);
                output.Add($"{party.Count} {party.Min() + 1} {party.Max() + 1}");
            }
        }
    }
}
