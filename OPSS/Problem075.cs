namespace OPSS
{
    /* Difficulty: 4/5
     * 
Zadanie polega na obliczeniu wartości podanych wyrażeń.
Wejście
Pierwsza linia wejścia zawiera liczbę całkowitą Q (1 ≤ Q ≤ 20), oznaczającą liczbę zestawów
danych. W następnych liniach opisane są kolejno po sobie zestawy danych. Pierwsza linia każdego
zestawu zawiera liczbę całkowitą N (1 ≤ N ≤ 10000), będącą liczbą wyrażeń. W kolejnych N liniach
zestawu danych opisanych jest N niepustych wyrażeń, po jednym w każdej linii.
Wyrażeniem jest:
1. pojedyncza liczba bez znaku
2. 2 wyrażenia połączone znakiem dodawania '+', znakiem odejmowania '-' lub znakiem
mnożenia '*'
3. wyrażenie ujęte w nawiasy okrągłe
4. wyrażenie poprzedzone minusem, a następnie ujęte w nawiasy okrągłe
5. symbol oznaczający wartość jednego z wyrażeń podanych z zestawie danych ('w1' oznacza
wartość pierwszego wyrażenia, 'w2' - wartość drugiego, 'w3' - trzeciego, itd. aż do N)
Wartość wyrażenia obliczana jest z zachowaniem kolejności wykonywania działań (w ogólnie
przyjęty w matematyce sposób). Wartość dowolnego wyrażenia (również wchodzącego w skład
innego wyrażenia) zawsze zawiera się w przedziale [-10^6, 10^6]. Wyrażenia nie zawierają białych
znaków. Długość każdego wyrażenia jest mniejsza niż 256 znaków.
Wszystkie N wyrażeń można ustawić w takiej kolejności, że w każdym wyrażeniu, jeśli wystąpią
symbole oznaczające wartości wyrażeń, to będą to wartości wyrażeń poprzednich.
Wyjście
W oddzielnych liniach należy wypisać wartości wszystkich wyrażeń ze wszystkich zestawów
danych w tej samej kolejności w jakiej wyrażenia pojawiły się na wejściu.
     */
    public sealed class Wyrazenia : ProblemBase
    {
        protected override string Input => "2\r\n4\r\n8*4\r\n19\r\n3+17-81+12\r\n(-9)*(-3)\r\n3\r\nw2-1\r\n5\r\n(-w1*(2+w2))";

        protected override string Output => "32\r\n19\r\n-49\r\n27\r\n4\r\n5\r\n-28";

        enum TokenType
        {
            Number,
            Operation,
            Expression
        }

        enum Operation
        {
            Add,
            Sub,
            Mul,
            Div,
        }

        class Token
        {
            public TokenType Type;
            public Operation Operation;
            public int Value;
            public double Level;

            public override string ToString() => $"Type: {Type}, Value: {(Type == TokenType.Operation ? Operation : Value)}, Level: {Level}";
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            int j = 1;
            for (int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[j]);
                j++;
                List<Token>[] tokens = new List<Token>[a];
                Dictionary<int, List<(int, int)>> dependencies = [];
                int[] waiting = new int[a];
                for (int k = 0; k < a; k++)
                {
                    tokens[k] = [];
                    double level = 0;
                    bool neg = false, expr = false, first = true, evalImmediate = true;
                    int l = 0;
                    while (l < input[j].Length)
                    {
                        switch (input[j][l])
                        {
                            case '(':
                                level++;
                                first = true;
                                l++;
                                continue;

                            case ')':
                                level--;
                                l++;
                                continue;

                            case '+':
                                tokens[k].Add(new Token()
                                {
                                    Level = level,
                                    Type = TokenType.Operation,
                                    Operation = Operation.Add
                                });
                                l++;
                                continue;

                            case '-':
                                if (first)
                                {
                                    neg = true;
                                    first = false;
                                }
                                else
                                    tokens[k].Add(new Token()
                                    {
                                        Level = level,
                                        Type = TokenType.Operation,
                                        Operation = Operation.Sub
                                    });
                                l++;
                                continue;

                            case '/':
                                tokens[k].Add(new Token()
                                {
                                    Level = level + 0.5,
                                    Type = TokenType.Operation,
                                    Operation = Operation.Div
                                });
                                l++;
                                continue;

                            case '*':
                                tokens[k].Add(new Token()
                                {
                                    Level = level + 0.5,
                                    Type = TokenType.Operation,
                                    Operation = Operation.Mul
                                });
                                l++;
                                continue;

                            case 'w':
                                expr = true;
                                l++;
                                break;
                        }
                        int res = 0;
                        while (l < input[j].Length && input[j][l] >= '0' && input[j][l] <= '9')
                        {
                            res = 10 * res + input[j][l] - '0';
                            l++;
                        }
                        first = false;
                        if (expr)
                        {
                            res--;
                            if (tokens[res]?.Count == 1)
                            {
                                tokens[k].Add(new Token()
                                {
                                    Level = level,
                                    Type = TokenType.Number,
                                    Value = tokens[res][0].Value * (neg ? -1 : 1),
                                });
                            }
                            else
                            {
                                if(neg)
                                {
                                    tokens[k].Add(new Token()
                                    {
                                        Level = level,
                                        Type = TokenType.Number,
                                        Value = -1
                                    });
                                    tokens[k].Add(new Token()
                                    {
                                        Level = level + 0.5,
                                        Type = TokenType.Operation,
                                        Operation = Operation.Mul
                                    });
                                }
                                tokens[k].Add(new Token()
                                {
                                    Level = level,
                                    Type = TokenType.Expression,
                                    Value = res
                                });
                                waiting[k]++;
                                if (!dependencies.ContainsKey(res))
                                    dependencies.Add(res, []);
                                dependencies[res].Add((k, tokens[k].Count - 1));
                                evalImmediate = false;
                            }
                            expr = false;
                            neg = false;
                        }
                        else
                        {
                            if (neg)
                            {
                                neg = false;
                                res *= -1;
                            }
                            tokens[k].Add(new Token()
                            {
                                Level = level,
                                Type = TokenType.Number,
                                Value = res
                            });
                        }
                    }
                    if (evalImmediate)
                    {
                        Eval(tokens, k, dependencies, waiting);
                    }
                    j++;
                }
                while (dependencies.Any())
                {
                    var key = dependencies.Keys.FirstOrDefault(k => tokens[k].Count == 1);
                    Eval(tokens, key, dependencies, waiting);
                    dependencies.Remove(key);
                }
                foreach (var o in tokens)
                    output.Add(o[0].Value.ToString());
            }
        }

        private void Eval(List<Token>[] tokens, int index, Dictionary<int, List<(int, int)>> dependencies, int[] waiting)
        {
            int levelStart = 0;
            var tab = tokens[index];
            while (tab.Count > 1)
            {
                int j = 0;
                var comp = tab[j + 1].Level.CompareTo(tab[j].Level);
            loop:
                while (j < tab.Count - 1 && comp >= 0)
                {
                    j++;
                    if (comp > 0)
                        levelStart = j;
                    if (j == tab.Count - 1)
                        break;
                    comp = tab[j + 1].Level.CompareTo(tab[j].Level);
                }
                if (j == levelStart && tab[j].Type == TokenType.Number)
                {
                    tab[j].Level--;
                    if (j == tab.Count - 1)
                    {
                        j--;
                        levelStart = j;
                    }
                    comp = tab[j + 1].Level.CompareTo(tab[j].Level);
                    goto loop;
                }
                if (tab[j].Level - (int)tab[j - 1].Level == 0.5)
                {
                    j++;
                    levelStart--;
                }
                int res = 0;
                switch (tab[levelStart + 1].Operation)
                {
                    case Operation.Sub:
                        res = tab[levelStart].Value - tab[levelStart + 2].Value;
                        break;

                    case Operation.Div:
                        res = tab[levelStart].Value / tab[levelStart + 2].Value;
                        break;

                    case Operation.Add:
                        res = tab[levelStart].Value + tab[levelStart + 2].Value;
                        break;

                    case Operation.Mul:
                        res = tab[levelStart].Value * tab[levelStart + 2].Value;
                        break;
                }
                var toAdd = new Token()
                {
                    Level = (int)tab[levelStart + 1].Level,
                    Type = TokenType.Number,
                    Value = res
                };
                tab.RemoveRange(levelStart, 3);

                if (tab.Count > levelStart)

                    tab.Insert(levelStart, toAdd);
                else
                    tab.Add(toAdd);
            }
            int val = tab[0].Value;
            if (dependencies.ContainsKey(index))
            {
                foreach (var l in dependencies[index])
                {
                    tokens[l.Item1][l.Item2] = new Token()
                    {
                        Level = tokens[l.Item1][l.Item2].Level,
                        Type = TokenType.Number,
                        Value = val
                    };
                    waiting[l.Item1]--;
                    if (waiting[l.Item1] == 0)
                        Eval(tokens, l.Item1, dependencies, waiting);
                }
                dependencies.Remove(index);
            }
        }
    }
}
