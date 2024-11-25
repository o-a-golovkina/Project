using System.Text.RegularExpressions;

namespace Project
{
    public class Product
    {
        private string? name;
        private double price;
        private ProductType type;

        private static readonly Regex regex = new(@"^[a-zA-Z]{3,}$");

        public string Name
        {
            get => name!;
            set
            {
                if (value == null || !regex.IsMatch(value))
                    throw new FormatException("Product name must use Latin letters and be over two characters!");
                name = value;
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (value <= 0)
                    throw new FormatException("Value of price must be above 0!");
                price = value;
            }
        }

        public ProductType Type
        {
            get => type;
            set => type = value;
        }

        public Product(string name, double price, ProductType type)
        {
            Name = name;
            Price = price;
            Type = type;
        }
    }
}
