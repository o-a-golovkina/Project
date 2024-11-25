using Project;

namespace TestProject
{

    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        [DataRow(null, 0)]
        [DataRow("", 530.90)]
        [DataRow(" ", 530.90)]
        [DataRow("M", 530.90)]
        [DataRow("Mike", -530.90)]
        public void TestMethod_CreateWrongCustomer(string name, double balance)
        {
            //Arrange
            Customer customer;

            //Act + Assert
            Assert.ThrowsException<FormatException>(() => customer = new Customer(name, balance));
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
            bool result = customer.CreateOrder(null!);

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
            order.Number = 1;
            order.AddProduct(milk, 1);

            // Act
            _ = customer.CreateOrder(order);
            Order result = customer.FindOrder(1);

            // Assert
            Assert.AreEqual(order.Number, result.Number);
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
            Assert.AreEqual(null, result);
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
            string s = "white";

            // Act
            bool result = customer.BuyOrder(order, ref s);

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
            string s = "white";

            // Act
            bool result = customer.BuyOrder(order, ref s);

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
            Product milk = new Product("Milk", 12.2, ProductType.DAIRY);
            Product pork = new Product("Pork", 22.3, ProductType.MEAT);
            Order order = new Order(customer);
            order.Number = 1;
            order.AddProduct(milk, 1);
            order.AddProduct(pork, 1);
            customer.CreateOrder(order);
            string str = " Name: Mike\t Balance: 530,30$\t Card: WHITE (9%)\n" +
                         "┌───────────┬────────┬────────┬───────────────────────────────┬─────────┬─────────┐\n" +
                         "│ Date      │ Number │ Status │ Products                      │ Price   │ Total   │\n" +
                         "├───────────┼────────┼────────┼───────────────────────────────┼─────────┼─────────┤\n" +
                        $"│{order.Date:yyyy-MM-dd} │001     │NEW     │Milk(DAIRY) ‒ 12,20$           │34,50$   │31,39$   │\n" +
                         "│           │        │        │Pork(MEAT) ‒ 22,30$            │         │         │\n" +
                         "├───────────┼────────┼────────┼───────────────────────────────┼─────────┼─────────┤\n" +
                         "└───────────┴────────┴────────┴───────────────────────────────┴─────────┴─────────┘\n";

            // Act
            string info = customer.GetInfo();

            // Assert
            Assert.AreEqual(str, info);
        }
    }
}