using Application.Core;

namespace Application.SalesPerformanceTeam
{
    public class SPTParams : PagingParams
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? SellerId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}