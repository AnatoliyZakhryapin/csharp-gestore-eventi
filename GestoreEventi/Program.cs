namespace GestoreEventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            CreaEvento();
        }

        static Evento CreaEvento()
        {
            Console.WriteLine();
            Console.Write("Inserisci il nome dell'evento: ");

            string titoloEvento = Console.ReadLine();


            Console.Write("Inserisci la data dell'evento (gg/mm/yyyy): ");

            string inputDataEvento = Console.ReadLine();
            string formatoData = "dd/MM/yyyy";

            DateTime dataEvento = DateTime.ParseExact(inputDataEvento, formatoData, null);

            Console.Write("Inserisci il numero di posti totali: ");

            int postiTotali = Convert.ToInt32(Console.ReadLine());

            Evento nuovoEvento = new Evento(titoloEvento, dataEvento, postiTotali);

            return nuovoEvento;
        }
    }

  
}
