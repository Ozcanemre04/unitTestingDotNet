
using ProductApp.Api.models;

namespace ProductApp.UnitTest.Fixture
{
    public class DataFixture
    {
        internal static IEnumerable<Product> getAllProducts()
        {
           return new List<Product>
             {
                new Product {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "iphone",
                    Price = 159.56
                },
                new Product {
                    Id = new Guid("ab2bd134-98ca-4fe3-a80a-53ea0cd9c200"),
                    Name = "samsung",
                    Price = 235.43
                }
             };
        }
    }
}