namespace Application.Seller
{
    public class SellerDto
    {
        public Guid SellerId { get; set; }
        public string SellerName { get; set; }
        public bool SellerIsActive { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}