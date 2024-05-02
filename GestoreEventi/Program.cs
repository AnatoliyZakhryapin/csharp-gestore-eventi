namespace GestoreEventi
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Evento evento = CreaEvento();

            //PrenotaPosti(evento);

            //Console.WriteLine();
            //VisualizzaPostiPrenotati(evento);
            //VisualizzaPostiDisponibili(evento);

            //StartMenuDisdirePosti(evento);

            StartMenuCreaNuovaProgrammaEventi();
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

        static ProgrammaEventi CreaNuovaProgrammaEventi()
        {
            Console.Write("Indica il nome del tuo programma Eventi: ");

            string titoloProgrammaEventi = Console.ReadLine();

            return new ProgrammaEventi(titoloProgrammaEventi);
        }

        static void StartMenuCreaNuovaProgrammaEventi()
        {
            ProgrammaEventi newProgrammaEventi = CreaNuovaProgrammaEventi();

            Console.Write("Indica il numero di eventi da inserire: ");

            int eventiDaCreare = Convert.ToInt32(Console.ReadLine());

            int count = 1;

            while (eventiDaCreare > 0) 
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine($"Evento {count}");
                    Evento nuovoEvento = CreaEvento();
                    newProgrammaEventi.AggiungiEventoAllaLista(nuovoEvento);
                    eventiDaCreare--;
                    count++;
                }
                catch (Exception e) 
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    continue;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Il numero di eventi nel programma è: {newProgrammaEventi.NumeroEventi()}");

            Console.WriteLine();
            Console.WriteLine("Ecco il tuo programma eventi:");
            Console.WriteLine($"{newProgrammaEventi.CreaStringaDiProgrammaEventi()}");

            StampaEventiInData(newProgrammaEventi);

            EliminaTuttiEventi(newProgrammaEventi);
        }

        public static void StampaEventiInData(ProgrammaEventi programmaEventi)
        {
            Console.WriteLine();
            Console.Write("Inserisci una data per sapere che eventi ci saranno (gg/mm/yyyy): ");

            string inputDataEvento = Console.ReadLine();
            string formatoData = "dd/MM/yyyy";

            DateTime dataEvento = DateTime.ParseExact(inputDataEvento, formatoData, null);

            List<Evento> eventiInData = programmaEventi.EventiInData(dataEvento);

            if (eventiInData.Count > 0 )
            {
                ProgrammaEventi.StampaEventi(eventiInData);
            }
            else 
            { 
                Console.WriteLine();    
                Console.WriteLine("Non ci sono eventi per data selezionata!");
            }
        }

        public static void EliminaTuttiEventi(ProgrammaEventi newProgrammaEventi)
        {
            Console.WriteLine();
            Console.Write("Vuoi eliminare tutti eventi (si/no)? ");
            bool vuoiCancellare = Console.ReadLine() == "si" ? true : false;

            switch (vuoiCancellare)
            {
                case true:
                    newProgrammaEventi.SvuotaListaEventi();

                    Console.WriteLine("La lista di Eventi e svuotata con successo!");

                    break;
                case false:
                    Console.WriteLine("Ok va bene!");;
                    break;
            }
        }
    }

  
}
