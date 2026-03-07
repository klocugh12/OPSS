namespace OPSS
{
    /* 1/5
     * 
Firma DB-Bit, główny potentat na rynku baz danych w Opsslandii, postanowiła rozszerzyć swój
produkt o nowe możliwości wyszukiwania słów w tekście. Firmowi badacze, po przeanalizowaniu
różnorodnych zbiorów tekstowych, postawili hipotezę, że najlepszym rozwiązaniem będzie oparcie
nowej technologii na słowach, które mają więcej samogłosek (małe lub duże litery: 'a','e','i','o','u','y')
niż spółgłosek i zawierają przynajmniej jedną spółgłoskę. Słowa o tej własności nazwali słowami
dźwięcznymi. W Opsslandii spółgłoski zapisuje się używając tylko jednej litery.
W celu wykazania słuszności hipotezy, naukowcy muszą sprawdzić częstość występowania
dźwięcznych słów w zadanym tekście. Naukowcy nie potrafią programować, a programiści firmy
DB-Bit są zbyt obciążeni pracą. Pomóż ambitnym badaczom.
Zadanie
Napisz program, który wyznaczy liczbę dźwięcznych słów w podanej liście.
Wejście
W pierwszym wierszu znajduje się liczba słów N, 1 ≤ N ≤ 1000. W kolejnych N wierszach znajdują
się niepuste słowa składające się z co najwyżej 100 liter (dużych i małych) alfabetu angielskiego.
Wyjście
Na wyjściu powinna znaleźć się tylko jedna liczba określająca ilość dźwięcznych słów.
     */
    public sealed class DzwieczneSlowa : ProblemBase
    {
        protected override string Input => "3\r\naBecadLo\r\nOPSS\r\naaba";

        protected override string Output => "1";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int total = 0;
            for(int i = 1; i <= N; i++)
            {
                char[] vowels = ['a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y'];
                string s = input[i];
                int count = s.Count(c => vowels.Contains(c));
                if (count > (s.Length >> 1) && count < s.Length)
                    total++;
            }
            output.Add(total.ToString());
        }
    }
}
