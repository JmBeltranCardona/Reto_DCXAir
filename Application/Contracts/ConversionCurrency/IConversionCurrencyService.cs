namespace Application.Contracts.ConversionCurrency
{
    public interface IConversionCurrencyService
    {
        Task<double?> ConvertCurrency(string currentCurrency, string currencyToConvert, double amount);
    }
}
