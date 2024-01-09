using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ProductApp.Api.data;
using ProductApp.Api.dto;
using ProductApp.Api.models;
using ProductApp.Api.services;
using ProductApp.UnitTest.Fixture;

namespace ProductApp.UnitTest.services
{
    public class UpdateProductServiceTest
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ApplicationDbContext> _context;
        private readonly Guid id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
        public UpdateProductServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _context = new Mock<ApplicationDbContext>();
        }
        [Fact]
        public async Task UpdateProduct()
        {
            // arrange
            var updateProductDto = new UpdateProductDto
            {
                Name = "Samsung",
                Price = 999.99
            };
            var product = new Product
            {
                Id = id,
                Name = "Iphone",
                Price = 34.45,

            };

            var productDto = new ProductDto
            {

                Id = id,
                Name = "Samsung",
                Price = 999.99,

            };
            _context.Setup(c => c.Products.FindAsync(id)).ReturnsAsync(product);
            _mapper.Setup(p => p.Map<Product>(updateProductDto)).Returns(product);
            _mapper.Setup(p => p.Map<ProductDto>(product)).Returns(productDto);
            var service = new ProductService(_context.Object, _mapper.Object);
            // act

            var result = await service.UpdateProduct(id, updateProductDto);
            // assert
            Assert.NotNull(result);
            Assert.IsType<ProductDto>(result);
            Assert.Equal("Samsung", result.Name);
            Assert.Equal(999.99, result.Price);
        }

        [Fact]
        public async Task UpdateProduct_InvalidId_ThrowsException()
        {
            // Arrange
            var updateProductDto = new UpdateProductDto
            {
                Name = "Samsung",
                Price = 999.99
            };
            _context.Setup(c => c.Products.FindAsync(Guid.NewGuid())).ReturnsAsync((Product)null); // Simulate product not found
            var productService = new ProductService(_context.Object, _mapper.Object);
            // Act and Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await productService.UpdateProduct(id, updateProductDto));
            Assert.Equal("Product is not found", exception.Message);

        }
    }
}