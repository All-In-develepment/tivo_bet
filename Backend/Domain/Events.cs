using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Events
    {
        [Key]
        public Guid EventsId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public ICollection<Register> Registers { get; set; }
        public ICollection<SalesPerformanceTeam> SalesPerformanceTeams { get; set; }
    }
}