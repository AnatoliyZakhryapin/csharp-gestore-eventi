using System.Globalization;

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

            StartMenuCreaNuovaProgrammaEventi();
        }

        static Evento CreaEvento()
        {
            string titoloEvento = "";

            while (string.IsNullOrWhiteSpace(titoloEvento))
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Inserisci il nome dell'evento: ");
                    titoloEvento = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(titoloEvento))
                        throw new Exception("Il titolo non può essere vuoto.");
                }
                catch (Exception e)
                {
                    Console.WriteLine();    
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }

            DateTime dataEvento = DateTime.MinValue;
            bool dataNonCorretta = true;

            while (dataNonCorretta)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Inserisci la data dell'evento (gg/mm/yyyy): ");

                    string inputDataEvento = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(inputDataEvento))
                        throw new Exception("Data non può essere vuota.");

                    string formatoData = "dd/MM/yyyy";

                    dataEvento = DateTime.ParseExact(inputDataEvento, formatoData, null);

                    if (DateTime.TryParseExact(inputDataEvento, formatoData, null, DateTimeStyles.None, out DateTime result))
                    {
                        dataEvento = result;

                        DateTime dataAttuale = DateTime.Now;
                        if (dataEvento == DateTime.MinValue)
                            throw new Exception("La data non può essere nulla.");

                        if (dataAttuale > dataEvento)
                            throw new Exception("La data dell'evento non può essere nel passato.");

                        dataNonCorretta = false;
                    }
                    else
                    {
                        throw new Exception("Formato data non valido.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }

            int postiTotali = 0;
            while (postiTotali <= 0)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Inserisci il numero di posti totali: ");
                    if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    {
                        postiTotali = result;
                    }
                    else
                    {
                        throw new Exception("La capienza massima deve esse maggiore di zero.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }

            string rispostaConferenza = "";

            while (rispostaConferenza.ToLower() != "si" && rispostaConferenza.ToLower() != "no")
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Questo evento è una conferenza? (si/no): ");
                    rispostaConferenza = Console.ReadLine();

                    if (rispostaConferenza.ToLower() != "si" && rispostaConferenza.ToLower() != "no")
                    {
                        throw new Exception("Risposta non valida. Inserisci 'si' o 'no'.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }

            if (rispostaConferenza.ToLower() == "si")
            {
                string relatoreConferenza = "";
                while (relatoreConferenza == "")
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Inserisci il nome del relatore: ");
                        relatoreConferenza = Console.ReadLine();

                        if (relatoreConferenza.Any(char.IsDigit))
                        {
                            
                            throw new Exception("Errore: Il nome del relatore non può contenere numeri.");
                        }
                           

                        if (string.IsNullOrWhiteSpace(relatoreConferenza))
                        {
                            relatoreConferenza = "";
                            throw new Exception("Errore: Il nome del relatore non può essere vuoto.");
                        }
                            
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Errore: {e.Message}");
                    }
                }

                double prezzoConferenza = 0.0;
                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Inserisci il prezzo dell'evento: ");
                        string inputPrezzo = Console.ReadLine();

                        prezzoConferenza = Convert.ToDouble(inputPrezzo);

                        if (prezzoConferenza < 0)
                            throw new Exception("Il prezzo non può essere negativo.");

                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Errore: Formato prezzo non valido. Inserisci un valore numerico valido.");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Errore: Il valore del prezzo è troppo grande o troppo piccolo per essere gestito.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Errore: {e.Message}");
                    }
                }

                return new Conferenza(titoloEvento, dataEvento, postiTotali, relatoreConferenza, prezzoConferenza);
            }
            else
            {
                return new Evento(titoloEvento, dataEvento, postiTotali);
            }
        }
        static void PrenotaPosti(Evento evento)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Quanti posti desideri prenotare? ");
                    string inputPostiDaPrenotare = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputPostiDaPrenotare) || !int.TryParse(inputPostiDaPrenotare, out int postiDaPrenotare))
                        throw new Exception("Inserisci un numero valido per i posti da prenotare.");

                    if (postiDaPrenotare <= 0)
                        throw new Exception("Il numero di posti da prenotare deve essere maggiore di zero.");

                    evento.PrenotaPosti(postiDaPrenotare);

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }
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
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Indica il numero di posti da disdire: ");
                    string inputPostiDaDisdire = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputPostiDaDisdire) || !int.TryParse(inputPostiDaDisdire, out int postiDaDisdire))
                        throw new Exception("Inserisci un numero valido per i posti da disdire.");

                    if (postiDaDisdire < 0)
                        throw new Exception("Il numero di posti da disdire non può essere negativo.");

                    evento.DiscdiciPosti(postiDaDisdire);

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }
        }
        static void StartMenuDisdirePosti(Evento evento)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Vuoi disdire dei posti (si/no)? ");
                    string input = Console.ReadLine().ToLower();

                    if (input != "si" && input != "no")
                        throw new Exception("Risposta non valida. Scegli 'si' o 'no'.");

                    bool vuoiDisdire = input == "si";
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

                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }
        }

        static ProgrammaEventi CreaNuovaProgrammaEventi()
        {
            string titoloProgrammaEventi = "";
            while (string.IsNullOrWhiteSpace(titoloProgrammaEventi))
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Indica il nome del tuo programma Eventi: ");
                    titoloProgrammaEventi = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(titoloProgrammaEventi))
                        throw new Exception("Errore: Il nome del programma eventi non può essere vuoto.");
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                }
            }
            return new ProgrammaEventi(titoloProgrammaEventi);
        }

        static void StartMenuCreaNuovaProgrammaEventi()
        {
            ProgrammaEventi newProgrammaEventi = CreaNuovaProgrammaEventi();

            int eventiDaCreare = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Indica il numero di eventi da inserire: ");
                    eventiDaCreare = Convert.ToInt32(Console.ReadLine());

                    if (eventiDaCreare < 0)
                        throw new Exception("Il numero di eventi non può essere negativo.");
 
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Errore: Formato del numero non valido. Inserisci un numero intero valido.");
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }

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
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Inserisci una data per sapere che eventi ci saranno (gg/mm/yyyy): ");

                    string inputDataEvento = Console.ReadLine();
                    string formatoData = "dd/MM/yyyy";

                    if (DateTime.TryParseExact(inputDataEvento, formatoData, null, DateTimeStyles.None, out DateTime dataEvento))
                    {
                        List<Evento> eventiInData = programmaEventi.EventiInData(dataEvento);

                        if (eventiInData.Count > 0)
                        {
                            Console.WriteLine();
                            ProgrammaEventi.StampaEventi(eventiInData);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Non ci sono eventi per data selezionata!");
                        }

                        break;
                    }
                    else
                    {
                        throw new Exception("Formato data non valido. Assicurati di inserire la data nel formato corretto (gg/mm/yyyy).");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }             
        }

        public static void EliminaTuttiEventi(ProgrammaEventi newProgrammaEventi)
        {
            bool vuoiCancellare = false;
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Vuoi eliminare tutti eventi (si/no)? ");

                    string input = Console.ReadLine().ToLower();

                    if (input == "si" || input == "no")
                    {
                        vuoiCancellare = input == "si";
                        break;
                    }
                    else
                    {
                      
                        throw new Exception("Errore: Inserisci 'si' per confermare l'eliminazione o 'no' per annullare.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Errore: {e.Message}");
                }
            }
           
            switch (vuoiCancellare)
            {
                case true:
                    newProgrammaEventi.SvuotaListaEventi();
                    Console.WriteLine();
                    Console.WriteLine("La lista di Eventi e svuotata con successo!");

                    break;
                case false:
                    Console.WriteLine();
                    Console.WriteLine("Ok va bene!");;
                    break;
            }
        }
    }

  
}
