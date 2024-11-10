using Project;

namespace TestProject
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        [DataRow(null, null)]
        [DataRow("", 530.90)]
        [DataRow(" ", 530.90)]
        [DataRow("M", 530.90)]
        [DataRow("Mike", -530.90)]
        public void TestMethod_CreateWrongCustomer(string name, double balance)
        {
            //Arrange
            Customer customer;

            //Act + Assert
            Assert.ThrowsException<ArgumentException>(() => customer = new Customer(name, balance));
        }

        [TestMethod]
        public void TestMethod_CreateCorrectCustomer()
        {
            //Arrange
            string name = "Mike";
            double balance = 530.90;
            Customer expectedResult = new Customer("Mike", 530.90);

            //Act
            Customer actualResult = new Customer(name, balance);

            //Assert
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
            Assert.AreEqual(expectedResult.Balance, actualResult.Balance);
        }

        [TestMethod]
        public void TestMethod_CreateCorrectOrder()
        {
            // Arrange
            Customer customer = new Customer("Mike", 530.90);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Order order = new Order(customer);
            order.AddProduct(milk, 1);

            // Act
            bool result = customer.CreateOrder(order);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(customer.Orders.Contains(order));
        }

        [TestMethod]
        public void TestMethod_CreateWrongOrder()
        {
            // Arrange
            Customer customer = new Customer("Mike", 530.90);

            // Act
            bool result = customer.CreateOrder(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestMethod_FindOrder_ExistingOrder()
        {
            // Arrange
            Customer customer = new Customer("Mike", 530.90);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Order order = new Order(customer);
            order.AddProduct(milk, 1);

            // Act
            _ = customer.CreateOrder(order);
            Order result = customer.FindOrder(1);

            // Assert
            Assert.AreEqual(result, order);
        }

        [TestMethod]
        public void TestMethod_FindOrder_NonexistingOrder()
        {
            // Arrange
            Customer customer = new Customer("Mike", 530.90);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Order order = new Order(customer);
            order.AddProduct(milk, 1);

            // Act
            _ = customer.CreateOrder(order);
            Order result = customer.FindOrder(3);

            // Assert
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void TestMethod_BuyOrder_EnoughMoney()
        {
            // Arrange
            Customer customer = new Customer("Mike", 530.90);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Order order = new Order(customer);
            order.AddProduct(milk, 1);
            customer.CreateOrder(order);

            // Act
            bool result = customer.BuyOrder(order);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(518, 7, customer.Balance);
        }

        [TestMethod]
        public void TestMethod_BuyOrder_NotEnoughMoney()
        {
            // Arrange
            Customer customer = new Customer("Mike", 10);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Order order = new Order(customer);
            order.AddProduct(milk, 1);
            customer.CreateOrder(order);

            // Act
            bool result = customer.BuyOrder(order);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(10, customer.Balance);
        }

        [TestMethod]
        public void TestMethod_DeleteOrder_WithExistingOrder()
        {
            // Arrange
            Customer customer = new Customer("Mike", 530.30);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Order order = new Order(customer);
            order.AddProduct(milk, 1);
            customer.CreateOrder(order);

            // Act
            bool result = customer.DeleteOrder(order);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(customer.Orders.Contains(order));
        }

        [TestMethod]
        public void TestMethod_DeleteOrder_WithNonExistingOrder()
        {
            // Arrange
            Customer customer = new Customer("Mike", 530.30);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Order order = new Order(customer);
            order.AddProduct(milk, 1);

            // Act
            bool result = customer.DeleteOrder(order);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestMethod_GetInfo()
        {
            // Arrange
            var customer = new Customer("Mike", 530.30);
            string str = "------------------------------------" +
                         "\nCustomer: Mike" +
                         "\nBalance: 530.30$" +
                         "\n------------------------------------" +
                         "\n" +
                         "\nORDER: 1" +
                         "\n   Date: 10.12.2010" +
                         "\n   Status: INPROGRESS" +
                         "\n------------------------------------" +
                         "\nProducts:" +
                         "\n   Milk (DAIRY):   12,20$" +
                         "\n   Cake (BAKERY):   20,25$" +
                         "\n   Water (BEVERAGES):   7,5$" +
                         "\n------------------------------------" +
                         "\nTotal price:    39,95$" +
                         "\n------------------------------------";

            // Act
            string info = customer.GetInfo();

            // Assert
            Assert.AreEqual(str, info);
        }
    }
}