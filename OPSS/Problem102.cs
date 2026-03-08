namespace OPSS
{
    /* 4/5
     * Uwaga! Historia jest w całej rozciągłości fikcją literacką i jakiekolwiek podobieństwo do faktów i
zdarzeń jest zupełnie przypadkowe.
W pobliżu dosyć dużego, uniwersyteckiego miasta postanowiono rozbudować istniejące lotnisko
sportowe. Planowane jest wydłużenie i poszerzenia pasa startowego oraz rozbudowa infrastruktury
lotniska. Inwestycja pewnie ruszyłaby już pełną parą gdyby nie problemy ekologiczne. Trawiaste
lotnisko sportowe zamieszkuje mianowicie dosyć rzadki gatunek susła, który znalazł tu doskonałe
warunki rozwoju. Inwestorzy chcą wyłapać sympatyczne zwierzaki i przenieść je w inne miejsce,
ale ekolodzy uważają, że nigdzie nie znajdzie się tak dobrego siedliska dla susłów i chcą bronić ich
spokoju. Aby zapobiec wyłapywaniu susłów ekolodzy planują otoczyć ich norki "żywym
łańcuchem" biorąc się za ręce.
Znane jest położenie wejść do norek susłów, a problem ekologów polega na określeniu minimalnej
liczby ludzi, którzy potrzebni są do utworzenia "żywego łańcucha". Spróbuj rozwiązać problem
ekologów!
Zadanie
Należy wyznaczyć minimalną liczbę ekologów, którzy potrzebni są do utworzenia "żywego
łańcucha" chroniącego norki susłów przy założeniu, że średnia rozpiętość ramion ekologa (i
ekolożki?) to 150cm. Dodatkowo należy też podać numery punktów oznaczających wejścia do
norek przez które łańcuch będzie przechodził.
Wejście
W pierwszym wierszu wejścia podana jest liczba całkowita 0<L<1001, oznaczająca liczbę
zestawów danych. W każdym z L kolejnych wierszy występuje liczba Ni (2< Ni<200000),
oznaczająca liczbę punktów (norek susłów) oraz Ni różnych par liczb całkowitych xi,yi (-100001<xi,
yi< 100001) oznaczających współrzędne punktów podane w centymetrach. Wszystkie liczby
oddzielone są pojedynczą spacją.
Wyjście
Na wyjściu, w jednym wierszu dla każdego zestawu danych, należy wypisać liczbę ekologów
niezbędną do utworzenia łańcucha oraz listę numerów punktów, przez które łańcuch będzie
przechodził. Lista punktów powinna rozpoczynać się od punktu o najmniejszej współrzędnej x,
przez który będzie przechodził łańcuch a następne punkty na obwodzie łańcucha powinny tworzyć
obieg o kierunku przeciwnym do ruchu wskazówek zegara. Jeżeli jest kilka punktów na obwodzie
łańcucha, których współrzędna x równa jest xmin to punktem początkowym łańcucha jest ten, który
ma najmniejszą współrzędną y. Numeracja punktów jest zgodna z ich kolejnością podaną na
wejściu.
     */
    public sealed class ProtestEkologow : ProblemBase
    {
        protected override string Input => "2\r\n8 500 1000 1200 1400 0 1200 2000 1000 1300 500 0 0 1000 0 1800 0\r\n14 200 1000 1200 1400 500 1000 2000 1000 1300 500 0 0 1000 -200 1800 0 100 500 600 600 1400 200 1400 1000 1600 1200 1900 500";

        protected override string Output => "41 6 7 8 4 2 3\r\n40 6 7 8 14 4 13 2 1 9";

        class PointData
        {
            public int Position;

            public int X;

            public int Y;

            public double Angle;

            public double Radius;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                List<PointData> points = [];
                int minX = int.MaxValue, minY = int.MaxValue;
                var splits = input[i].Split(' ').Skip(1).Select(s => int.Parse(s)).ToArray();
                Dictionary<int, PointData> mapping = [];
                for (int j = 0; j < splits.Length; j += 2)
                {
                    PointData point = new()
                    {
                        X = splits[j],
                        Y = splits[j + 1],
                        Position = (j >> 1) + 1
                    };
                    if (minY > point.Y || (minY == point.Y && minX > point.X))
                    {
                        minX = point.X;
                        minY = point.Y;
                    }
                    points.Add(point);
                    mapping.Add(point.Position, point);
                }
                foreach (var p in points)
                {
                    p.X -= minX;
                    p.Y -= minY;
                    p.Radius = Math.Sqrt(p.X * p.X + p.Y * p.Y);
                    p.Angle = Math.Atan2(p.Y, p.X);
                    if (p.Angle < 0)
                        p.Angle += Math.PI * 2.0;
                }
                double course = 0.0;
                points.Sort((a, b) =>
                {
                    var res = a.Angle.CompareTo(b.Angle);
                    return res != 0 ? res : a.Radius.CompareTo(b.Radius);
                });
                List<int> solution = [points[0].Position];
                for (int k = 1; k < points.Count; k++)
                {
                    var div = Math.Atan2(points[k].Y - mapping[solution[^1]].Y, points[k].X - mapping[solution[^1]].X);
                    if (div < 0)
                        div += Math.PI * 2.0;
                    while (div < course)
                    {
                        solution.RemoveAt(solution.Count - 1);
                        div = Math.Atan2(points[k].Y - mapping[solution[^1]].Y, points[k].X - mapping[solution[^1]].X);
                        if (div < 0)
                            div += Math.PI * 2.0;
                        course = Math.Atan2(mapping[solution[^1]].Y - mapping[solution[^2]].Y, mapping[solution[^1]].X - mapping[solution[^2]].X);
                        if (course < 0)
                            course += Math.PI * 2.0;
                    }
                    solution.Add(points[k].Position);
                    course = div;
                }
                course = 0;
                for (int k = 0; k < solution.Count; k++)
                {
                    var curr = mapping[solution[k]];
                    var prev = mapping[solution[(k + 1) % (solution.Count)]];
                    course += Math.Sqrt((curr.X - prev.X) * (curr.X - prev.X)
                        + (curr.Y - prev.Y) * (curr.Y - prev.Y)); 
                }
                while (mapping[solution[^1]].X < mapping[solution[0]].X || 
                    (mapping[solution[^1]].X == mapping[solution[0]].X && mapping[solution[^1]].Y < mapping[solution[0]].Y))
                {
                    solution.Insert(0, solution[^1]);
                    solution.RemoveAt(solution.Count - 1);
                }
                var lastLine = points.Where(p => p.Angle == mapping[solution[^1]].Angle).ToList();
                if(lastLine.Count > 1)
                {
                    lastLine.Sort((a, b) => -a.Radius.CompareTo(b.Radius));
                    solution.RemoveAt(solution.Count - 1);
                    solution.AddRange(lastLine.Select(p => p.Position));
                }
                output.Add($"{Math.Ceiling(course / 150.0)} {string.Join(" ", solution)}");
            }
        }
    }
}
