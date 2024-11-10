using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Order
    {
        private DateTime date;
        private int number = 1;

        public Customer Customer { get; set; }
        public List<Product> Products { get; private set; }
        public DateTime Date
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public int Number
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public Status Status { get; set; }

        public Order(Customer customer)
        {
            //Initialise Products
            throw new NotImplementedException();
        }

        public double GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public bool AddProduct(Product product, int count)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
