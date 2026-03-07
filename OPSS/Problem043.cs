namespace OPSS
{
    /* 3/5
     * 
Po sukcesie rynkowym, jakim było wyprodukowanie bazy danych Mir 13k, dla firmy Miracle
nastał okres dynamicznego rozwoju. Dział programistów znacznie się powiększył i postanowiono
przenieść go do nowej siedziby. Aby zminimalizować koszty związane ze zmianą miejsca pracy
ustalono następujący plan przeprowadzki:
1) każdy programista musi spakować swoje rzeczy do pojemnika, wynieść go przed siedzibę firmy i
wstawić na końcu szeregu pojemników innych pracowników (pojemniki będą tworzyły jeden
szereg w kolejności wynoszenia)
2) wszystkie pojemniki w szeregu zostaną zważone i podzielone na K grup z zachowaniem
kolejności wynoszenia
3) jedna ciężarówka przewiezie kolejno grupy pojemników do nowej siedziby wykonując dokładnie
K kursów pomiędzy siedzibami Miracle
4) pojemniki będą rozpakowane w takiej kolejności, w jakiej były wynoszone przez pracowników
Ustalono z firmą transportową, że koszt przewiezienia rzeczy pracowników jest wprost
proporcjonalny do ładowności ciężarówki (ustalona liczba kursów K nie ma wpływu na koszt
transportu). Miracle chce przewieźć pojemniki możliwie najmniejszym kosztem. Pomóż jej
podzielić pojemniki na K grup, bez zmiany kolejności, tak by sumaryczna waga przedmiotów w
najcięższej grupie była najmniejsza. Liczba kursów K nie ma wpływu na koszt przewozu, więc
może się nawet zdarzyć sytuacja, w której ciężarówka kursuje z pustym ładunkiem.
Wejście
W pierwszym wierszu znajdują się dwie liczby całkowite 1 ≤ N ≤ 1000 i 1 ≤ K ≤ 100 oznaczające
odpowiednio ilość pojemników (N) i liczbę kursów (K). W drugim i ostatnim wierszu jest N liczb
całkowitych dodatnich 1 ≤ xi ≤ 1000, określających wagi poszczególnych pojemników. Wagi są
podane w kolejności wynoszenie ze starej siedziby.
Wyjście
Na wyjściu trzeba podać minimalną sumaryczną wagę najcięższej grupy pojemników, przy podziale
N pojemników na K grup.
     */
    public sealed class Przeprowadzka : ProblemBase
    {
        protected override string Input => "6 3\r\n1 1 2 3 5 7";

        protected override string Output => "7";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int N = int.Parse(splits[0]), K = int.Parse(splits[1]);
            var boxes = input[1].Split(' ').Select(s => int.Parse(s)).ToList();
            while (boxes.Count > K)
            {
                if (boxes.Count == 2)
                {
                    boxes[0] += boxes[1];
                    boxes.RemoveAt(1);
                }
                else
                {
                    int index = boxes.IndexOf(boxes.Min());
                    if (index == 0)
                    {
                        boxes[0] += boxes[1];
                        boxes.RemoveAt(1);
                    }
                    else if (index == boxes.Count - 1 || boxes[index - 1] < boxes[index + 1])
                    {
                        boxes[index - 1] += boxes[index];
                        boxes.RemoveAt(index);
                    }
                    else
                    {
                        boxes[index + 1] += boxes[index];
                        boxes.RemoveAt(index);
                    }
                }
            }
            output.Add(boxes.Max().ToString());
        }
    }
}
