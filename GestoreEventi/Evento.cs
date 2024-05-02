using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreEventi
{
    internal class Evento
    {
        private string titolo;
        private DateTime data;
        private int posti;
        private int postiPrenotati;

        public Evento(string titolo, DateTime data, int posti) 
        {
            Titolo = titolo;
            Data = data;
            Posti = posti;
            PostiPrenotati = 0;
        }

        public string Titolo
        {
            get { return titolo; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Titolo non puo essere vuoto");

                titolo = value; 
            }
        }

        public DateTime Data
        {
            get { return data; }
            set 
            { 
                DateTime dataAttuale = DateTime.Now;

                if (value == DateTime.MinValue) 
                    throw new Exception("La data non può essere nulla.");

                if (dataAttuale > value)
                    throw new Exception("La data dell'evento non puo essere nel passato!");

                data = value; 
            }
        }

        public int Posti
        {
            get { return posti; }
            private set 
            {
                if (value <= 0)
                    throw new Exception("La capienza massima deve esse maggiore di zero.");

                posti = value; 
            }
        }

        public int PostiPrenotati
        {
            get { return postiPrenotati; }
            private set { postiPrenotati = value; }
        }

        public int PostiDisponibili() => this.Posti - this.PostiPrenotati;

        public void PrenotaPosti (int postiDaPrenotare)
        {
            DateTime dataAttuale = DateTime.Now;

            if (PostiDisponibili() <= 0)
                throw new Exception("I posti disponibili sono terminati!");
            if (postiDaPrenotare > PostiDisponibili())
                throw new Exception("I posti disponibili sono inferiori a queli da prenotare, cambia il numero di posti!");
            if (dataAttuale >= this.Data)
                throw new Exception("Evento terminato, non puoi fare operazioni!");

            PostiPrenotati += postiDaPrenotare;
        }

        public void DiscdiciPosti (int postiDaDisdire)
        {
            DateTime dataAttuale = DateTime.Now;

            if (dataAttuale >= this.Data)
                throw new Exception("Evento terminato, non puoi fare operazioni!");
            if (PostiPrenotati < postiDaDisdire)
                throw new Exception("Evento terminato, non puoi fare operazioni!");

            PostiPrenotati -= postiDaDisdire;
        }

        public override string ToString()
        {
            string dataFormatata = this.Data.ToString("dd/MM/yyyy");

            return $"{dataFormatata} - {this.Titolo}";
        }
    }
}
