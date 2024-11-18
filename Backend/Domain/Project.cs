namespace Domain
{
    public class Project
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public bool ProjectIsActive { get; set; }
        public ICollection<Seller> Sellers { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Register> Registers { get; set; }
        public ICollection<SalesPerformanceTeam> SalesPerformanceTeams { get; set; }
        public ICollection<ProjectWeight> ProjectWeights { get; set; }
    }
}