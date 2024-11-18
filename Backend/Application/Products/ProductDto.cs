namespace Application.Products
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProdutcDescription { get; set; }
        public bool ProductIsActive { get; set; }
        public decimal ProductPrice { get; set; }
    }
}