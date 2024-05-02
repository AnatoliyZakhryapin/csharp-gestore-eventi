using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreEventi
{
    internal class Conferenza : Evento
    {
        private string relatore;
        private double prezzo;

        public Conferenza(string titolo, DateTime data, int posti, string relatore, double prezzo ) : base(titolo, data, posti)
        {
            Relatore = relatore;
            Prezzo = prezzo;
        }

        public string Relatore
        {
            get { return relatore; }
            private set { relatore = value; }
        }

        public double Prezzo
        {
            get { return prezzo; }
            private set { prezzo = value; }
        }

        public string DataFormattata()
        {
            return Data.ToString("dd/MM/yyyy");
        }

        public string PrezzoFormattato()
        {
            return prezzo.ToString("0.00");
        }

        public override string ToString()
        {
            return $"{DataFormattata()} - {Titolo} - {Relatore} - {PrezzoFormattato()} euro";
        }
    }
}
