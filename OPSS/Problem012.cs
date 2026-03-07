using System.Text;

namespace OPSS
{
    /* 1/5
     * 
Mnisi podczas studiowania Starego Testamentu natrafili na liczne fragmenty zapisane szyfrem
atbasz (tradycyjną hebrajską odmianią szyfru podstawieniowego).
Atbasz polega na zastąpieniu litery położonej w pewnym miejscu, licząc od początka alfabetu, literą
położoną w tym samym miejscu licząc od końca.
Pracowici mnisi stworzyli różne alfabety, wypisali wszystkie odnalezione fragmenty i uświadomili
sobie jak dużo jeszcze przed nimi pracy.
Pomoż zatem biblistom (bo zależy im na wiedzy i czasie) rozkodować znalezione fragmenty.
Wejście:
W pierwszym wierszu znajduje się alfabet (są to znaki ascii z przedziału [33..126] czyli ['!'..'~']. Nie
muszą być posortowane zgodnie z ich kodami ascii! W następnej linii znajduje sie liczba 0 < N <
1000000 fragmentów znalezionych przez mnichów.
W kolejnych N liniach znajdują się zakodowane słowa szyfrem atbasz (każde zawiera minimalnie
jeden znak i maksymalnie 32 znaki)
Wyjście:
W kolejnych N wierszach powinny pojawić się słowa rozkodowane przez Twój program zgodnie z
kolejnością ich wypisania przez mnichów
     */
    public sealed class StaryTestament : ProblemBase
    {
        protected override string Input => "ZaBoW9#At\r\n3\r\nZtA\r\nBo99###aa\r\nttAA##99WWooBBaZ";

        protected override string Output => "tZa\r\n#9ooBBBAA\r\nZZaaBBooWW99##At";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            string alphabet = input[0];
            Dictionary<char, char> cipher = new();
            for (int i = 0; i < alphabet.Length; i++) 
            {
                cipher.Add(alphabet[i], alphabet[alphabet.Length - i - 1]);
            }
            int N = int.Parse(input[1]);
            for(int i = 2; i <= N + 1; i++)
            {
                StringBuilder msg = new();
                foreach (char c in input[i])
                    msg.Append(cipher[c]);
                output.Add(msg.ToString());
            }
        }
    }
}
