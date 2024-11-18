namespace Domain
{
    public class Bookmaker
    {
        public Guid BookmakerId { get; set; }
        public string BookmakerName { get; set; }
        public ICollection<Register> Registers { get; set; }
    }
}