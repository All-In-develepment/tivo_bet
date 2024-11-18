using Application.Core;

namespace Application.Register
{
    public class RegisterParams : PagingParams
    {
        public DateTime StartDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
}