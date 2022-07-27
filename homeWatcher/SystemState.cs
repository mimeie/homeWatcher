namespace homeWatcher
{
    public class SystemState
    {
        public DateTime StatusTimestamp { get; set; }    
        
        public int StatusCounter { get; set; }

        public string? StatusMessage { get; set; }
    }
}
