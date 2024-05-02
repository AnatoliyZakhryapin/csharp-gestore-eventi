using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreEventi
{
    internal class ProgrammaEventi
    {
        public string Titolo { get; private set; }
        public List<Evento> Eventi { get; private set; }

        public ProgrammaEventi(string titolo)
        {
            Titolo = titolo;
            Eventi = new List<Evento>();
        }

        public void AggiungiEventoAllaLista(Evento evento)
        {
            Eventi.Add(evento);
        }

        public List<Evento> EventiInData(DateTime dataDaFiltrare)
        {
            List<Evento> eventiInData = new List<Evento>();

            foreach (Evento evento in Eventi)
            {
                if (evento.Data.Date == dataDaFiltrare.Date)
                    eventiInData.Add(evento);
            }

            return eventiInData;
        }

        public static string CreaStringaDiListaEventi(List<Evento> eventi)
        {
            StringBuilder result = new StringBuilder();

            foreach (Evento evento in eventi)
            {
                result.AppendLine($"\t{evento.ToString()} ");
            }

            return result.ToString();
        }

        public int NumeroEventi()
        {
            return Eventi.Count;
        }

        public void SvuotaListaEventi()
        {
            Eventi.Clear();
        }

        public string CreaStringaDiProgrammaEventi()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{Titolo}");

            result.Append(CreaStringaDiListaEventi(Eventi));

            return result.ToString();
        }

        public static void StampaEventi(List<Evento> eventi)
        {
            string EventiInStringa = CreaStringaDiListaEventi(eventi);

            Console.WriteLine($"{EventiInStringa}");
        }
    }
}
