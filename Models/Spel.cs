using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ReversiMvcApp.Models
{

    // Enum reversi kleuren
    public enum Kleur
    {
        Geen,
        Wit,
        Zwart
    }


    public class Spel
    {
        [Key]
        public int Token { get; set; }

        public string Omschrijving { get; set; }

        public string Speler1Token { get; set; }
        public string Speler1Naam { get; set; }
        public string Speler2Token { get; set; }
        public string Speler2Naam { get; set; }

        public Kleur AanDeBeurt { get; set; }
        
    }
}
