using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProdutcDescription { get; set; }
        public bool ProductIsActive { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ProductPrice { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}