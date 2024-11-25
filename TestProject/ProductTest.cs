using Project;

namespace TestProject
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        [DataRow(null, 0, (ProductType)1)]
        [DataRow("", 12.2, (ProductType)1)]
        [DataRow("M", 12.2, (ProductType)1)]
        [DataRow("Milk", -12.2, (ProductType)1)]
        [DataRow("Milk", 0, (ProductType)1)]
        public void TestMethod_CreateWrongProduct(string name, double price, ProductType type)
        {
            //Arrange
            Product product;

            //Act + Assert
            Assert.ThrowsException<FormatException>(() => product = new Product(name, price, type));
        }

        [TestMethod]
        public void TestMethod_CreateCorrectProduct()
        {
            //Arrange
            string name = "Milk";
            double price = 12.2;
            ProductType type = (ProductType)1;
            Product expectedResult = new("Milk", 12.2, (ProductType)1);

            //Act
            Product actualResult = new(name, price, type);

            //Assert
            Assert.AreEqual(expectedResult.Name, actualResult.Name);
            Assert.AreEqual(expectedResult.Price, actualResult.Price);
            Assert.AreEqual(expectedResult.Type, actualResult.Type);
        }
    }
}