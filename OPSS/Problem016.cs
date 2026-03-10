namespace OPSS
{
    /* Difficulty: 3/5
     * 
Dziesięciu matematyków leci balonem nad Oceanem Spokojnym. Kiedy przekroczyli równik,
zdecydowali uczcić to zdarzenie, otwierając butelkę szampana. Niestety, korek wybił dziurę w
balonie. Wodór zaczął się ulatniać, powodując opadanie balonu. Wkrótce spadnie do oceanu, a
baloniarze zostaną zjedzeni przez wygłodniałe rekiny.
Ale nie wszystko stracone. Jeden z baloniarzy może się poświęcić wyskakując z balonu, w celu
przedłużenia życia pozostałym chociaż na krótką chwilę. Ciągle istnieje jeden problem - kto ma
wyskoczyć? Matematycy wymyślili, że każdy z nich napisze liczbę naturalną ai nie mniejszą niż 1 i
nie większą niż 10000. Następnie obliczają magiczną liczbę N, która jest liczbą wszystkich
dodatnich dzielników iloczynu a1*a2*...*a10. Na przykład, liczbą dodatnich dzielników liczby 6
jest 4 (dzielniki: 1,2,3,6). Bohater (matematyk, który wyskoczy) jest wyznaczony przez ostatnią
cyfrę N.
Twoim zadaniem, jest wyznaczenie tej cyfry.
Wejście
Wejście zawiera dziesięć liczb, każda w oddzielnej linii.
Wyjście
Wyjście powinno zawierać jedną cyfrę 0-9 (ostatnia cyfra liczby N).
     */
    public sealed class DzielniBaloniarze : ProblemBase
    {
        protected override string Input => "1\r\n2\r\n6\r\n1\r\n3\r\n1\r\n1\r\n1\r\n1\r\n1";

        protected override string Output => "9";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            List<int> primes = [2, 3];
            for (int i = 5; i <= 1000; i  += 2)
            {
                int index = 0;
                int limit = (int)Math.Sqrt(i);
                bool add = true;
                while (primes[index] <= limit)
                {
                    if(i % primes[index] != 0)
                    {
                        add = false;
                        break;
                    }
                    index++;
                }
                if (add)
                    primes.Add(i);
            }
            int[] counts = new int[primes.Count];
            for(int i = 0; i < 10; i++)
            {
                int c = int.Parse(input[i]);
                int j = 0;
                while(c > 1)
                {
                    while(c % primes[j] == 0)
                    {
                        c /= primes[j];
                        counts[j]++;
                    }
                    j++;
                }
            }
            int d = 1;
            for (int i = 0; i < counts.Length; i++)
                if (counts[i] > 0)
                    d = (d * (counts[i] + 1)) % 10;
            output.Add(d.ToString());
        }
    }
}
