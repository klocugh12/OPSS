using System.Text;

namespace OPSS
{
    /* 3/5
     * 
Liczbą wyważoną nazwiemy dodatnią liczbę naturalną posiadającą tyle samo dzielników
parzystych co nieparzystych. Twoim zadaniem będzie wyznaczenie dla zadanej dodatniej liczby N
najmniejszej liczby wyważonej większej od N.
Wejście
W pierwszym wierszu wejścia znajduje się liczba C, określająca ilość zestawów danych, 1 ≤ C ≤
100. Każdy z C zestawów danych składa się z jednego wiersza zawierającego jedną dodatnią liczbę
naturalną N, składającą się co najwyżej z 200 cyfr.
Wyjście
Dla każdego zestawu danych, w osobnych liniach wyjścia, należy wyznaczyć najmniejszą liczbę
wyważoną większą od zadanej liczby N z każdego zestawu danych.
     */
    public sealed class LiczbyWywazone : ProblemBase
    {
        protected override string Input => "2\r\n1\r\n2";

        protected override string Output => "2\r\n6";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                StringBuilder s = new(input[i]);
                int k = int.Parse(input[i].Substring(Math.Max(input[i].Length - 2, 0)));
                int toAdd = 4 - ((k + 2) % 4);
                k = s.Length - 1;
                bool carry = true;
                while(k >= 0 && carry)
                {
                    int x = (s[k] - '0') + toAdd;
                    carry = x > 9;
                    s[k] = (char)((x % 10) + '0');
                    k--;
                }
                if (carry)
                    s.Insert(0, '1');
                output.Add(s.ToString());
            }
        }
    }
}
