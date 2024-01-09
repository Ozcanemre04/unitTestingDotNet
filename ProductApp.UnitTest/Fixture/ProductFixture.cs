using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Api.dto;

namespace ProductApp.UnitTest.Fixture
{
    public class ProductFixture
    {
        internal static IEnumerable<ProductDto> getAllProducts()
        {
           return new List<ProductDto>
             {
                new ProductDto {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "iphone",
                    Price = 159.56
                },
                new ProductDto {
                    Id = new Guid("ab2bd134-98ca-4fe3-a80a-53ea0cd9c200"),
                    Name = "samsung",
                    Price = 235.43
                }
             };
        }
    }
}