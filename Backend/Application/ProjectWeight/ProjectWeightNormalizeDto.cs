namespace Application.ProjectWeight
{
    public class ProjectWeightNormalizeDto
    {
        public Guid ProjectWeightId { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime Month { get; set; }
        public decimal SalesValueWeight { get; set; }
        public decimal SalesValueWeightNormalized { get; set; }
        public decimal ConversionWeight { get; set; }
        public decimal ConversionWeightNormalized { get; set; }
        public decimal RegistrationWeight { get; set; }
        public decimal RegistrationWeightNormalized { get; set; }
        public decimal DepositWeight { get; set; }
        public decimal DepositWeightNormalized { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}