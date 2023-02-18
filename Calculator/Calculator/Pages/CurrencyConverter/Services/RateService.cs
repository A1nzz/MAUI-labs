using System.Text.Json;
using Calculator.Pages.CurrencyConverter.Entities;

namespace Calculator.Pages.CurrencyConverter.Services
{
    public class RateService : IRateService
    {
        private readonly HttpClient _client;

        private readonly List<int> _ArrayOfCurrenciesId = new List<int>() { 456, 451, 431, 426, 462, 429 };
        public RateService()
        {
            _client = new HttpClient();
        }
        public async Task<IEnumerable<Rate>> GetRates(DateTime date)
        {
            var RateItems = new List<Rate>();
            Uri uri = new Uri(string.Format("https://www.nbrb.by/api/exrates/rates", string.Empty));
            var response = await _client.GetAsync($"{uri}?ondate={date:yyyy-M-d}&periodicity=0");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                RateItems = JsonSerializer.Deserialize<List<Rate>>(content);
            }
            return RateItems.Where(x => _ArrayOfCurrenciesId.Contains(x.Cur_ID));
        }
    }
}
