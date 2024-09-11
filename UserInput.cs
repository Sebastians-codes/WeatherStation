namespace WeatherStation;

/*
    En klass som tar input från användaren och verifierar att värdet är inom vissa bounds.
    Denna klass blev mer komplicerad än vad jag trodde den skulle bli men det blev bra träning.
*/
public class UserInput
{
    /*
        Två private readonly char arrays detta för att dom kommer aldrig ändras under programmets gång
        och det är bara denna klassen som behöver veta hur metoderna fungerar.

        min max för att sätta boundsen för vad en int kan hålla.
        dessa är const för att annars vet inte kompilern vilket värde som ska användas som default i en annan
        metod.
    */
    private const int _max = 2147483647,
        _min = -2147483648;
    private readonly char[] _specialChars =
    [
        '.', ',', '-', '/', '*', '+', ' ', '!', '"', '#', '$',
        '¤', '%', '&', '@', '£', '€', '1', '2', '3', '4', '5',
        '6', '7', '8', '9', '0'
    ];

    private readonly char[] _temps = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-'];

    /*
        Metod för att ta input från användaren och även skicka ett meddelenade t.ex Skriv ditt namn.
        värdet veriferas i CheckString och om det är godkänt så returerar värdet i TitleCase
        vilket betyder att första bokstaven är Stor och resten små.
    */
    public string GetString(string message = "")
    {
        if (!message.Equals(""))
            Console.Write(message);

        string str = Console.ReadLine();

        if (!CheckString(str))
        {
            Console.WriteLine("Invalid Input. Try again.");
            return GetString(message);
        }

        return TitleCase(str);
    }

    /*
        Jag ville inte använda mig av TryParse eller en Try catch så jag valde att skriva min egna
        verifiering av en int.

        min max är default på lägsta och högsta värdet en int kan vara.
        message är för att du ska kunna fråga om input av användaren.

        Om CheckIfNum veriferar att det går att parsa stringen till en int så görs det annars körs
        metoden igen med recursion.

        sista if är för att man ska kunna sätta eget värde på min max för som för denna inlämning
        då värdet ska vara mellan -60 och 60 passerar den inte denna check körs metoden igen med
        recursion.
    */
    public int GetInt(int min = _min, int max = _max, string message = "")
    {
        if (!message.Equals(""))
            Console.Write(message);

        string num = Console.ReadLine();

        if (!CheckIfNum(num))
            return GetInt(min, max, message);

        int celsius = int.Parse(num);

        if (celsius >= min && celsius <= max)
            return celsius;
        return GetInt(min, max, message);
    }

    /*
        CheckIfNum veriferar om en string kan parsas till en int.
        först kollar den om det ens finns något värde i stringen

        Sen med hjälp av tre metoder så veriferas stringen om den har ett char värde som kan parsas till en int

        en nested forloop för att testa alla chars i stringen mot _temps arrayen
        för att verifera att det inte finns några tecken i stringen som inte får finnas.
        detta försäkrar även att stringen är rätt formaterad och eftersom
        minval och maxval bara checkar med char värdet så finns det strings som kan passera den checken men inte den sista.
    */
    private bool CheckIfNum(string str)
    {
        if (string.Empty.Equals(str))
            return false;

        if (!(MinValue(str) && NumFormat(str)) || !(MaxValue(str) && NumFormat(str)))
            return false;

        for (int i = 0; i < str.Length; i++)
        {
            bool Nan = false;
            for (int j = 0; j < _temps.Length; j++)
            {
                if (char.Equals(str[i], _temps[j]))
                {
                    Nan = false;
                    break;
                }
                else
                    Nan = true;
            }
            if (Nan)
                return false;
        }
        return true;
    }

    /*
        Metod som set till att stringen börjar på rätt sätt
        om den börjar på - och sen 0 är den felaktig eller om
        den börjar på 0 men är stringen är längre än 1 char.

        sen om stringen är längre än 0 så kollas det att det inte finns några
        - på någon mer index i stringen med hjälp av en for loop.
    */
    private bool NumFormat(string str)
    {
        if (
            char.Equals(str[0], '-') && char.Equals(str[1], '0')
            || char.Equals(str[0], '0') && str.Length > 1
        )
            return false;

        if (str.Length > 0)
            for (int i = 1; i < str.Length; i++)
                if (char.Equals(str[i], '-'))
                    return false;

        return true;
    }

    /*
        MinValue och MaxValue är nästan samma och jag skulle kunna göra dom till en metod men nu blev det såhär.

        i min kollar den först om det första indexet är - annars returerar den MinMaxLogic
        i max kollar den först om det första indexet inte är - annars returerar den MinMaxLogic

        metoderna tar respektive värde i string och ger det värdet och värdet från användaren samt
        längen på stringsen till en metod som heter MinMaxLogic
    */

    private bool MinValue(string str)
    {
        if (!char.Equals(str[0], '-'))
            return true;

        string minValue = _min.ToString();
        int strLength = str.Length,
            minLength = minValue.Length;

        return MinMaxLogic(str, minValue, strLength, minLength, false);
    }

