using System.Text;

namespace OPSS
{
    /* Difficulty: 1/5
     * 
Rekursywna bakteria czwórkowa to bakteria, która żyje na powierzchniach kwadratowych. Bakteria
składa się z jednego chromosomu typu B, C (chromosomy proste) lub S (chromosomu złożonego).
Chromosom złożony S zbudowany jest z czterech komórek, z których każda jest pojedynczą
rekursywną bakterią czwórkową.
Ale to nie wszystko co charakteryzuje taką bakterię. Jej życie możemy podzielić na dwie dekady:
dekadę przeobrażania, a po niej dekadę obumierania. Podczas pierwszej dekady życia pewne
komórki bakterii przekształcają się aż do momentu, kiedy już proces przeobrażania nie może zajść.
Wówczas bakteria zaczyna obumierać (rozpoczyna się druga dekada). Proces przeobrażania polega
na tym, że chromosomy typu S, które składają się z chromosomów tego samego typu (B lub C)
stają się chromosomem tego jednego typu (B lub C). Inaczej mówiąc: 4 komórki tego samego typu
łączą się, tworząc jedną komórkę danego typu.
Do zapisu budowy bakterii używa się tzw. meta-chromosomu - ciągu złożonego ze znaków B, C, S.
Jeżeli bakteria składa się chromosomu prostego B lub C, wówczas jej meta-chromosom ma postać
znaku odpowiednio B lub C. Jeżeli natomiast bakteria składa się z chromosomu złożonego S,
wówczas jej meta-chromosom jest ciągiem opisu meta-chromosomów bakterii wchodzących w
skład S.
+----+----+----+----+
| | | | |
| C | C | C | B |
+----+----+----+----+
| | | | |
| C | C | C | B |
+----+----+----+----+
| | | |
| | B | B |
| B +----+----+
| | | |
| | B | B |
+---------+----+----+
Rys. Rekursywna bakteria czwórkowa (cykl 1)
Meta-chromosom dla bakterii na powyższym rysunku będzie miał postać:
SSCCCCSCBCBBSBBBB. Po procesie przeobrażania (każde przekształcenie trwa 1 cykl) bakteria
będzie miała postać:
+---------+----+----+
| | | |
| | C | B |
| C +----+----+
| | | |
| | C | B |
+---------+----+----+
| | |
| | |
| B | B |
| | |
| | |
+---------+---------+
Rys. Rekursywna bakteria czwórkowa (cykl 2)
a opisujący ją meta-chromosom skróci się do ciągu: SCSCBCBBB w czasie 2 cykli.
Zadanie
Twoim zadaniem jest oprogramowanie systemu dla bakteriologów. Dla zadanej rekursywnej
bakterii czwórkowej w postaci meta-chromosomu, wyznacz postać meta-chromosomu oraz numer
cyklu, od którego bakteria zacznie obumierać (czyli przeobrażanie bakterii nie będzie więcej
zachodzić).
Wejście
W pierwszej linii wejścia znajduje się liczba C, określająca liczbę zestawów danych, 1 ≤ C ≤ 30. W
kolejnych wierszach wejścia znajdują się zestawy danych. Każdy z C zestawów danych składa się z
wiersza zawierającego niepusty ciąg znaków, będący meta-chromosomem. Długość ciągu nie
przekracza 100000.
Wyjście
Dla każdego zestawu danych, w osobnej linii wyjścia, należy wypisać meta-chromosom oraz numer
cyklu (oddzielone pojedyncza spacją), od którego bakteria zacznie obumierać.
     */
    public sealed class RekursywnaBakteriaCzworkowa : ProblemBase
    {
        protected override string Input => "3\r\nSSCCCCSCBCBBSBBBB\r\nSSCCCCSCCCCSCCCCSCCCC\r\nSBBBB";

        protected override string Output => "SCSCBCBBB 2\r\nC 3\r\nB 2";

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for (int i = 1; i <= N; i++)
            {
                StringBuilder newGenome = new(input[i]);
                int cycle = 0;
                int len;
                do
                {
                    len = newGenome.Length;
                    newGenome.Replace("SCCCC", "C").Replace("SBBBB", "B");
                    cycle++;
                }
                while (len > newGenome.Length);

                output.Add($"{newGenome} {cycle}");
            }
        }
    }
}
