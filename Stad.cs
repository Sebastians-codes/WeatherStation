namespace WeatherStation;

/*
    Klass för att hålla data om en stad så som namn och vilken temperatur det är i staden.
    public för att kunna använda dessa properties i andra metoder
    och en override av ToString för att presentera propertiesen i en formaterad string.
*/
public class Stad(string namn, int temp)
{
    public string Namn = namn;
    public int Temp = temp;

    public override string ToString() => $"Stad: {Namn}, Temperatur: {Temp}C.";
}
