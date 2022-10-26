namespace ReversiMvcApp.Models
{

    // Enum reversi kleuren
    public enum Kleur
    {
        Wit,
        Zwart,
        Geen
    }


    public class Spel
    {
        public int Token { get; set; }

        public string Omschrijving { get; set; }

        public int Speler1Token { get; set; }
        public string Speler1Naam { get; set; }
        public int Speler2Token { get; set; }
        public int Speler2Naam { get; set; }

        public Kleur[,] Bord { get; set; }


    }
}
