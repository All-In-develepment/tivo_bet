using Domain;

namespace Application.Register
{
    public class RegisterDto
    {
        public Guid RegisterId { get; set; }
        public DateTime RegisterDate { get; set; }
        public int RegisterTotal { get; set; }
        public decimal RegisterAmount { get; set; }
        public decimal RegisterAVG { get; set; }
        public int RegisterLeads { get; set; }
        public float RegisterAVGConversion { get; set; }
        public string RegisterType { get; set; }
        public Guid EventsId { get; set; }
        public string EventsName { get; set; }
        public Guid SellerId { get; set; }
        public string SellerName { get; set; }
        public Guid BookmakerId { get; set; }
        public string BookmakerName { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}