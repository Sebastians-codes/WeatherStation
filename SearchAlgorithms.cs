namespace WeatherStation;
public class SearchAlgorithms
{
    // Söker igenom varje element och om lika med söktemp retureras vilket indexet den hittades på. annars -1

    /*
        Pseudokod

        för varje element i städer
            om elementets temp är lika med sök temp
                returera index av element

        annars returera -1
    */
    public int Linsok(List<Stad> städer, int n, int sökTemp)
    {
        for (int i = 0; i < n; i++)
            if (städer[i].Temp == sökTemp)
                return i;

        return -1;
    }

    // BinarySearch gjort med recursion
    /*
        BinarySearch fungerar genom att ta det första och sista indexet
        plussa ihop dom och dela på 2 och avrunda neråt till närmsta heltal denna kallar vi pivot.
        Detta nummer används sedan som index och om elementet i listan på
        det indexet är lika med sök tempen så retureras det indexet som tempen hittades på.

        Om elementet på indexet av pivot är mindre än sök tempen så körs metoden igen men denna gången
        halveras sök ytan till det elementet som är + 1 från vårat förra resultat.

        Om elementet mer så körs metoden igen fast halverad från andra sidan med pivot - 1.

        Om sök tempen inte kan hittas retureras - 1.
    */
    public int BinarySearch(List<Stad> städer, int left, int right, int temp)
    {
        // För att undvika integer overflow. behövs inte i detta fall.
        //int pivot = left + (right - left) / 2;
        int pivot = (left + right) / 2;

        // Om left start är mer än höger right betyder det att temperaturen inte kunde hittas och -1 retureras.
        if (left > right)
            return -1;

        // Om tempen av staden med index av pivot är lika med sök temp retureras indexen för staden med samma söktemp.
        if (städer[pivot].Temp == temp)
            return pivot;

        // Om tempen av index pivot är mindre än sök, körs metoden igen men med start av pivot + 1.
        if (städer[pivot].Temp < temp)
            return BinarySearch(städer, pivot + 1, right, temp);

        // Annars är elemntet mer än sök och då körs metoden igen men med slut "right" av pivot - 1.
        return BinarySearch(städer, left, pivot - 1, temp);
    }

    /*
        Metod för att skriva ut staden med högst temperatur med hjälp av ^1 som ger sista index
        i arrayen och eftersom den är sorterad så är sista index den staden med högst temperatur.
    */
    public string HögstTemp(List<Stad> städer) => städer[^1].ToString();

    /*
        Metod för att skriva ut staden med lägst temperatur med hjälp av 0 som ger första index
        i arrayen och eftersom den är sorterad så är det första indexet den staden med lägst temperatur.
    */
    public string LägstTemp(List<Stad> städer) => städer[0].ToString();

    /*
        Metod för att hitta både lägst och högst temperatur i en osorterad lista.
        stad är lika med den första staden i arrayen
        och temp är like med temperaturen från denna stad.

        om maxTemp är true letar vi efter staden med högst temp
        med en for loop
        om stadens temperatur med index i är mer än temp så får temp värdet
        av staden med index i's temperatur.
        och stad för värdet av staden med index i
        när loopen är klar kommer vi ha staden med högst
        för att hitta lägsta måste maxTemp vara false
        då fungerar programmet lika dant bara att den ändrar bara stad och temp
        variablerna om det är värdena är mindre än dom som är i stad och temp.
        
    */
    public string FindMinMaxUnsorted(List<Stad> städer, bool maxTemp)
    {
        Stad stad = städer[0];
        int temp = städer[0].Temp;
        for (int i = 1; i < städer.Count; i++)
            if (maxTemp && städer[i].Temp > temp)
            {
                temp = städer[i].Temp;
                stad = städer[i];
            }
            else if (!maxTemp && städer[i].Temp < temp)
            {
                temp = städer[i].Temp;
                stad = städer[i];
            }
        // Ternary för att ändra stringen beroende på vad vi letar efter.
        return $"Staden med {(maxTemp ? "högst" : "lägst")} temperatur är {stad.ToString()}";
    }
}
