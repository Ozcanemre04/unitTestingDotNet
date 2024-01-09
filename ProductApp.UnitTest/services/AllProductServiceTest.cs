
using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using ProductApp.Api.data;
using ProductApp.Api.dto;
using ProductApp.Api.models;
using ProductApp.Api.services;
using ProductApp.UnitTest.Fixture;

namespace ProductApp.UnitTest.services
{
    public class AllProductServiceTest
    {
        [Fact]
        public async Task GetAllProducts_onSuccess_shouldReturn2Result()
        {
        
            // arrange 
            var mapper = new Mock<IMapper>();
            var context = new Mock<ApplicationDbContext>();
            context.Setup(c=>c.Products).ReturnsDbSet(DataFixture.getAllProducts());
            
            var service= new ProductService(context.Object,mapper.Object);
            // act
            var result = await service.GetAllProducts();
            
            // asssert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
            Assert.Equal(2,result.Count());  
        }
    }
}