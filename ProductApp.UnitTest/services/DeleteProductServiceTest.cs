using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using ProductApp.Api.data;
using ProductApp.Api.models;
using ProductApp.Api.services;
using ProductApp.UnitTest.Fixture;

namespace ProductApp.UnitTest.services
{
    public class DeleteProductServiceTest
    {
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ApplicationDbContext> _context;
        private readonly Guid id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200") ;
        public DeleteProductServiceTest()
        {
            _mapper = new Mock<IMapper>();
            _context = new Mock<ApplicationDbContext>();
        }

        [Fact]
        public async Task DeleteProduct_OnSuccess()
        {
            // arrange
            _context.Setup(c=>c.Products.FindAsync(id)).ReturnsAsync(DataFixture.getAllProducts().First(e=>e.Id ==id));
            var service = new ProductService(_context.Object,_mapper.Object);
            // act
            var result = await service.DeleteProduct(id);
            // assert
            Assert.Equal("Product is deleted",result);
        }
        [Fact]
        public async Task DeleteProduct_OnFail_WithNotFoundException()
        {
            // arrange
            _context.Setup(c=>c.Products.FindAsync(Guid.NewGuid())).ReturnsAsync((Product)null);
            var service = new ProductService(_context.Object,_mapper.Object);
            // act and assert
            var exeception = await Assert.ThrowsAsync<Exception>(async ()=> await service.DeleteProduct(Guid.NewGuid()));
            Assert.Equal("Product is not found",exeception.Message);
        }
    }
}