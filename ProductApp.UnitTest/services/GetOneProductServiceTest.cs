using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using ProductApp.Api.data;
using ProductApp.Api.dto;
using ProductApp.Api.models;
using ProductApp.Api.services;
using ProductApp.UnitTest.Fixture;
using Xunit.Abstractions;

namespace ProductApp.UnitTest.services
{
    public class GetOneProductServiceTest
    {

        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ApplicationDbContext> _context;
        private readonly Guid id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
        private readonly Product findProduct;
        private readonly ProductDto findProductDto;

        public GetOneProductServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _context = new Mock<ApplicationDbContext>();
            findProduct = DataFixture.getAllProducts().FirstOrDefault(e => e.Id == id)!;
            findProductDto = ProductFixture.getAllProducts().FirstOrDefault(e => e.Id == id)!;
        }

        [Fact]
        public async Task GetProductById_onSuccess_shouldReturn1Value()
        {
            // arrange
            _context.Setup(c => c.Products.FindAsync(id)).ReturnsAsync(findProduct);
            _mapper.Setup(m => m.Map<ProductDto>(findProduct)).Returns(findProductDto);
            var service = new ProductService(_context.Object, _mapper.Object);
            // act
            var result = await service.GetProductById(id);
            // assert
            Assert.NotNull(result);
            Assert.IsType<ProductDto>(result);
            Assert.Equal("iphone", result.Name);
            Assert.Equal(159.56, result.Price);


        }
        [Fact]
        public async Task GetProductById_onfail_shouldReturnException()
        {
            // arrange 
            _context.Setup(c => c.Products.FindAsync(Guid.NewGuid())).ReturnsAsync((Product)null);
            var service = new ProductService(_context.Object, _mapper.Object);
            // act and assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await service.GetProductById(Guid.NewGuid()));
            Assert.Equal("Product is not found", exception.Message);
        }
    }
}