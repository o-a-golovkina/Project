using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface ICustomer
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public bool CreateOrder(Order order);
        public Order FindOrder(int number);
        public bool BuyOrder(Order order);
        public bool DeleteOrder(Order order);
        public string GetInfo();
    }
}
