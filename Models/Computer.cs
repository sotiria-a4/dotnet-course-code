namespace HelloWorld.Models
{
    public class Computer
    {
        // private string _motherboard ;
        public int ComputerId { get; set; }
        public string Motherboard { get; set; } //  Το private σημαίνει ότι η property είναι προσβάσιμη μόνο μέσα στην ίδια την κλάση Computer.

        public int? CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; }


        public Computer() // to default einai private. ara den mporw na ftiaksw instance aytou new Computer() apo allh klash. An to kano public tote mporw na ftiaksw instance apo allh klash.
        {
            if (VideoCard == null)
            {
                VideoCard = "";
            }
            if (Motherboard == null)
            {
                Motherboard = "";
            }
            if (CPUCores == null)
            {
                CPUCores = 0;
            }
        }
    }
}