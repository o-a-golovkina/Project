using System.Collections;

namespace Project
{
    public class Order : IEnumerable, IComparable<Order>
    {
        private readonly DateTime date;
        private static int number = 0;

        public int Number { get; set; }

        private static readonly Random random = new();

        public DiscoundCard Card { get; set; }

        public Status Status { get; set; }

        public Customer Customer { get; set; }

        public List<Product> Products { get; private set; }

        public DateTime Date => date;

        public Order(Customer customer)
        {
            Customer = customer;
            Card = customer.Card;
            Products = [];
            Number = ++number;
            Status = Status.NEW;
            DateTime startDate = new(2024, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            date = startDate.AddDays(random.Next(range));
        }

        public static void ChangeNumer() => --number;

        public double GetTotalPrice() => Products.Sum(p => p.Price);

        public bool AddProduct(Product product, int count)
        {
            if (count <= 0)
                return false;

            for (int i = 1; i <= count; i++)
                Products.Add(product);
            return true;
        }

        public bool RemoveProduct(Product product) => Products.Remove(product);

        public void Clear() => Products.Clear();

        public IEnumerator GetEnumerator() => Products.GetEnumerator();

        public int CompareTo(Order? other) => other == null ? throw new ArgumentNullException("Incorrect value!") : Date.CompareTo(other.Date);
    }
}
