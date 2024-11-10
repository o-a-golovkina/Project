using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Customer: ICustomer
    {
        private string name;
        private double balance;

        public List<Order> Orders { get; private set; } = new List<Order>();
        public string Name
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public double Balance
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public Customer(string name, double balance)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrder(Order order)
        {            
            throw new NotImplementedException();
        }

        public Order FindOrder(int number)
        {
            throw new NotImplementedException();
        }

        public bool BuyOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public string GetInfo()
        {
            throw new NotImplementedException();
        }
    }
}
