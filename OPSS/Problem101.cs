using System.Text;

namespace OPSS
{
    /* 3/5
     * 
Krzyś ma kłopoty z działaniami na ułamkach. Jego tata, chcąc pomóc synkowi w szkolnych
problemach zaproponował mu zabawę w dodawanie "nietypowych" ułamków. Nietypowość polega
na tym, że sposób (format) zapisywania ułamków jest bardzo odmienny od tego, którego nauczają
w szkole.
Przyłącz się do tej edukacyjnej zabawy.
Format zapisywania ułamków to "a", "a/b/c" lub "b/c", gdzie a, b, c są liczbami całkowitymi
nieujemnymi. Zapis "a" oznacza liczbę całkowitą nieujemną, zapis "a/b/c" oznacza liczbę będącą
tzw. ułamkiem mieszanym (a+b/c), gdzie część ułamkowa jest właściwa ale może nie być skrócona,
zaś zapis "b/c" oznacza ułamek właściwy, który może nie być skrócony.
Zadanie
Obliczyć sumę dwóch ułamków podanych w "nietypowym" formacie. Wynik przedstawić również
w "nietypowym" formacie ze skróconą częścią ułamkową.
Wejście
W pierwszej linii wejścia znajduje się liczba D, 1 ≤ D ≤ 1000, oznaczająca liczbę zestawów danych.
W kolejnych D wierszach występują po dwa ułamki, które należy zsumować. Każdy ułamek
zapisany jest w "nietypowym" formacie, tj. "a", "a/b/c" lub "b/c", gdzie a, b, c są liczbami
całkowitymi, 0 ≤ a ≤ 10000, 0 < b < c ≤ 10000. Ułamki oddzielone są od siebie znakiem "+".
Wyjście
Na wyjściu w osobnej linii dla każdego zestawu należy wypisać sumę dwóch ułamków w
"nietypowym" formacie przy czym części ułamkowe muszą być ułamkami nieskracalnymi.
     */
    public sealed class Ulamki : ProblemBase
    {
        protected override string Input => "2\r\n10+7/8\r\n1/2/3+1/1/2";

        protected override string Output => "10/7/8\r\n3/1/6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split('+');
                var val1 = splits[0].Split('/').Select(s => int.Parse(s)).ToArray();
                var val2 = splits[1].Split('/').Select(s => int.Parse(s)).ToArray();
                int[] results = [0, 0, 1];
                if (val1.Length != 2)
                    results[0] = val1[0];
                if(val1.Length > 1)
                {
                    results[1] = val1[val1.Length - 2];
                    results[2] = val1[val1.Length - 1];
                }
                if (val2.Length != 2)
                    results[0] += val2[0];
                if (val2.Length > 1)
                {
                    results[1] = results[1] * val2[val2.Length - 1] + val2[val2.Length - 2] * results[2];
                    results[2] *= val2[val2.Length - 1];
                    int whole = results[1] / results[2];
                    results[0] += whole;
                    results[1] -= whole * results[2];
                }
                int a = results[1], b = results[2];
                while(a > 0)
                {
                    int temp = a;
                    a = b % a;
                    b = temp;
                }
                if(b > 1)
                {
                    results[1] /= b;
                    results[2] /= b;
                }
                StringBuilder sb = new();
                if (results[0] > 0)
                    sb.Append(results[0]);
                if (results[1] > 0)
                {
                    if (sb.Length > 0)
                        sb.Append("/");
                    sb.Append($"{results[1]}/{results[2]}");
                }
                output.Add(sb.ToString());
            }
        }
    }
}
