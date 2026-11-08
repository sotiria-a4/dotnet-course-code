using System.Text.Json.Serialization;

namespace HelloWorld.Models
{
    public class Computer
    {
        // private string _motherboard ;
        [JsonPropertyName("computer_id")]
        public int ComputerId { get; set; }
        [JsonPropertyName("motherboard")]
        public string Motherboard { get; set; } //  Το private σημαίνει ότι η property είναι προσβάσιμη μόνο μέσα στην ίδια την κλάση Computer.
        [JsonPropertyName("cpu_cores")]
        public int? CPUCores { get; set; }
        [JsonPropertyName("has_wifi")]
        public bool HasWifi { get; set; }
        [JsonPropertyName("has_lte")]
        public bool HasLTE { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("video_card")]
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