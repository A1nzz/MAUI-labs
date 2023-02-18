using Calculator.Pages.CurrencyConverter.Entities;

namespace Calculator.Pages.CurrencyConverter.Services
{
    public interface IRateService
    {
        public Task<IEnumerable<Rate>> GetRates(DateTime date);
    }
}
