using System.Text.RegularExpressions;

namespace Project
{
    public class Customer : ICustomer
    {
        private string? name;
        private double balance;
        private int boughtOrders = 0;

        public static readonly Regex regex = new(@"^[a-zA-Z]{3,}$");

        public List<Order> Orders { get; private set; } = [];

        public DiscountCard Card { get; private set; }

        public string Name
        {
            get => name!;
            set
            {
                if (value == null || !regex.IsMatch(value))
                    throw new FormatException("Customer name must use Latin letters and be over two characters!");
                name = value;
            }
        }

        public double Balance
        {
            get => balance;
            set
            {
                if (value <= 0)
                    throw new FormatException("Value of balance must be above 0!");
                balance = value;
            }
        }

        public Customer(string name, double balance)
        {
            Name = name;
            Balance = balance;
            Card = new WhiteCard();
        }

        public bool CreateOrder(Order order)
        {
            if (order == null)
                return false;
            Orders.Add(order);
            return true;
        }

        public Order FindOrder(int number)
        {
            int i = Orders.FindIndex(o => o.Number == number);
            if (i == -1)
                return null!;
            return Orders[i];
        }

        public bool PutMoney(int amount)
        {
            if (amount <= 0)
                return false;
            balance += amount;
            return true;
        }

        public bool BuyOrder(Order order, ref string s)
        {
            if (order.Status == Status.BOUGHT)
                return false;
            if (balance < PriceWithDiscount(order))
                return false;
            balance -= PriceWithDiscount(order);
            order.Status = Status.BOUGHT;

            boughtOrders++;

            if (boughtOrders == 2)
            {
                s = "black";
                Card = new BlackCard();
            }

            if (boughtOrders == 5)
            {
                s = "gold";
                Card = new GoldCard();
            }

            return true;
        }

        private double PriceWithDiscount(Order order) => order.GetTotalPrice() - order.Card.CalculateDiscount(order.GetTotalPrice());

        public bool DeleteOrder(Order order) => order != null && Orders.Remove(order);

        public string GetInfo()
        {
            string str = $" Name: {name}\t Balance: {balance:f2}$\t Card: {Card.CardType} ({Card.Percent}%)\n";
            if (Orders.Count == 0)
                str += " Order list is clear.";
            else
            {
                Orders.Sort();
                str += "┌───────────┬────────┬────────┬───────────────────────────────┬─────────┬─────────┐\n" +
                       "│ Date      │ Number │ Status │ Products                      │ Price   │ Total   │\n" +
                       "├───────────┼────────┼────────┼───────────────────────────────┼─────────┼─────────┤\n";

                foreach (Order order in Orders)
                {
                    List<Product> outputs = [];
                    foreach (Product product in order)
                    {
                        string productDetails = $"{product.Name}({product.Type}) ‒ {product.Price:f2}$";
                        string orderPrice = $"{order.GetTotalPrice():f2}$";
                        string orderDiscount = $"{PriceWithDiscount(order):f2}$";
                        if (outputs.Count == 0)
                        {
                            str += $"│{order.Date:yyyy-MM-dd} │{order.Number:D3}     │{order.Status,-7} │{productDetails,-30} │{orderPrice,-8} │{orderDiscount,-8} │\n";
                            outputs.Add(product);
                        }
                        else
                            str += $"│           │        │        │{productDetails,-30} │         │         │\n";
                    }
                    str += "├───────────┼────────┼────────┼───────────────────────────────┼─────────┼─────────┤\n";
                }
                str += "└───────────┴────────┴────────┴───────────────────────────────┴─────────┴─────────┘\n";
            }

            return str;
        }
    }
}