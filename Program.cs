using Microsoft.VisualBasic;

static void Main()
{
    var dzialanie = true;
    var flaga1 = true;
    var flaga2 = true;
    var flaga3 = true;

    ConsoleKeyInfo klawisz;
    double odpowiedz1;
    int odpowiedz2;
    int odpowiedz3;

    List<Kontenerowiec> listaStatkow = new List<Kontenerowiec>();

    while (dzialanie)
    {
        Console.Clear();
        Console.WriteLine("Lista kontenerowcow:");
        Console.WriteLine("");
        if(listaStatkow.Count == 0)
        {
            Console.WriteLine("Brak");
            flaga1 = false;
        } 
        else
        {
            flaga1 = true;

            foreach(var statek in listaStatkow)
            {
                Console.WriteLine($"Statek numer {listaStatkow.IndexOf(statek)} ( speed = {statek.maksymalnaPredkosc}, maxContainerNum = {statek.maksymalnaIloscKontenerow}, maxWeight = {statek.maksymalnaWagaKontenerow})");
                Console.WriteLine("     Lista kontenerow na pokladzie:");

                if(statek.konteneryNaPokladzie.Count == 0)
                {
                    Console.WriteLine("     Brak");
                    flaga2 = false;
                }
                else
                {
                    foreach(var kontener in statek.konteneryNaPokladzie)
                    Console.WriteLine($"     Kontener numer {kontener.numerSeryjny} o masie ladunku {kontener.masaLadunku}");
                }
                Console.WriteLine("");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Mozliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        if(flaga1) Console.WriteLine("2. Usun kontenerowiec");
        if(flaga1) Console.WriteLine("3. Dodaj kontener");
        if(flaga1 && flaga2) Console.WriteLine("4. Usun kontener");
        Console.WriteLine("0. Wyjdz");

        klawisz = Console.ReadKey();

        switch(klawisz.KeyChar)
        {
            case '0': 
                dzialanie = false;
                break;
            case '1': 
                Console.Clear();

                Console.WriteLine("Podaj maksymalna predkosc statku");
                odpowiedz1 = double.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Podaj maksymalna ilosc kontenerow jaka statek moze przewozic");
                odpowiedz2 = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Podaj maksymalna wage wszystkich kontenerow kotre moga byc transportowane przez statek");
                odpowiedz3 = int.Parse(Console.ReadLine());

                Kontenerowiec x = new Kontenerowiec(odpowiedz1 ,odpowiedz2, odpowiedz3);
                
                listaStatkow.Add(x);

                break;
            case '2':
                Console.Clear();

                Console.WriteLine("Podaj podaj ktory statek chcesz usunac");
                odpowiedz2 = int.Parse(Console.ReadLine());

                if(odpowiedz2 < listaStatkow.Count())
                {
                    listaStatkow.RemoveAt(odpowiedz2);
                }

                break;
            case '3':

            break;
        }
    }
}

Main();

var setKontenerow = new HashSet<int>(); 
string[] rodzajLadunku = ["niebezpieczny", "zwykly"];

interface Ladunek
{
    void Oproznienie();
    void Zaladowanie(double masaLadunku);
}

interface IHazardNotifier
{
    void NiebezpieczneZajscie(string zajscie);
}

abstract class Kontener(double masaLadunku, int wysokosc, int wagaWlasna, int glebokosc, string numerSeryjny, int maksymalnaLadownosc) : Ladunek 
{
    public double masaLadunku { get; set; } = masaLadunku; //kilogramy
    public int wysokosc { get; set; } = wysokosc; //centymetry
    public int wagaWlasna { get; set; } = wagaWlasna; //sam kontener, kilogramy
    public int glebokosc { get; set; } = glebokosc; //centymetry
    public string numerSeryjny { get; set; } = numerSeryjny; //format: KON-C-1
    public int maksymalnaLadownosc { get; set; } = maksymalnaLadownosc; //kilogramy

    public virtual void Oproznienie()
    {
        Console.WriteLine("Ladunek o masie " + this.masaLadunku + " oprozniony");

        this.masaLadunku = 0;
    }

    public virtual void Zaladowanie(double masaLadunku)
    {
        try 
        {
            if(masaLadunku + this.masaLadunku < this.maksymalnaLadownosc)
            {
                Console.WriteLine($"Ladunek o masie {masaLadunku} zaladowany");
            }
            else
            {
                throw new OverfillException($"Ladunek o masie {masaLadunku}, jest za ciezki. Maksymalnie {this.maksymalnaLadownosc}");
            }        
        }
        catch (OverfillException e) 
        {
            Console.WriteLine(e);
        } 
    }
}

class OverfillException(string message) : Exception(message);

class KontenerNaPlyny(double masaLadunku, int wysokosc, int wagaWlasna, int glebokosc, string numerSeryjny, int maksymalnaLadownosc, string rodzajLadunku) : Kontener(masaLadunku, wysokosc, wagaWlasna, glebokosc, numerSeryjny, maksymalnaLadownosc), IHazardNotifier
{
    public string rodzajLadunku { get; set; } = rodzajLadunku;
    public void NiebezpieczneZajscie(string zajscie)
    {
        Console.WriteLine($"Zaszla niebiezpieczna sytuacja typu: {zajscie} w kontenerze o numerze {this.numerSeryjny}");
    }

    public override void Zaladowanie(double masaLadunku)
    {
        try 
        {
            if(masaLadunku + this.masaLadunku < 0.5 * this.maksymalnaLadownosc && rodzajLadunku == "niebezpieczny")
            {
                Console.WriteLine($"Ladunek o masie {masaLadunku} zaladowany");
            }
            else
            {
                throw new OverfillException($"Akcja niedozwolona. Dla tego rodzaju ladunku: {this.rodzajLadunku} mozna wypelnic kontener tylko w polowie");
            }

            if(masaLadunku + this.masaLadunku < 0.9 * this.maksymalnaLadownosc && rodzajLadunku == "zwykly")
            {
                Console.WriteLine($"Ladunek o masie {masaLadunku} zaladowany");
            }    
            else
            {
                throw new OverfillException($"Akcja niedozwolona. Dla tego rodzaju ladunku: {this.rodzajLadunku} mozna wypelnic kontener tylko w 90%");
            }   
        }
        catch (OverfillException e) 
        {
            Console.WriteLine(e);
        }
    }
}

class KontenerNaGaz(double masaLadunku, int wysokosc, int wagaWlasna, int glebokosc, string numerSeryjny, int maksymalnaLadownosc) : Kontener(masaLadunku, wysokosc, wagaWlasna, glebokosc, numerSeryjny, maksymalnaLadownosc), IHazardNotifier
{
    public override void Oproznienie()
    {
        Console.WriteLine("Oprozniono ladunek o masie " + 0.95 * this.masaLadunku + ". Pozostale 5% ladunku pozostalo wewnatrz");

        this.masaLadunku = 0.05 * this.masaLadunku;
    }

    public void NiebezpieczneZajscie(string zajscie)
    {
        Console.WriteLine($"Zaszla niebiezpieczna sytuacja typu: {zajscie} w kontenerze o numerze {this.numerSeryjny}");
    }
}

class KontenerChlodniczy(double masaLadunku, int wysokosc, int wagaWlasna, int glebokosc, string numerSeryjny, int maksymalnaLadownosc, double temperatura, string rodzajProduktu) : Kontener(masaLadunku, wysokosc, wagaWlasna, glebokosc, numerSeryjny, maksymalnaLadownosc)
{
    public double temperatura { get; set; } = temperatura;
    public string rodzajProduktu { get; set; } = rodzajProduktu;
}

class Kontenerowiec(double maksymalnaPredkosc, int maksymalnaIloscKontenerow, int maksymalnaWagaKontenerow)
{
    public List<Kontener> konteneryNaPokladzie = new List<Kontener>();
    public double maksymalnaPredkosc { get; set; } = maksymalnaPredkosc; //wezly
    public int maksymalnaIloscKontenerow { get; set; } = maksymalnaIloscKontenerow;
    public int maksymalnaWagaKontenerow { get; set; } = maksymalnaWagaKontenerow; //tony
}