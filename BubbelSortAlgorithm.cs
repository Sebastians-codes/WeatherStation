using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation
{
    // En visuel representation på hur BubbelSort algorytmen fungerar.
    public class BubbelSortAlgorithm
    {
        /*
            random för att generera sluppade siffror för _ints arrayen.
            initialiseringen av Random klassen och allokering av arrayen sker i Constructorn.

            devider är en string constructor som skapar en linje i länged av arrayen gånger 7 för att matcha
            med paddingen som jag använder för att få det att få arrayerna att vara rakt ovanför varandra även
            fast det är olika värden som tar olika mycket plats på skärmen.

        */
        private Random _random;
        private int[] _ints = new int[8];
        private string _devider = new('-', 8 * 7);
        private bool _auto;

        public BubbelSortAlgorithm()
        {
            _random = new();
            AllocateIntegerArray();
        }

        public void Sort()
        {
            /*
                lengthOfArray är längden av ints arrayen.

                sleepTimer är för att styra mellan auto läge och manuellt läge för att itterara genom algorytmen.

                boolen swapped är för att avsluta metoden om arrayen är sorterad redan.
                Om swapped är false betyder det att algorytmen har gått igenom hela listan från första till
                sista index utan att ha bytt plats på något och i och med det så är listan sorterad.
            */
            int lengthOfArray = _ints.Length,
                sleepTimer;
            bool swapped;

            // metod för att sätta värdet på variabeln _auto som avgör läget metoden körs i.
            AutoOrManualItteration();

            // _auto = true så sätts delayen på auto till 1.5sek. annars 0.
            if (_auto)
                sleepTimer = 1500;
            else
                sleepTimer = 0;

            /*
                Bubbelsort ittererar genom en lista lika många gånger som det finns värden.
                detta gör att om listan är 13 värden lång så kommer den att kolla igenom hela listan 13 gånger
                om man då inte har en check av kollar om något ändrades i itteration som avslutar loopen.
            */
            for (int i = 0; i < lengthOfArray; i++)
            {
                //varje ny sekvens av loopen så sätts swapped till false för att kolla om arrayen är sorterad.
                swapped = false;
                for (int j = 0; j < lengthOfArray - 1; j++)
                {
                    // Thread.Sleep som alltid körs men delayen ändras av boolen _auto som sätter sleepTimer.
                    Thread.Sleep(sleepTimer);
                    // Två metoder som skriver ut den aktuella positionen av algoritmen i arrayen och värdena i arrayen.
                    ShowIndexOfSearch(lengthOfArray, j, i);
                    PrintValuesInList(_ints, lengthOfArray);
                    // om värden i arrayen på index j är mer än värdet på index j + 1.
                    if (_ints[j] > _ints[j + 1])
                    {
                        // metod som skriver ut vilka värden som kommer byta plats.
                        PrintGreater(j);

                        // byter värdet på index j till värdet på index j + 1 och tvärtom genom en value tuple
                        (_ints[j], _ints[j + 1]) = (_ints[j + 1], _ints[j]);

                        // sätter swapped till true så algorytmen fortsätter.
                        swapped = true;

                        // Skriver ut arrayen efter bytet skett mellan de två indexerna.
                        PrintValuesInList(_ints, lengthOfArray);
                    }
                    else
                        // Om värdet på index j är mindre eller lika med värdet på index j + 1
                        PrintLesser(j);

                    // Om _auto är false så navigeras programmet manuellt.
                    Console.Write($">{_devider}>\n");
                    if (!_auto)
                    {
                        Console.Write("\nPress any key to get to next itteration.\n");
                        Console.ReadKey();
                    }
                }
                // Om swapped är false betyder det att inget byte skett och listan är sorterad och programmet avslutas.
                if (!swapped)
                {
                    Console.WriteLine("\nList is sorted!");
                    break;
                }
            }
        }

        /*
            Metod för att be användaren att välja vilket läga att köra programmet i manuellt
            eller auto.
            Console.ReadLine och ToLower för att formattera inputen.
            Om värdet inte är m eller a så körs metoden igen med recursion.
        */
        private void AutoOrManualItteration()
        {
            Console.Clear();
            Console.WriteLine("Welcome to my showcase of how the bubbel sort algorithm works.");
            Console.Write("Do you want to run it in manual or auto mode?\nm/a? -> ");
            string choice = Console.ReadLine().ToLower();
            if (choice.Equals("m"))
                _auto = false;
            else if (choice.Equals("a"))
                _auto = true;
            else
                AutoOrManualItteration();
        }

        /*
            Metod som formatterar en text med vissa tecken i röd eller blå färg
            om ett byte av två kommer ske i algorytmen.
        */
        private void PrintGreater(int j)
        {
            Console.Write("Value at index ");
            PrintColorTextRedOrBlue("red", j.ToString());
            Console.Write(" is greater then the value at index ");
            PrintColorTextRedOrBlue("blue", (j + 1).ToString());
            Console.Write(".\nSwapping index of values ");
            PrintColorTextRedOrBlue("red", (_ints[j]).ToString());
            Console.Write(" and ");
            PrintColorTextRedOrBlue("blue", (_ints[j + 1]).ToString());
            Console.Write(".");
        }

        /*
            Metod som formatterar en text med vissa tecken i röd eller blå färg
            om ett byte inte hände i algorytmen.
        */
        private void PrintLesser(int j)
        {
            Console.Write("Value at index ");
            PrintColorTextRedOrBlue("red", j.ToString());
            Console.Write(" is not greater then the value at index ");
            PrintColorTextRedOrBlue("blue", (j + 1).ToString());
            Console.Write(".\n");
        }

        // Metod som formatterar och skriver ut alla värden i arryen på ett snyggt sätt.
        private void PrintValuesInList(int[] ints, int length)
        {
            Console.WriteLine();
            for (int i = 0; i < length; i++)
                Console.Write($" [{ints[i]}] ".PadRight(7));
            Console.WriteLine();
        }

        /*
            Metoden som visar vilken index som algorytmen är på och vilka index som jämförs med varandra.
            En char array skapas i längden av arrayen som används i bubbelsort.
            med hjälp av index från Sort metoden så visas v som en pil på det indexer som jämförs.
            även färgen sätts med hjälp av index om i är lika med index är färgen röd annars om den
            är index + 1 så är den blå annars får texten den vanliga färgen.
            PadRight används för att få rätt avstånd mellan array elementen i formatering.
        */
        private void ShowIndexOfSearch(int length, int index, int sequence)
        {
            int i = 0;
            char[] positionInList = new char[length];

            Console.Clear();

            Console.Write(
                $"\nSequence -> {sequence + 1} itteration -> {index + 1}.\nComparing index "
            );
            PrintColorTextRedOrBlue("red", index.ToString());
            Console.Write(" and ");
            PrintColorTextRedOrBlue("blue", (index + 1).ToString());
            Console.Write($"\n\n>{_devider}>\n");

            for (i = 0; i < length; i++)
            {
                if (i == index || i == index + 1)
                {
                    Console.Write(" [");

                    if (i == index)
                        PrintColorTextRedOrBlue("red", "v");
                    else
                        PrintColorTextRedOrBlue("blue", "v");
                    Console.Write("] ".PadRight(4));
                }
                else
                    Console.Write($" [{i}] ".PadRight(7));
            }
        }

        /*
            Metod för att skriva ut en färgad text till consolen.
            genom sin parameter color så tar den antigen stringen red eller blue
            detta sätter färgen som kommer användas vid utskrift och text sätter var som skrivs.
            efter utskriften har skett så återställs färgen till det normala.
        */
        private void PrintColorTextRedOrBlue(string color, string text)
        {
            if (color.ToLower().Equals("red"))
                Console.ForegroundColor = ConsoleColor.Red;

            if (color.ToLower().Equals("blue"))
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write(text);

            Console.ResetColor();
        }

        // Metod för att allokera _ints arrayen med slumpade nummer mellan 0 till 99.
        private void AllocateIntegerArray()
        {
            for (int i = 0; i < _ints.Length; i++)
                _ints[i] = _random.Next(0, 100);
        }
    }
}
