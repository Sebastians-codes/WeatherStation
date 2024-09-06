using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation {
    // En visuel representation på hur BinarySearch algorytmen fungerar.
    public class BinarySearchAlgorithm {
        /*
            _random för att generara slumpade nummer.

            _ints en array som inte initialiserad för att den görs det av användaren.
        */
        private Random _random;
        private int[] _ints;

        // Constructor som initaliserar ett Random object.
        public BinarySearchAlgorithm() {
            _random = new();
        }

        // Metod som kör och visar hur BinarySearch algorytmen fungerar.
        public void Search() {
            // init sammlings metod för flera som behöver köras innan search kan fungera som den ska.
            Init();
            Console.Clear();
            // FindIndex metod för att ta input från användaren.
            /*
                left är första indexen i arrayen och right är sista indexet i arrayen.
                pivot deklarerars men initialiseras inte för att den görs det i varje
                itteration av loopen.
            */
            int find = FindIndex(),
                left = 0,
                right = _ints.Length - 1,
                pivot;

            /*
                Lopen körs tills att siffran är hittad eller left är mer än right vilket
                betyder att hela arrayen har sökts igenom.
            */
            while (left <= right) {
                Console.Clear();
                Console.WriteLine($"Finding index of number {find}.");
                Console.WriteLine(
                    "\nGreen numbers are the bounds of where the algorithm is working within."
                        + "\nBlue is the number we are searching for."
                        + "\nRed is the number the algorithm is comparing with.\n"
                );
                Console.WriteLine($"While left {left} is less then or equal too right {right}.");
                // pivot är mitten av arrayen avrundat neråt till närmsta.
                // Med left + (right - left) / 2 kan man undvika integer overflow men det kan inte hända i denna metod.
                pivot = (left + right) / 2;
                Console.WriteLine(
                    $"Pivot is = {left} + {right} devided by two rounded down.\nResult {pivot}.\n"
                );
                // Skriver ut hela Arrayen som söks igenom och märker intresse punkter med färg.
                PrintCurrentArray(_ints, pivot, find, left, right);
                // Om elemntet vid _ints index pivot är lika med find är siffran hittad och loopen avslutas.
                if (_ints[pivot] == find) {
                    Console.WriteLine($"Index of the number {find} is {pivot}.");
                    break;
                }
                // Om elemtet är mindre än siffran görs nästa sökning med start "left" från pivot + 1.
                else if (_ints[pivot] < find) {
                    Console.WriteLine(
                        $"Element at index {pivot} = \"{_ints[pivot]}\" is less then {find}."
                            + $"\nLeft = {pivot} + 1."
                    );
                    left = pivot + 1;
                }
                // Om elemntet är mer än siffran görs nästa sökning med slut "right" från pivot - 1.
                else {
                    Console.WriteLine(
                        $"Element at index {pivot} = \"{_ints[pivot]}\" is greater then {find}."
                            + $"\nRight = {pivot} - 1."
                    );
                    right = pivot - 1;
                }
                Console.Write("Press any key to get to next itteration.");
                Console.ReadKey();
            }
            Console.Write("Press any key to get to get back to mainmenu.");
            Console.ReadKey();
        }

        /*
            Metod som färgsätter de element som är av intresse för algorytmen samt den siffran vi letar efter.
            Detta ger en klar bild av hur algorytmen hela tiden halverar sök ytan och till slut hittar siffran.
        */
        private void PrintCurrentArray(int[] ints, int pivot, int find, int left, int right) {
            for (int i = 0; i < ints.Length; i++) {
                if (i == pivot)
                    PrintColorTextRedBlueOrGreen("red", _ints[i].ToString());
                else if (_ints[i] == _ints[Array.IndexOf(_ints, find)])
                    PrintColorTextRedBlueOrGreen("blue", _ints[i].ToString());
                else if (i == left || i == right)
                    PrintColorTextRedBlueOrGreen("green", _ints[i].ToString());
                else
                    Console.Write(_ints[i]);
                Console.Write(", ");
            }
            Console.WriteLine("\n");
        }

        // Skapar arrayen av ints allokerar den med slumpade siffror och sorterar arrayen.
        private void Init() {
            CreateSizeOfArray();
            AllocateIntegerArray();
            BubbelSort(_ints.Length);
        }

        /*
            Metod för att ta en siffra från användaren och kolla att den finss i arrayen om den inte
            gör det så körs metoden igen med hjälp av recursion.
            Hade velat göra checken med _ints.Contains istället för en egen loop men
            ville inte använda inbyggda metoder.
        */
        private int FindIndex() {
            PrintElements();
            Console.Write("Enter the number u want to find the index of.\n-> ");
            if (int.TryParse(Console.ReadLine(), out int number) && IsInArray(number))
                return number;
            Console.Clear();
            Console.WriteLine("You must enter a value in the array.");
            return FindIndex();
        }

        // Metod som kollar om ett nummer finns i arrayen.
        private bool IsInArray(int number) {
            for (int i = 0; i < _ints.Length; i++)
                if (_ints[i] == number)
                    return true;
            return false;
        }

        // Metod som skiver ut alla element i arrayen.
        private void PrintElements() {
            foreach (int num in _ints)
                Console.Write($"{num}, ");
            Console.WriteLine();
        }

        // BubbelSort för att sortera arrayen men så minimal som möjligt för att det behövs ej mer för detta.
        private void BubbelSort(int length) {
            for (int i = 0; i < length; i++)
                for (int j = 0; j < length - 1; j++)
                    if (_ints[j] > _ints[j + 1])
                        (_ints[j], _ints[j + 1]) = (_ints[j + 1], _ints[j]);
        }

        // Metod för att allokera varje index i arrayen med en unik slumpad siffra mellan 0 och 9999
        private void AllocateIntegerArray() {
            for (int i = 0; i < _ints.Length; i++) {
                int randomNumber = _random.Next(0, 10000);
                if (!_ints.Contains(randomNumber))
                    _ints[i] = randomNumber;
                else
                    i--;
            }
        }

        // Metod för att ta input och bestämma hur stor arrayen ska vara som vi ska söka i.
        private void CreateSizeOfArray() {
            Console.Clear();
            Console.Write(
                "What size of a array do you want to test?"
                    + "\nMaximum size of the array is 750 numbers and minimum 5."
                    + "\n-> "
            );
            if (int.TryParse(Console.ReadLine(), out int length) && length > 4 && length < 751)
                _ints = new int[length];
            else {
                Console.Clear();
                Console.WriteLine("Try again.");
                CreateSizeOfArray();
            }
        }

        // Metod för att skiva ut text i färg och sedan ändra tillbaka den till standard.
        private void PrintColorTextRedBlueOrGreen(string color, string text) {
            if (color.ToLower().Equals("red"))
                Console.ForegroundColor = ConsoleColor.Red;

            if (color.ToLower().Equals("blue"))
                Console.ForegroundColor = ConsoleColor.DarkBlue;

            if (color.ToLower().Equals("green"))
                Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.Write(text);

            Console.ResetColor();
        }
    }
}
