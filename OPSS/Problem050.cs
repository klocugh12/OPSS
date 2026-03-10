namespace OPSS
{
    /* Difficulty: 3/5
     * Edek zawsze lubił chomiki. Jeszcze w czasach szkolnych opiekował się tymi zwierzętami w
pracowni biologicznej, a w domu hodował chomiki syryjskie. Po maturze wybrał studia na
Wydziale Biologii. Właśnie zbliża się koniec studiów i oczywiście Edek (a teraz właściwie już
Edward) wybrał temat pracy magisterskiej, który, jak zapewne już odgadliście, związany jest z
hodowlą chomików. Niedawno Wydział otrzymał dwie pary chomików egzotycznej rasy, która
dosyć wolno (jak na chomiki) się rozmnaża. Promotor zasugerował Edkowi, aby oszacował
liczebność populacji nowej rasy po kilku latach.
Samice chomików nowej rasy rodzą, przy dobrych warunkach, jedną parę co miesiąc. Małe chomiki
szybko dojrzewają i po miesiącu zdolne są już zostać rodzicami. Z dwóch par chomików, które
otrzymał wydział po miesiącu urodziły się jednak, zapewne z powodu stresu związanego ze zmianą
miejsca pobytu, tylko 2 młode, na szczęście była to para. W drugim miesiącu sytuacja sie
powtórzyła, ale juz w nastepnych miesiącach wszystko potoczyło się normalnie czyli z trzech par
"starych" urodziły się 3 pary młodych itd. Edek zaczął zliczać pary: chomików i na początku praca
choć żmudna szła mu bez specjalnych trudności: pierwszy miesiąc - 3, drugi miesiąc - 4, trzeci - 7,
po roku 521 par. Liczby rosły tak szybko, że Edek zaczął się obawiać czy przypadkiem chomiki nie
będą mnożyć się szybciej niż on będzie dodawał!
Pomóż pracowitemu studentowi biologii oszacować liczbę chomików po wielu latach hodowli,
zakładając, że nowa rasa chomików jest bardzo, bardzo długowieczna. Edek będzie zadowolony
gdy podasz długość i 10 pierwszych cyfr tej liczby.
Wejście
W pierwszej linii wejścia znajduje się liczba D, określająca ilość zestawów danych, 1 ≤ D ≤ 1000.
W kolejnych wierszach wejścia znajdują się zestawy danych. Każdy z D zestawów danych składa
się z wiersza zawierającego jedną dodatnią liczbę naturalną L, oznaczającą liczbę lat hodowli L, 1 ≤
L ≤ 5000.
Wyjście
Dla każdego zestawu danych, w osobnej linii wyjścia, należy wypisać liczbę cyfr N, z których
składa się poszukiwana przez Edka liczba chomików C a po spacji 10 początkowych cyfr liczby C.
W przypadku gdy N < 10, należy wypisać dokładnie N cyfr.
     */
    public sealed class ChomikiEdka : ProblemBase
    {
        protected override string Input => "2\r\n1\r\n4";

        protected override string Output => "3 521\r\n11 1739379600";

        static List<int> Add(List<int> shorter, List<int> longer)
        {
            longer = new(longer);
            bool carry = false;
            int k;
            for(k = 0; k < shorter.Count; k++)
            {
                longer[longer.Count - k - 1] += shorter[shorter.Count - k - 1] + (carry ? 1 : 0);
                int a = longer[longer.Count - k - 1];
                carry = a >= 10;
                if (carry)
                    longer[longer.Count - k - 1] %= 10;
            }
            k = longer.Count - shorter.Count - 1;
            while(carry && k >= 0)
            {
                longer[k]++;
                carry = longer[k] >= 10;
                if(carry)
                    longer[k] %= 10;
            }
            if (k < 0 && carry)
                longer.Insert(0, 1);
            return longer;
        }

        protected override void BuildSolution(string[] input, List<string> output)
        {
            int N = int.Parse(input[0]);
            for(int i = 1; i <= N; i++)
            {
                int a = int.Parse(input[i]) * 12;
                int m = 2;
                List<int> n1 = [3], n2 = [4]; 
                int c = a;
                while(m < a)
                {
                    List<int> temp = n2;
                    n2 = Add(n1, n2);
                    n1 = temp;
                    m++;
                }
                output.Add($"{n2.Count} {string.Join("", n2.Take(Math.Min(n2.Count, 10)))}");
            }
        }
    }
}
