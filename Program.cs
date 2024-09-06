using WeatherStation;

// Initalisering av alla klasser som används i detta program
BubbelSortAlgorithm bubbel = new();
BinarySearchAlgorithm binary = new();
SortAlgorithms sort = new();
SearchAlgorithms search = new();
UserInput input = new();

/*
    Lista av Städer initialiseras som tom. använder lista eftersom vi ska lägga till städer i listan och då är en lista
    absolut lättast att använda.

    string stad sätts till en tom string för att kunna användas som nyckel i do while loopen.
    demo är för min avslutnings överraskning.

    int temp, linear, binary, index för att få data från alla metoder och använda dom för att skriva ut till
    användaren och för att köra if statments.
*/
List<Stad> städer = [];
string stad = string.Empty,
    demo;
int temp,
    linearSearch,
    binarySearch,
    index;

/*
    Do while loop som frågar efter en stad och dess temperatur och sedan lägger till den staden i listan
    över städer.
    input från användaren tas med metoder skriva i UserInput klassen.
    om användaren skriver klar så avbryts loopen och programmet fortsätter.
*/
do {
    Console.Clear();
    Console.WriteLine(
        "Väder Rapport\n"
            + "Vänligen lägg till en stad och dess temperatur.\n"
            + "Skriv Klar när du har laggt till alla städer du vill."
    );

    stad = input.GetString("Skriv namn på staden du vill lägga till.\n-> ");
    if (stad.Equals("Klar"))
        continue;

    temp = input.GetInt(-60, 60, $"Vilken temperatur är det i {stad}? min -60 max 60c\n-> ");

    städer.Add(new Stad(stad, temp));
} while (!stad.Equals("Klar"));
Console.Clear();

// LinearSearch prompt
Console.WriteLine("Sök efter en stad med hjälp av LinearSearch.");

/*
    använder linearSearch och om index är -1 skrivs det att ingen stad hittades
    annars skrivs staden ut som hittades med temperaturen.
*/
linearSearch = input.GetInt(-60, 60, "Ange en temperatur att söka efter\n-> ");
index = search.Linsok(städer, städer.Count, linearSearch);

Console.WriteLine();
if (index == -1)
    Console.WriteLine("Det fanns ingen stad med den temperaturen.");
else
    Console.WriteLine(städer[index].ToString());

// Sorterar Listan med BubbelSort.
sort.BubbleSort(städer);

// Skriver ut lägsta och högsta temperaturerna i listan med metoder i SearchAlgorithms
Console.WriteLine();
Console.WriteLine($"Stad med lägst temperatur. {search.LägstTemp(städer)}");
Console.WriteLine($"Stad med högst temperatur. {search.HögstTemp(städer)}");
Console.WriteLine();

/*
    använder BinarySearch och om index är -1 skrivs det att ingen stad hittades
    annars skrivs staden ut som hittades med temperaturen.
*/
Console.WriteLine("Sök efter en stad med hjälp av BinarySearch.");

binarySearch = input.GetInt(-60, 60, "Ange en temperatur att söka efter\n-> ");
index = search.BinarySearch(städer, 0, städer.Count, binarySearch);

Console.WriteLine();
if (index == -1)
    Console.WriteLine("Det fanns ingen stad med den temperaturen.");
else
    Console.WriteLine(städer[index].ToString());

Console.WriteLine();

// Avsluts meddelande
Console.WriteLine("Detta avslutar den ordinarie uppgriften men jag har lite extra att bjuda på.");
Console.WriteLine("Tryck på en tangent för att gå vidare.");
Console.ReadKey();
Console.Clear();

/*
    En do while för att visa en meny för mina visualliseringar av hur bubbel och binary algorytmerna fungerar.
    dessa agerar på egen data inuti sina egna klasser.
*/
do {
    Console.WriteLine("Jag har visualiserat BubbelSort och BinarySearch Algorytmerna.");
    demo = input.GetString(
        "[Bubbel] för att se hur BubbelSort fungerar.\n"
            + "[Binary] för att se hur BinarySearch fungerar.\n"
            + "[Quit] för att avsluta programmet.\n-> "
    );

    if (demo.Equals("Bubbel"))
        bubbel.Sort();
    else if (demo.Equals("Binary"))
        binary.Search();
} while (!demo.Equals("Quit"));

Console.WriteLine("Tack för att du har användt mitt program.");
