namespace ATM_training;

public enum Currencies
{
    USD,
    EUR,
    GBP
}

public static class CurrencyExchange
{
    // ВАРИАНТ №1. Словарь для хранения курса обмена
    private static readonly Dictionary<(Currencies from, Currencies to), float> ExchangeRates = new()
    {
        { (Currencies.USD, Currencies.EUR), 0.91f },
        { (Currencies.EUR, Currencies.USD), 1.1f },
        { (Currencies.USD, Currencies.GBP), 0.78f },
        { (Currencies.GBP, Currencies.USD), 1.28f }
        // { (Currencies.EUR, Currencies.GBP), 0.86f },
        // { (Currencies.GBP, Currencies.EUR), 1.16f }
    };

    // ВАРИАНТ №2. Словарь для хранения курса обмена
    // как лучше хранить Курс Валют если б был реальный проект
    private static readonly Dictionary<Currencies, Dictionary<Currencies, float>> ExchangeRatesSecond = new()
    {
        {
            Currencies.USD,
            new Dictionary<Currencies, float> { { Currencies.EUR, 0.91f }, { Currencies.GBP, 0.78f } }
        },
        {
            Currencies.EUR,
            new Dictionary<Currencies, float> { { Currencies.USD, 1.1f }, { Currencies.GBP, 0.86f } }
        },
        {
            Currencies.GBP,
            new Dictionary<Currencies, float> { { Currencies.USD, 1.28f }, { Currencies.EUR, 1.16f } }
        }
    };


    // Метод для получения курса обмена и делаем конвертацию
    public static float? Convert(Currencies fromCurrency, Currencies toCurrency, float amount)
    {
        // Если валюты одинаковы, возвращаем обратно первоначальную сумму
        if (fromCurrency == toCurrency) return amount;

        if (ExchangeRates.TryGetValue((fromCurrency, toCurrency), out var rate)) return MathF.Round(amount * rate, 2);
        // Если курс обмена отсутствует, возвращаем null
        Console.WriteLine($"Exchange rate from {fromCurrency} to {toCurrency} is not defined.");
        return null;
    }
}