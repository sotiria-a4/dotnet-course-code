namespace HelloWorld.Models
{
    public class ComputerSnake
    {
        // private string _motherboard ;
        public int computer_id { get; set; }
        public string motherboard { get; set; } //  Το private σημαίνει ότι η property είναι προσβάσιμη μόνο μέσα στην ίδια την κλάση Computer.

        public int? cpu_cores { get; set; }
        public bool has_wifi { get; set; }
        public bool has_lte { get; set; }
        public DateTime? release_date { get; set; }
        public decimal price { get; set; }
        public string video_card { get; set; }


        public ComputerSnake() // to default einai private. ara den mporw na ftiaksw instance aytou new Computer() apo allh klash. An to kano public tote mporw na ftiaksw instance apo allh klash.
        {
            if (video_card == null)
            {
                video_card = "";
            }
            if (motherboard == null)
            {
                motherboard = "";
            }
            if (cpu_cores == null)
            {
                cpu_cores = 0;
            }
        }
    }
}