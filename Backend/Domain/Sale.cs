using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Sale
    {
        public Guid SaleId { get; set; }

        // Relação de Product com Sale, para converter ProductId em ProductName
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        // Relação de Seller com Sale, para converter SellerId em SellerName
        public Seller Seller { get; set; }
        public Guid SellerId { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SalePrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}