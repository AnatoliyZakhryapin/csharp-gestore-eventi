namespace GestoreEventi
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Evento evento = CreaEvento();

            PrenotaPosti(evento);

            Console.WriteLine();
            VisualizzaPostiPrenotati(evento);
            VisualizzaPostiDisponibili(evento);

            StartMenuDisdirePosti(evento);
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
        static void PrenotaPosti(Evento evento)
        {
            Console.Write("Quanti posti desideri prenotare? ");

            int postiDaPrenotare = Convert.ToInt32(Console.ReadLine());

            evento.PrenotaPosti(postiDaPrenotare);
        }

        static void VisualizzaPostiPrenotati(Evento evento)
        {
            Console.WriteLine($"Numero di posti prenotati - {evento.PostiPrenotati}");
        }
        static void VisualizzaPostiDisponibili(Evento evento)
        {
            Console.WriteLine($"Numero di posti disponibili - {evento.PostiDisponibili()}");
        }

        static void DisdirePosti(Evento evento)
        {
            Console.Write("Indica il numero di posti da disdire: ");

            int postiDaDisdire = Convert.ToInt32(Console.ReadLine());

            evento.DiscdiciPosti(postiDaDisdire);
        }

        static void StartMenuDisdirePosti(Evento evento)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Vuoi disdire dei posti (si/no)? ");
                bool vuoiDisdire = Console.ReadLine() == "si" ? true : false;

                switch (vuoiDisdire)
                {
                    case true:
                        DisdirePosti(evento);

                        Console.WriteLine();
                        VisualizzaPostiPrenotati(evento);
                        VisualizzaPostiDisponibili(evento);
                        break;
                    case false:
                        Console.WriteLine("Ok va bene!");

                        Console.WriteLine();
                        VisualizzaPostiPrenotati(evento);
                        VisualizzaPostiDisponibili(evento);
                        break;
                }

                if (vuoiDisdire == false)
                    return;
            }
        }
    }

  
}
