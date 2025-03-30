using Microsoft.VisualBasic;

static void Main()
{
    var dzialanie = true;
    var flaga1 = true;
    var flaga2 = true;
    var flaga3 = false;

    ConsoleKeyInfo klawisz;
    double odpowiedz1;
    int odpowiedz2;
    int odpowiedz3;
    string odpowiedz4 = "niebezpieczny";
    string odpowiedz5;
    double odpowiedz6;
    int odpowiedz7;
    int odpowiedz8;

    int wysokosc;
    int wagaWlasna;
    int glebokosc;
    int maksymalnaLadownosc;

    string numerSeryjnyTemp1;
    string numerSeryjnyTemp2;
    string numerSeryjnyTemp3;

    List<Kontenerowiec> listaStatkow = new List<Kontenerowiec>();
    var setNumerySeryjne = new HashSet<string>();
    Random generator = new Random();

    while (dzialanie)
    {
        Console.Clear();
        flaga3 = false;
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
                    flaga2 = true;
                    flaga3 = true;
                    foreach(var kontener in statek.konteneryNaPokladzie)
                    Console.WriteLine($"      - Kontener numer {kontener.numerSeryjny} o masie ladunku {kontener.masaLadunku}");
                }
                Console.WriteLine("");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Mozliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        if(flaga1) Console.WriteLine("2. Usun kontenerowiec");
        if(flaga1) Console.WriteLine("3. Dodaj kontener");
        if(flaga1 && flaga3) Console.WriteLine("4. Usun kontener");
        if(flaga3) Console.WriteLine("5. Zastap kontener");
        if(listaStatkow.Count > 1 && flaga3) Console.WriteLine("6. Przenies kontener pomiedzy statkami");
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
                Console.WriteLine("Podaj maksymalna wage wszystkich kontenerow ktore moga byc transportowane przez statek");
                odpowiedz3 = int.Parse(Console.ReadLine());

                Kontenerowiec x = new Kontenerowiec(odpowiedz1 ,odpowiedz2, odpowiedz3);
                
                listaStatkow.Add(x);

                break;
            case '2':
                Console.Clear();

                Console.WriteLine("Podaj ktory statek chcesz usunac");
                odpowiedz2 = int.Parse(Console.ReadLine());

                if(odpowiedz2 < listaStatkow.Count())
                {
                    listaStatkow.RemoveAt(odpowiedz2);
                }

                break;
            case '3':
                Console.Clear();

                Console.WriteLine("Na ktorym statku chcesz umiescic kontener?");
                odpowiedz7 = int.Parse(Console.ReadLine());

                if(listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.Count > listaStatkow.ElementAt(odpowiedz7).maksymalnaIloscKontenerow) break;

                Console.WriteLine("Kontener o jakim rozmiarze chcesz utworzyc?");
                Console.WriteLine("     1. ISO-20");
                Console.WriteLine("     2. ISO-40");
                Console.WriteLine("     3. ISO-40 High Cube");
                odpowiedz2 = int.Parse(Console.ReadLine());

                switch(odpowiedz2)
                {
                    case 1:
                        wysokosc = 255;
                        wagaWlasna = 334;
                        glebokosc = 800;
                        maksymalnaLadownosc = 4000;
                        break;
                    case 2:
                        wysokosc = 255;
                        wagaWlasna = 674;
                        glebokosc = 1200;
                        maksymalnaLadownosc = 7000;
                        break;
                    case 3:
                        wysokosc = 255;
                        wagaWlasna = 1200;
                        glebokosc = 1800;
                        maksymalnaLadownosc = 12000;
                        break;
                    default:
                        wysokosc = 255;
                        wagaWlasna = 1200;
                        glebokosc = 1800;
                        maksymalnaLadownosc = 12000;
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Kontener o jakim przeznaczeniu chcesz utworzyc?");
                Console.WriteLine("     1. Na plyny");
                Console.WriteLine("     2. Na gaz");
                Console.WriteLine("     3. Chlodniczy");
                odpowiedz3 = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if(odpowiedz3 == 1)
                {
                    Console.WriteLine("Czy rodzaj plynu jest niebezpieczny? y/n");
                    odpowiedz4 = Console.ReadLine();
                    if(odpowiedz4 == "y")
                    {
                        odpowiedz4 = "niebezpieczny";
                    }
                    else
                    {
                        odpowiedz4 = "zwykly";
                    }
                }
                else if(odpowiedz3 == 3)
                {
                    Console.WriteLine("Jaki rodzaj ladunku bedzie przechowywany w kontenerze?");
                    odpowiedz5 = Console.ReadLine();
                    Console.WriteLine("W jakiej temperaturze bedzie przechowywany ladunek?");
                    odpowiedz6 = double.Parse(Console.ReadLine());
                }
                else
                {
                    odpowiedz4 = "zwykly";
                }

                Console.WriteLine("Jaka mase ladunku chcesz umiescic w kontenerze?");
                odpowiedz1 = double.Parse(Console.ReadLine());
                Console.WriteLine();

                switch(odpowiedz3)
                {
                    case 1:
                        do
                        {
                            numerSeryjnyTemp1 = "KON-L-" + generator.Next(1, 100000);
                            
                        } while (setNumerySeryjne.Contains(numerSeryjnyTemp1));
                        setNumerySeryjne.Add(numerSeryjnyTemp1);
                        
                        Kontener y1 = new KontenerNaPlyny(0, wysokosc, wagaWlasna, glebokosc,numerSeryjnyTemp1, maksymalnaLadownosc, odpowiedz4);
                        y1.Zaladowanie(odpowiedz1);
                        listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.Add(y1);
                        break;
                    case 2:
                        do
                        {
                            numerSeryjnyTemp2 = "KON-G-" + generator.Next(1, 100000);
                            
                        } while (setNumerySeryjne.Contains(numerSeryjnyTemp2));
                        setNumerySeryjne.Add(numerSeryjnyTemp2);

                        Kontener y2 = new KontenerNaGaz(0, wysokosc, wagaWlasna, glebokosc, numerSeryjnyTemp2, maksymalnaLadownosc);
                        y2.Zaladowanie(odpowiedz1);
                        listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.Add(y2);
                        break;
                    case 3:
                        do
                        {
                            numerSeryjnyTemp3 = "KON-C-" + generator.Next(1, 100000);
                            
                        } while (setNumerySeryjne.Contains(numerSeryjnyTemp3));
                        setNumerySeryjne.Add(numerSeryjnyTemp3);

                        Kontener y3 = new KontenerNaGaz(0, wysokosc, wagaWlasna, glebokosc, numerSeryjnyTemp3, maksymalnaLadownosc);
                        y3.Zaladowanie(odpowiedz1);
                        listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.Add(y3);
                        break;
                }

            break;
        case '4':
            Console.Clear();

            Console.WriteLine("Z ktorego statku chcesz usunac kontener?");
            odpowiedz7 = int.Parse(Console.ReadLine());
            Console.WriteLine("Ktory kontener wedlug kolejnosci zaladunku chcesz usunac?");
            odpowiedz2 = int.Parse(Console.ReadLine());
            listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.ElementAt(odpowiedz7).Oproznienie();
            listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.RemoveAt(odpowiedz2);
            break;
        case '5':
            Console.Clear();

                Console.WriteLine("Na ktorym statku chcesz zamienic kontener?");
                odpowiedz7 = int.Parse(Console.ReadLine());

                if(listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.Count > listaStatkow.ElementAt(odpowiedz7).maksymalnaIloscKontenerow) break;

                Console.WriteLine("Ktory kontener wedlug kolejnosci zaladunku chcesz zastapic?");
                odpowiedz8 = int.Parse(Console.ReadLine());

                Console.WriteLine("Kontener o jakim rozmiarze chcesz utworzyc?");
                Console.WriteLine("     1. ISO-20");
                Console.WriteLine("     2. ISO-40");
                Console.WriteLine("     3. ISO-40 High Cube");
                odpowiedz2 = int.Parse(Console.ReadLine());

                switch(odpowiedz2)
                {
                    case 1:
                        wysokosc = 255;
                        wagaWlasna = 334;
                        glebokosc = 800;
                        maksymalnaLadownosc = 4000;
                        break;
                    case 2:
                        wysokosc = 255;
                        wagaWlasna = 674;
                        glebokosc = 1200;
                        maksymalnaLadownosc = 7000;
                        break;
                    case 3:
                        wysokosc = 255;
                        wagaWlasna = 1200;
                        glebokosc = 1800;
                        maksymalnaLadownosc = 12000;
                        break;
                    default:
                        wysokosc = 255;
                        wagaWlasna = 1200;
                        glebokosc = 1800;
                        maksymalnaLadownosc = 12000;
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Kontener o jakim przeznaczeniu chcesz utworzyc?");
                Console.WriteLine("     1. Na plyny");
                Console.WriteLine("     2. Na gaz");
                Console.WriteLine("     3. Chlodniczy");
                odpowiedz3 = int.Parse(Console.ReadLine());
                Console.WriteLine();
                
                if(odpowiedz3 == 1)
                {
                    Console.WriteLine("Czy rodzaj plynu jest niebezpieczny? y/n");
                    odpowiedz4 = Console.ReadLine();
                    if(odpowiedz4 == "y")
                    {
                        odpowiedz4 = "niebezpieczny";
                    }
                    else
                    {
                        odpowiedz4 = "zwykly";
                    }
                }
                else if(odpowiedz3 == 3)
                {
                    Console.WriteLine("Jaki rodzaj ladunku bedzie przechowywany w kontenerze?");
                    odpowiedz5 = Console.ReadLine();
                    Console.WriteLine("W jakiej temperaturze bedzie przechowywany ladunek?");
                    odpowiedz6 = double.Parse(Console.ReadLine());
                }
                else
                {
                    odpowiedz4 = "zwykly";
                }

                Console.WriteLine("Jaka mase ladunku chcesz umiescic w kontenerze?");
                odpowiedz1 = double.Parse(Console.ReadLine());
                Console.WriteLine();

                switch(odpowiedz3)
                {
                    case 1:
                        do
                        {
                            numerSeryjnyTemp1 = "KON-L-" + generator.Next(1, 100000);
                            
                        } while (setNumerySeryjne.Contains(numerSeryjnyTemp1));
                        setNumerySeryjne.Add(numerSeryjnyTemp1);
                        
                        Kontener y1 = new KontenerNaPlyny(0, wysokosc, wagaWlasna, glebokosc,numerSeryjnyTemp1, maksymalnaLadownosc, odpowiedz4);
                        y1.Zaladowanie(odpowiedz1);
                        listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie[odpowiedz8] = y1;
                        break;
                    case 2:
                        do
                        {
                            numerSeryjnyTemp2 = "KON-G-" + generator.Next(1, 100000);
                            
                        } while (setNumerySeryjne.Contains(numerSeryjnyTemp2));
                        setNumerySeryjne.Add(numerSeryjnyTemp2);

                        Kontener y2 = new KontenerNaGaz(0, wysokosc, wagaWlasna, glebokosc, numerSeryjnyTemp2, maksymalnaLadownosc);
                        y2.Zaladowanie(odpowiedz1);
                        listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie[odpowiedz8] = y2;
                        break;
                    case 3:
                        do
                        {
                            numerSeryjnyTemp3 = "KON-C-" + generator.Next(1, 100000);
                            
                        } while (setNumerySeryjne.Contains(numerSeryjnyTemp3));
                        setNumerySeryjne.Add(numerSeryjnyTemp3);

                        Kontener y3 = new KontenerNaGaz(0, wysokosc, wagaWlasna, glebokosc, numerSeryjnyTemp3, maksymalnaLadownosc);
                        y3.Zaladowanie(odpowiedz1);
                        listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie[odpowiedz8] = y3;
                        break;
                }
                
            break;
        case '6':
            Console.Clear();

            Console.WriteLine("Z ktorego statku chcesz przeniesc kontener?");
            odpowiedz7 = int.Parse(Console.ReadLine());
            Console.WriteLine("Ktory kontener wedlug kolejnosci zaladunku chcesz przeniesc?");
            odpowiedz2 = int.Parse(Console.ReadLine());
             Console.WriteLine("Na ktory statem chcesz przeniesc kontener?");
            odpowiedz3 = int.Parse(Console.ReadLine());

            listaStatkow.ElementAt(odpowiedz3).konteneryNaPokladzie.Add(listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.ElementAt(odpowiedz2));
            listaStatkow.ElementAt(odpowiedz7).konteneryNaPokladzie.RemoveAt(odpowiedz2);
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
                this.masaLadunku += masaLadunku;
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
                this.masaLadunku += masaLadunku;
            }
            else
            {
                throw new OverfillException($"Akcja niedozwolona. Dla tego rodzaju ladunku: {this.rodzajLadunku} mozna wypelnic kontener tylko w polowie");
            }

            if(masaLadunku + this.masaLadunku < 0.9 * this.maksymalnaLadownosc && rodzajLadunku == "zwykly")
            {
                Console.WriteLine($"Ladunek o masie {masaLadunku} zaladowany");
                this.masaLadunku += masaLadunku;
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