using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApp.Api.controller;
using ProductApp.Api.dto;
using ProductApp.Api.services;
using ProductApp.UnitTest.Fixture;

namespace ProductApp.UnitTest.Controller
{
    public class ProductsControllerTest
    {
       private Mock<IProductService> _productService;
       public ProductsControllerTest()
       {
        _productService = new Mock<IProductService>();

       }
        
        [Fact]
        public async void AllProduct_ReturnOkResult()
        {
            // arrange
            _productService.Setup(t => t.GetAllProducts()).ReturnsAsync(ProductFixture.getAllProducts());
             var controller = new ProductController(_productService.Object);
            // act
            var response = await controller.AllProduct();
            var okResponse = response as OkObjectResult;
            // assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200,okResponse!.StatusCode);
            
            
        }
        [Fact]
        public async void AllProduct_ShouldReturn2Results()
        {
            // arrane
             var _productService = new Mock<IProductService>();
            _productService.Setup(t => t.GetAllProducts()).ReturnsAsync(ProductFixture.getAllProducts());
             var controller = new ProductController(_productService.Object);
            // act
            var response = await controller.AllProduct() as OkObjectResult;
            var responseResult = response!.Value;
            var responseValue = responseResult as List<ProductDto>;
            // assert
            
            Assert.Equal(2,responseValue!.Count);
            Assert.NotNull(responseResult);
            Assert.IsType<List<ProductDto>>(responseResult);
        }


    }
}