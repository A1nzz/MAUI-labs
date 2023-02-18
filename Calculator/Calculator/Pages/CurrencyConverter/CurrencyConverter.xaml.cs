using Calculator.Pages.CurrencyConverter.Entities;
using Calculator.Pages.CurrencyConverter.Services;


namespace Calculator.Pages.CurrencyConverter;

public partial class CurrencyConverter : ContentPage
{

    private IRateService _rateService;

    public List<Rate> RatesList { get; set; } = new();

    public DateTime TodayDate { get; init; }

    private Rate _curRate { get; set; } = null;


    public CurrencyConverter()
    {
        TodayDate = DateTime.Today;
        InitializeComponent();
        _rateService = new RateService();
        BindingContext = this;
    }

    public async void DateSelected(object sender, EventArgs e)
    {
        RatesList = (await _rateService.GetRates(datePicker.Date)).ToList();
        rightPicker.ItemsSource = RatesList;
        BynEntry.Text = "";
    }

    public async void OnLoaded(object sender, EventArgs e)
    {
        RatesList = (await _rateService.GetRates(datePicker.Date)).ToList();
        rightPicker.ItemsSource = RatesList;
    }

    public void OnCurrentRateChanged(object sender, EventArgs e)
    {
        _curRate = rightPicker.SelectedItem as Rate;
        if (BynEntry.Text == String.Empty)
        {
            CurrencyEntry.Text = String.Empty;
        }
        else if (_curRate != null)
        {
            CurrencyEntry.TextChanged -= OnCurrencyEntryTextChanged;
            BynEntry.TextChanged -= OnBynEntryTextChanged;
            BynEntry.Text = $"{ConvertCurrencyToBYN(Convert.ToDecimal(_curRate.Cur_OfficialRate / _curRate.Cur_Scale), Convert.ToDecimal(CurrencyEntry.Text)):0.##}";
            CurrencyEntry.TextChanged += OnCurrencyEntryTextChanged;
            BynEntry.TextChanged += OnBynEntryTextChanged;
        }
    }

    public void OnBynEntryTextChanged(object sender, TextChangedEventArgs e)
    {

        if (BynEntry.Text == String.Empty)
        {
            CurrencyEntry.Text = String.Empty;
        }
        else
        {

            if (_curRate != null)
            {

                decimal number;

                if (Decimal.TryParse(BynEntry.Text, out number))
                {
                    CurrencyEntry.TextChanged -= OnCurrencyEntryTextChanged;
                    CurrencyEntry.Text = $"{ConvertBYNToCurrency(Convert.ToDecimal(_curRate.Cur_OfficialRate / _curRate.Cur_Scale), number):0.##}";
                    CurrencyEntry.TextChanged += OnCurrencyEntryTextChanged;
                } else
                {
                    BynEntry.Text = e.OldTextValue;
                }
            }

        }
    }
    public void OnCurrencyEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (CurrencyEntry.Text == String.Empty)
        {
            BynEntry.Text = String.Empty;
        }
        else
        {
            if (_curRate != null)
            {
                decimal number;
                if (Decimal.TryParse(CurrencyEntry.Text, out number))
                {
                    BynEntry.TextChanged -= OnBynEntryTextChanged;
                    BynEntry.Text = $"{ConvertCurrencyToBYN(Convert.ToDecimal(_curRate.Cur_OfficialRate / _curRate.Cur_Scale), number):0.##}";
                    BynEntry.TextChanged += OnBynEntryTextChanged;
                }
                else
                {
                    CurrencyEntry.Text = e.OldTextValue;

                }

            }
        }
    }

    private decimal ConvertBYNToCurrency(decimal curRate, decimal enteredByn)
    {
        return enteredByn / curRate;
    }

    private decimal ConvertCurrencyToBYN(decimal curRate, decimal enteredCurrency)
    {
        return enteredCurrency * curRate;
    }
}