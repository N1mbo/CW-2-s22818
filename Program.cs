static void Main(){}

public class Kontener 
{
    public int masaLadunku; //kilogramy
    public int wysokosc; //centymetry
    public int wagaWlasna; //sam kontener, kilogramy
    public int glebokosc; //centymetry
    public string numerSeryjny; //format: KON-C-1
    public int maksymalnaLadownosc; //kilogramy

    public Kontener()
    {

    }

    void OproznienieLadunku()
    {
        Console.WriteLine("Ladunek o masie {0}, oprozniony", this.masaLadunku);

        this.masaLadunku = 0;
    }

    void ZaladowanieLadunku(int masaLadunku)
    {
        try 
        {
            if(masaLadunku < this.maksymalnaLadownosc)
            {
                Console.WriteLine("Ladunek o masie " + masaLadunku + ", zaladowany");
            }
            else
            {
                throw new OverfillException("Ladunek o masie " + masaLadunku + ", jest za ciezki");
            }        
        }
        catch (OverfillException e) 
        {
            Console.WriteLine(e);
        } 
    }
}

class OverfillException(string message) : Exception(message);