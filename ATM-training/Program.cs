using System;
using System.Collections.Generic;

namespace ATM_training
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool inAction = false; // Флаг для отслеживания выполняет ли юзер действие
            // изначально должен идти запрос на сервер для получения информации о всех открытых счетах юзера
            // в ответе должны получить Dictionary(валюта, сумма средств в этой валюте)
            // после мы должны инициализировать userWallet с этими значениями
            // var initialBalances = new Dictionary<Currencies, float>
            // {
            //      { Currencies.USD, 1000.0f },
            //      { Currencies.EUR, 800.0f },
            //      { Currencies.GBP, 700.0f }
            // };
            var userWallet = new Wallet(1000, 800, 700);


            // после этого нужно инициализировать пункты меню
            // возмоможно у некоторых пользователей, есть доп.пункты меню в зависимости от его статуса
            // карта физ.лицо/юр.лицо. Рассматриваем сценарий физ лица

            // Пункты для главного меню
            var mainMenuItems = new List<Board.BoardItem>
            {
                new Board.BoardItem(1, "View your balance"),
                new Board.BoardItem(2, "Depositing funds"),
                new Board.BoardItem(3, "Withdrawals"),
                new Board.BoardItem(4, "Currency exchange"),
                new Board.BoardItem(5, "Exit")
            };

            // Создаем главное меню
            var mainMenu = new Board(mainMenuItems);
            Console.WriteLine("Welcome to the ATM!");
            while (true)
            {
                // Если мы не в процессе действия, показываем меню
                if (!inAction)
                {
                    mainMenu.Display(); // Показываем меню

                    // Получаем ввод пользователя
                    string selectedAction = Console.ReadLine();

                    inAction = true; // Устанавливаем флаг в true, так как мы начали обработку действия

                    switch (selectedAction)
                    {
                        case "1":
                            // Просмотр баланса
                            userWallet.Balance();
                            break;
                        case "2":
                            // Внесение средств
                            userWallet.Deposit();
                            break;
                        case "3":
                            // Снятие средств
                            userWallet.Withdraw();
                            break;
                        case "4":
                            // Обмен валют
                            userWallet.Exchange();
                            break;
                        case "5":
                            // Выход
                            Console.WriteLine("Thanks for using the ATM!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("This menu item does not exist. Try again.");

                            break;
                    }
                    

                    // Сообщение для возврата в меню
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey(); // Ждем нажатия клавиши перед возвращением в меню
                    Console.WriteLine("\n");
                    inAction = false; // Завершаем текущее действие, можем снова показать меню
                }
            }
        }
    }
}