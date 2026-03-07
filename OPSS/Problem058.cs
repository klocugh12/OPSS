namespace OPSS
{
    /* 4/5
     * Firma Miracle, idąc z duchem czasu postanowiła stać się firmą przyjazną środowisku naturalnemu
(w chwili obecnej 30% energii elektrycznej pochodzi ze źródeł odnawialnych). Zarząd firmy podjął
decyzję, że wszystkie odpady bedą segregowane i umieszczane w specjalnych kontenerach, w
każdym inny rodzaj. Segregowanie odpadów powoduje dość duże przestoje w pracy - więc w
interesie Miracle jest, aby trwało możliwie najkrócej.
Odpady będą segregowane przez robota, który w danym momencie może przenosić tylko jeden
przedmiot. Czynności wykonywane przez robota sprowadzają się do wyjęcia przedmiotu z
kontenera i przełożenie go do docelowego kontenera. Jeden przedmiot może być przeniesiony tylko
jeden raz. Przyjmujemy, że koszt przeniesienia jednego przedmiotu wynosi 1 jednostkę czasu.
Zakładamy, że kontenery mają nieograniczoną pojemność. Jeżeli przedmioty są posortowane, to
robot kończy pracę.
W celu poprawnego zaplanowania całej operacji należy określić, ile czasu zajmuje segregowanie w
najlepszym i najgorszym przypadku.
Wejście
W pierwszym wierszu znajduje się liczba kontenerów N, 1≤ N≤ 200 równa liczbie rodzajów
odpadów. W kolejnych N wierszach znajduje się opis zawartości kontenerów. Zawartość każdego
kontenera określa N liczb x1,x2,..., xn równych odpowiednio ilości przedmiotów rodzaju 1,2..n 0≤ xi≤
1000, 1≤ i≤ N.
Wyjście
Na wyjściu powinny znaleźć się dwie liczby równe odpowiednio minimalnej i maksymalnej liczbie
jednostek czasu potrzebnej na posegregowanie odpadów.
     */
    public sealed class Segregacja : ProblemBase
    {
        protected override string Input => "2\r\n2 3\r\n4 3";

        protected override string Output => "5 7";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            List<List<int>> infos = [];
            
            for (int i = 1; i <= N; i++)
            {
                var splits = input[i].Split(' ').Select(s => int.Parse(s)).ToList();
                infos.Add(splits);
            }
            List<int> mins = [0], maxs = [0];
            for (int i = 1; i < N; i++)
            {
                List<int> stayMin = [], stayMax = [], switchMin = [], switchMax = [];
                int sum = 0;
                for (int j = 0; j <= i; j++)
                    sum += infos[j][i];
                for (int j = 0; j < i; j++)
                {
                    switchMin.Add(infos[j][mins[j]] + sum - infos[j][i]);
                    switchMax.Add(infos[j][maxs[j]] + sum - infos[j][i]);
                    stayMin.Add(infos[j][i] + infos[i][mins[j]]);
                    stayMax.Add(infos[j][i] + infos[i][mins[j]]);
                }
                int min = 0, max = 0;
                for(int j = 1; j < i; j++)
                {
                    int d = switchMin[j] - stayMin[j];
                    if (d < switchMin[min] - stayMin[min])
                        min = j;
                    if (d > switchMin[max] - stayMin[max])
                        min = j;
                }
                if (switchMin[min] - stayMin[min] < 0)
                {
                    int m2 = mins.IndexOf(min);
                    mins[m2] = i;
                    mins.Add(min);
                    for (int j = 0; j <= i; j++)
                        infos[j][mins[j]] = switchMin[min];
                }
                else
                {
                    mins.Add(i);
                    for (int j = 0; j < i; j++)
                        infos[j][mins[j]] = stayMin[min];    
                }
                if (switchMax[max] - stayMax[max] > 0)
                {
                    int m2 = maxs.IndexOf(max);
                    maxs[m2] = i;
                    maxs.Add(max);
                    for (int j = 0; j <= i; j++)
                        infos[j][maxs[j]] = switchMax[max];
                }
                else
                {
                    maxs.Add(i);
                    for (int j = 0; j <= i; j++)
                        infos[j][maxs[j]] = stayMax[max];                   
                }
            }
            output.Add($"{infos[0][mins[0]]} {infos[0][maxs[0]]}");
        }
    }
}
