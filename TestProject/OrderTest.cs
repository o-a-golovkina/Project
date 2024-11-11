using Project;

namespace TestProject
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void TestMethod_GetTotalPrice()
        {
            //Arrange
            Customer cus = new Customer("Mike", 250.50);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Product cake = new Product("Cake", 20.25, (ProductType)2);
            double expectedResult = 12.2 * 3 + 20.25;

            //Act
            Order order = new Order(cus);
            order.AddProduct(milk, 3);
            order.AddProduct(cake, 1);
            double actualResult = order.GetTotalPrice();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestMethod_AddProduct()
        {
            //Arrange
            Customer cus = new Customer("Mike", 250.50);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Product cake = new Product("Cake", 20.25, (ProductType)2);
            List<Product> expectedResult = new List<Product>
            {
                milk, milk, milk, cake
            };

            //Act
            Order order = new Order(cus);
            order.AddProduct(milk, 3);
            order.AddProduct(cake, 1);
            List<Product> actualResult = order.Products;

            //Assert
            Assert.AreEqual(expectedResult[0], actualResult[0]);
            Assert.AreEqual(expectedResult[1], actualResult[1]);
            Assert.AreEqual(expectedResult[2], actualResult[2]);
            Assert.AreEqual(expectedResult[3], actualResult[3]);
            Assert.AreEqual(actualResult[0], actualResult[1]);
        }

        [TestMethod]
        public void TestMethod_RemoveProduct()
        {
            //Arrange
            Customer cus = new Customer("Mike", 250.50);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Product cake = new Product("Cake", 20.25, (ProductType)2);
            Product pork = new Product("Pork", 18.32, (ProductType)3);

            //Act
            Order order = new Order(cus);
            order.AddProduct(milk, 3);
            order.AddProduct(cake, 1);

            //Assert
            Assert.IsTrue(order.RemoveProduct(cake));
            Assert.IsFalse(order.RemoveProduct(pork));
            Assert.IsFalse(order.Products.Contains(cake));
        }

        [TestMethod]
        public void TestMethod_Clear()
        {
            //Arrange
            Customer cus = new Customer("Mike", 250.50);
            Product milk = new Product("Milk", 12.2, (ProductType)1);
            Product cake = new Product("Cake", 20.25, (ProductType)2);

            //Act
            Order order = new Order(cus);
            order.AddProduct(milk, 1);
            order.AddProduct(cake, 1);
            order.Clear();

            //Assert
            Assert.IsFalse(order.Products.Contains(milk));
            Assert.IsFalse(order.Products.Contains(cake));
            Assert.AreEqual(0, order.Products.Count);
        }
    }
}