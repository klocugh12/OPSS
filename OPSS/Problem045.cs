namespace OPSS
{
    /* Difficulty: 2/5
     * 
Zespół ludzi tworzących system OPSS chce stworzyć specjalny język programowania OPS., który
będzie pomagał im w generowaniu testów do zadań konkursowych. Jednak ze względu na brak
czasu Tobie powierzają napisanie jego interpretera. Język ma być prosty i ma korzystać z notacji
postfiksowej.
Interpreter powinien wykonywać program w kolejności występowania w nim poleceń (symboli).
Poleceniem może być liczba całkowita bądź jedna z 4 operacji: 'O', 'P', 'S', '.'. Po napotkaniu liczby
interpreter powinien umieścić ją na szczycie stosu. Interpreter po napotkaniu operacji:
● O (Odejmij) - powinien zdjąć ze stosu liczbę L1, następnie zdjąć drugą liczbę L2 i umieścić
na szczycie stosu ich różnicę (L2-L1),
● P (Przemnóż) - powinien zdjąć dwie liczby ze szczytu stosu a następnie umieścić na stosie
ich iloczyn,
● S (Sumuj) - powinien zdjąć dwie liczby ze stosu a następnie na stosie umieścić ich sumę,
● . (Koniec) - powinien zakończyć wykonywanie programu, zdejmować kolejno wszystkie
liczby ze stosu i wypisać je na standardowe wyjście.
Wejście
W pierwszym wierszu znajduje się liczba C, oznaczająca ilość programów, 0 < C ≤ 100. W
następnych C liniach dostajemy treści programów, w których polecenia oddzielone są
pojedynczymi spacjami. Każdy program kończy się operacją '.'. Programy będą gwarantować, że
wartość bezwzględna wyniku każdej operacji 'O', 'P', 'S' nie przekroczy 231-1. Każdy program może
się składać maksymalnie z 500000 symboli.
Wyjście
Dla każdego programu w osobnej linijce wyjścia powinien znaleźć się wynik działania danego
programu w języku OPS. ;-)
     */
    public sealed class OPS : ProblemBase
    {
        protected override string Input => "3\r\n6502 68000 6502 68000 6502 O P S S 2005 4 2 .\r\n11 22 33 44 O S P .\r\n10 9 O -1 S 9 8 7 6 5 4 3 2 1 1 P .";

        protected override string Output => "2 4 2005 399934498\r\n121\r\n1 2 3 4 5 6 7 8 9 0";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ');
                Stack<int> stack = new();
                foreach (string s in splits) 
                {
                    int number;
                    if(int.TryParse(s, out number))
                        stack.Push(number);
                    else
                    {
                        switch(s)
                        {
                            case "O":
                                int c = stack.Pop();
                                stack.Push(stack.Pop() - c);
                                break;

                            case "P":
                                stack.Push(stack.Pop() * stack.Pop()); 
                                break;

                            case "S":
                                stack.Push(stack.Pop() + stack.Pop());
                                break;
                        }
                    }
                }
                output.Add(string.Join(" ", stack));
            }
        }
    }
}
