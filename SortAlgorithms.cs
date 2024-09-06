using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStation {
    public class SortAlgorithms {
        /*
            En halvt optimerad BubbelSort algorytm
            för varje element i listan jämförs nästa element efter om det är mindre
            och om det är mindre byter dom plats och nästa index jämförs med det framför det.

            detta är varför det är två for loops den första för att köra loopen lika många gånger
            som det finns element i listan.
            den andra för att jämföra varje index mot det framför om det är mindre
            byts dessa mot varandra

            om inga element byter plats så avslutas loopen.
            för om inga elemnt har bytt plats i den andra loopen är listan sorterad.

            Pseudokod
            
            för varje stad i städer
                för varje stads temp i städer
                    om temp är mer än temp i nästa stad
                        byt plats på städerna
        */
        public void BubbleSort(List<Stad> städer) {
            bool swapped;
            for (int i = 0; i < städer.Count; i++) {
                swapped = false;
                for (int j = 0; j < städer.Count - 1; j++) {
                    if (städer[j].Temp > städer[j + 1].Temp) {
                        (städer[j], städer[j + 1]) = (städer[j + 1], städer[j]);
                        swapped = true;
                    }
                }
                if (!swapped)
                    break;
            }
        }

        /*
            QuickSort med bara recursion lämnar denna gömd som ett litet easter egg.
        */
        public void QuickSort(List<Stad> städer, int left, int right) {
            if (left >= right)
                return;

            int pivot = Partition(städer, left, right, left, right);

            QuickSort(städer, left, pivot - 1);
            QuickSort(städer, pivot + 1, right);
        }

        private int Partition(List<Stad> städer, int left, int right, int leftIndex, int rightIndex) {
            if (leftIndex > rightIndex) {
                Swap(städer, left, rightIndex);
                return rightIndex;
            }

            if (städer[leftIndex].Temp <= städer[left].Temp)
                return Partition(städer, left, right, leftIndex + 1, rightIndex);

            Swap(städer, leftIndex, rightIndex);
            return Partition(städer, left, right, leftIndex, rightIndex - 1);
        }

        private void Swap(List<Stad> städer, int left, int right) =>
            (städer[left], städer[right]) = (städer[right], städer[left]);
    }
}
