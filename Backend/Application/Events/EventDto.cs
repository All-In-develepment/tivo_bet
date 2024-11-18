namespace Application.Events
{
    public class EventDto
    {
        public Guid EventsId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public ICollection<Domain.Register> Registers { get; set; }
    }
}