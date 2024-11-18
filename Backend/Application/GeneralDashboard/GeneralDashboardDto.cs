namespace Application.GeneralDashboard
{
    public class GeneralDashboardDto
    {
        public string SellerName { get; set; }
        public string TotalLeads { get; set; }
        public Decimal TotalSell { get; set; }
        public Decimal TotalDeposit { get; set; }
        public Decimal TotalReDeposit { get; set; }
        public Decimal TotalAmount { get; set; }
        public float TotalConversion { get; set; }
    }
}