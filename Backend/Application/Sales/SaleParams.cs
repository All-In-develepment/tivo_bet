using Application.Core;

namespace Application.Sales
{
    public class SaleParams : PagingParams
    {
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
}