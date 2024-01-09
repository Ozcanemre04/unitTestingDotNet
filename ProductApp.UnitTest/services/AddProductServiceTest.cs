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
    public class AddProductServiceTest
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ApplicationDbContext> _context;
        public AddProductServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _context = new Mock<ApplicationDbContext>();
        }
        [Fact]
        public async Task CreateProduct()
        {
            // arrange
            var addProductDto = new AddProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
            };
            var product = new Product
            {
                Name = "Iphone",
                Price = 34.45,

            };
            var productDto = new ProductDto
            {
                
                Id = Guid.NewGuid(),
                Name = "Iphone",
                Price = 34.45,
                
            };
            _mapper.Setup(m => m.Map<Product>(addProductDto)).Returns(product);
            _context.Setup(c => c.Products.Add(product));
            _context.Setup(c => c.SaveChanges()).Returns(1);
            _mapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);
            var service = new ProductService(_context.Object, _mapper.Object);


            // act
            var result = await service.CreateProduct(addProductDto);
            // assert
            Assert.NotNull(result);
            Assert.IsType<ProductDto>(result);
            Assert.Equal("Iphone",result.Name);
            Assert.Equal(34.45,result.Price);
            
        }
    }
}