namespace Application.Sales
{
    public class SaleDto
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}