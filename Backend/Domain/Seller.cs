namespace Domain
{
    public class Seller
    {
        public Guid SellerId { get; set; }
        public string SellerName { get; set; }
        public bool SellerIsActive { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Register> Registers { get; set; }
        public ICollection<SalesPerformanceTeam> SalesPerformanceTeams { get; set; }
    }
}