    private bool MaxValue(string str)
    {
        if (char.Equals(str[0], '-'))
            return true;

        string maxValue = _max.ToString();
        int strLength = str.Length,
            maxLength = maxValue.Length;

        return MinMaxLogic(str, maxValue, strLength, maxLength, true);
    }

    /*
        MinMaxLogic fungerar genom att ändra index beroende på om stringen har ett - eller inte
        detta sätts med boolen max
        om max är true så är index 0 och hela stringen kollas
        annars är index 1 och första charen som är - kollas ej.

        om längden på stringen från användaren är mer är max längden returerars false
            den är då för lång för att det ska gå att parsa stringen til en int.
        om längden på stringen från användaren är mindre än max längden retureras true
            då är stringen till tillräckligt kort för att parsas till en int.

        om stringen från användaren passerar dessa två betyder det att stringen som användaren har
        angett har samma längd som maxValue och då måste vi kolla värdena i varje index.

        för varje char i längden av maxValue detta är samma som användarens string
            så kollas varje index av användarens string om den charen är mer än samma index i maxValue
            om den är det så är värdet på stringen inte möjligt att parsa till en int
            då värdet överstiger vad en int kan hålla.

            om charen är mindre än charen på samma index i maxValue så är stringen okej
            och då returerars true.

        om stringen går förbi alla checkar så är stringen exakt samma värde som max värdet och då retureras true
    */
    private bool MinMaxLogic(string str, string maxValue, int strLength, int maxLength, bool max
    )
    {
        int index = 0;

        if (!max)
            index = 1;

        if (strLength > maxLength)
            return false;

        if (strLength < maxLength)
            return true;

        for (int i = index; i < maxLength; i++)
        {
            if (str[i] > maxValue[i])
                return false;
            if (str[i] < maxValue[i])
                return true;
        }
        return true;
    }

    /*
        Metod för att om stringen har ett värde som kan användas i metoden.
        waste är hur många tecken som inte kan användas i metod så om waste är mindre än
        stringen längd minus ett betyder det att det måste minst finnas 2 accepterade chars i stringen.
        för att true ska retureras
    */
    private bool CheckString(string str)
    {
        int waste = CountSpecialChars(str);

        if (waste < str.Length - 1)
            return true;

        return false;
    }

    /*
        Metod för att returera en string i TitleCase det vill säga första bokstaven stor resten små.

        först strippas stringen från tecken som inte är tillåtna och sedan skapas en array av chars
        i längedn av den strippade stringen.

        för varje char i arrayen om index är 0
            får arrayen värdet av stripped med index 0 toUpper
            annars
            får arrayen värdet av stripped med index i tolower

        sedan retureras arrayen som en string.
    */
    private string TitleCase(string str)
    {
        string stripped = StrippedFromSpecialChars(str);
        char[] titleCased = new char[stripped.Length];

        for (int i = 0; i < titleCased.Length; i++)
        {
            if (i == 0)
                titleCased[i] = char.ToUpper(stripped[i]);
            else
                titleCased[i] = char.ToLower(stripped[i]);
        }
        return new string(titleCased);
    }

    /*
        Metod för att ta bort alla tecken som inte får vara i stringen.
        Dessa tecken är specifierade i _specialChars arrayen.

        först räknas antalet chars som inte är tillåtna
        och vi sätter charIndex till 0 som vi använder senare.

        med antalet otillåtna chars och längden av stringen skapar
        vi en ny char array i längden av string - chars

        nestad for loop
            deklarerar en bool som heter isSpecial sätter till false

        varje char i stringen loppas igenom arrayen av otillåtna chars
        om charen är lika med en char i arrayen så sätt isSpecial till true
        och andra loopen breakas.

        då kollas det om isSpecial är false och om det är false
        så allokeras charen till char arrayen vi skapade med index av charIndex som allokerades tidigare
        och charIndex blir 1 värde högre inför nästa pass.

        när båda looparna är klara har vi nu en array med bara tillåtna chars
        denna retureras som en string.
    */
    private string StrippedFromSpecialChars(string str)
    {
        int countOfSpecialChars = CountSpecialChars(str),
            charIndex = 0;

        char[] chars = new char[str.Length - countOfSpecialChars];

        for (int i = 0; i < str.Length; i++)
        {
            bool isSpecial = false;
            for (int j = 0; j < _specialChars.Length; j++)
            {
                if (char.Equals(str[i], _specialChars[j]))
                {
                    isSpecial = true;
                    break;
                }
            }
            if (!isSpecial)
                chars[charIndex++] = str[i];
        }

        return new string(chars);
    }

    /*
        Metod för att räkna antalet otillåtna chars i en array
        den loopar varje char i stringen och jämför den mot varje char i _specialChars arrayen
        och om det är en match så plussas count som håller räkning på hur många
        otillåtna chars som är i arrayen.
        sedan returerars count.
    */
    private int CountSpecialChars(string str)
    {
        int count = 0;
        for (int i = 0; i < str.Length; i++)
            for (int j = 0; j < _specialChars.Length; j++)
                if (char.Equals(str[i], _specialChars[j]))
                    count++;
        return count;
    }
}
