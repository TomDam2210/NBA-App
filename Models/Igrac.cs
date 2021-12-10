using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBA_WebApp.Models
{
    public class Igrac
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pozicija { get; set; }
        public string Nadimak { get; set; }
        public int Broj_dresa { get; set; }

        public Igrac()
        {
            
        }
    }
}
