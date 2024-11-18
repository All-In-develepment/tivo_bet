namespace Application.ProjectWeight
{
    public class ProjectWeightDto
    {
        public Guid ProjectWeightId { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime Month { get; set; }
        public decimal SalesValueWeight { get; set; }
        public decimal ConversionWeight { get; set; }
        public decimal RegistrationWeight { get; set; }
        public decimal DepositWeight { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}