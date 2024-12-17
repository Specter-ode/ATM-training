namespace ATM_training;

// Класс для хранения информации о счете пользователя
public class Wallet
{
    // Словарь для хранения баланса по каждой валюте
    private readonly Dictionary<Currencies, float> _balances;

    // Конструктор, принимающий начальные значения баланса для каждой валюты
    public Wallet(float amount_USD = 0f, float amount_EUR = 0f, float amount_GBP = 0f)
    {
        _balances = new Dictionary<Currencies, float>
        {
            { Currencies.USD, amount_USD },
            { Currencies.EUR, amount_EUR },
            { Currencies.GBP, amount_GBP }
        };
    }

    // ВАРИАНТ 2. Если мы не знаем какие валюты будут
    // public Wallet(Dictionary<Currencies, float> initialBalances)
    // {
    //    _balances = initialBalances;
    // }

    // Метод для депозита средств
    public void Deposit()
    {
        var currency = GetCurrencyFromInput("deposit");
        if (currency == null) return;

        var amount = GetAmountFromInput("deposit");
        if (amount == null) return;


        _balances[currency.Value] += amount.Value;
        Console.WriteLine($"You have successfully deposited {amount} {currency.Value}.");
        Console.WriteLine($"New balance: {_balances[currency.Value]} {currency.Value}.");
    }

    // Метод для снятия средств
    public void Withdraw()
    {
        var currency = GetCurrencyFromInput("withdraw");
        if (currency == null) return;

        var amount = GetAmountFromInput("withdraw");
        if (amount == null) return;
        // проверяем хватает ли денег на счета для вывода, иначе показываем сообщение с ошибкой
        if (_balances[currency.Value] < amount)
        {
            Console.WriteLine("There are insufficient funds in the account.");
            return;
        }

        _balances[currency.Value] -= amount.Value;
        Console.WriteLine($"You have successfully withdraw {amount} {currency.Value}.");
        Console.WriteLine($"New balance: {_balances[currency.Value]} {currency.Value}.");
    }


    // Метод для обмена валют
    public void Exchange()
    {
        var fromCurrency = GetCurrencyFromInput("exchange from");
        if (fromCurrency == null) return;

        var amount = GetAmountFromInput("exchange");
        if (amount == null) return;
        // Проверка наличия средств в выбранной валюте на счете
        if (_balances[fromCurrency.Value] < amount)
        {
            Console.WriteLine("Insufficient funds for exchange.");
            return;
        }

        var toCurrency = GetCurrencyFromInput("exchange to");
        if (toCurrency == null) return;

        // если валюта совпадает, то прерываем эту операцию
        if (fromCurrency.Value == toCurrency.Value)
        {
            Console.WriteLine("You cannot exchange the same currency. Please choose different currencies.");
            return;
        }

        _balances[fromCurrency.Value] -= amount.Value;
        var targetAmount = CurrencyExchange.Convert(fromCurrency.Value, toCurrency.Value, amount.Value);
        if (targetAmount == null)
        {
            // Если курс обмена отсутствует, выводим сообщение и завершаем операцию
            Console.WriteLine("Currency exchange failed due to missing exchange rate.");
            return;
        }

        _balances[toCurrency.Value] += targetAmount.Value;

        Console.WriteLine($"Exchanged {amount} {fromCurrency.Value} to {targetAmount.Value} {toCurrency.Value}.");
        Console.WriteLine(
            $"New balances: {fromCurrency.Value}: {_balances[fromCurrency.Value]}, {toCurrency.Value}: {_balances[toCurrency.Value]}.");
    }

    // Метод для отображения баланса
    public void Balance()
    {
        foreach (var balance in _balances) Console.WriteLine($"{balance.Key}: {balance.Value}");
    }


    // Приватный метод для ввода и проверки валюты
    private Currencies? GetCurrencyFromInput(string actionDescription)
    {
        Console.WriteLine($"Enter the currency for {actionDescription} (USD, EUR, GBP):");
        var input = Console.ReadLine();

        // Проверяем, корректно ли введено значение независимо от регистра
        if (!Enum.TryParse(input, true, out Currencies currency))
        {
            Console.WriteLine("Invalid currency. Please try again.");
            return null;
        }

        // Проверяем, есть ли счёт в указанной валюте
        if (!_balances.ContainsKey(currency))
        {
            Console.WriteLine($"You do not have an account in {currency}.");
            return null;
        }

        // Возвращаем валюту, если все проверки пройдены
        return currency;
    }

    // Приватный метод для ввода суммы и округления
    private float? GetAmountFromInput(string actionDescription)
    {
        Console.WriteLine($"Enter the amount to {actionDescription}:");
        var input = Console.ReadLine();

        // Проверяем, является ли введённое значение числом
        if (!float.TryParse(input, out var amount))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return null;
        }

        // Проверяем, является ли число положительным
        if (amount <= 0)
        {
            Console.WriteLine("The amount must be greater than zero.");
            return null;
        }

        // Округляем сумму до двух знаков и возвращаем её
        return MathF.Round(amount, 2);
    }
}