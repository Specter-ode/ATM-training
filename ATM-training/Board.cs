namespace ATM_training;


public enum MenuItems
{
    Main,
    Balance,
    Deposit,
    Withdrawal,
    CurrencyExchange
}

public class Board
{
    private List<BoardItem> _boardItems;
    // Конструктор, который принимает список элементов доски
    public Board(List<BoardItem> boardItems)
    {
        _boardItems = boardItems;
    }
    
    public void Display()
    {
        Console.WriteLine("Select action");
        foreach (var item in _boardItems)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("----------------");
    }
    public class BoardItem
    {
        private int _case;
        private string _title;
   

        public BoardItem(int caseNumber, string title)
        {
            _case = caseNumber;
            _title = title;
        }

        public override string ToString()
        {
            return $"{_case}. {_title}";
        }
    }
}