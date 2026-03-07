namespace OPSS
{
    /* 2/5
     * 
Spróbuj rozwiązać następującą zagadkę:
"Statek płynie z Warszawy do Gdańska dobę, a z Gdańska do Warszawy dwie doby. Ile płynie
tratwa z Warszawy do Gdańska?"
Krótkie wyjaśnienie: statek płynie po Wiśle i posiada własny napęd o stałej w czasie mocy, a zatem
posiada stałą prędkość względem wody. Kiedy płynie z prądem, pokonuje ten sam dystans szybciej
niż płynąc pod prąd. Tratwa nie posiada własnego napędu i porusza się z prędkością prądu Wisły.
Wyjątkowo podamy rozwiązanie zagadki. Tratwa płynie z Warszawy do Gdańska 4 doby. Twoim
zadaniem będzie jednak rozwiązanie tego problemu dla dowolnych danych.
Wejście:
W jedynym wierszu wejścia znajdują się 2 liczby całkowite: N, M, 1 ≤ N < M ≤ 100000, gdzie N
oznacza czas w jakim statek pokonuje trasę z prądem, a M oznacza czas w jakim ten sam statek,
pokonuje tę samą trasę pod prąd.
Wyjście:
Wynikiem jest jeden wiersz zawierający liczbę oznaczającą czas w jakim trasę pokonuje tratwa.
Gwarantujemy, że dane będą tak dobrane, że wynikiem będzie dodatnia liczba całkowita mniejsza
od 2.
     */
    public sealed class Tratwa : ProblemBase
    {
        protected override string Input => "1 2";

        protected override string Output => "4";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            var splits = input[0].Split(' ');
            int tp = int.Parse(splits[0]), tm = int.Parse(splits[1]);
            //tp(vs + vp) = tm(vs - vp)
            //vs(tm - tp) = vp(tm + tp)
            //vs = vp(tm + tp)/(tm - tp)
            output.Add((tp * ((tm + tp) / (tm - tp) + 1)).ToString());
        }
    }
}
