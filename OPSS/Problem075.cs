namespace OPSS
{
    /* Difficulty: 4/5
     * You're given set of expressions to evaulate. Each expression can be any of following:
     * 1. A single unsigned number.
     * 2. Two expressions with addition, subtraction, multiplication or division operators (+, -, *, /, respectively).
     * 3. Another expression inside parentheses ().
     * 4. Another expression with leading negative - sign, both contained inside parentheses.
     * 5. A symbol wn, where n is a number of another expression in a data set, starting from 1.
     *    It represents value of another expression to substitute.
     *    
     * Respect standard order of algebraic operations.
     * 
     * Input
     * First line contains number of data sets Q (1 ≤ Q ≤ 20).
     * First line of each data set contains number of expressions N (1 ≤ N ≤ 10000).
     * Following N lines of each data set contain a sngle expression described with above rules.
     * Expressions cannot be empty. Each expression evaluates to value within range [-10^6, 10^6].
     * Whitespaces are not allowed. Length of any expression does not exceed 256 characters.
     * There are no circular dependencies between expressions, so all expressions can be fully evaluated.
     * 
     * Output
     * For each data set write N lines, each containing final value of each respective expression.
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
