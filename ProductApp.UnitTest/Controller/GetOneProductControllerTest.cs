using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApp.Api;
using ProductApp.Api.controller;
using ProductApp.Api.data;
using ProductApp.Api.dto;
using ProductApp.Api.models;
using ProductApp.Api.services;
using ProductApp.UnitTest.Fixture;

namespace ProductApp.UnitTest.Controller
{
    public class GetOneProductControllerTest
    {
        private Mock<IProductService> _productService;
        private readonly Guid productId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
        private readonly ProductDto _fixture;
        public GetOneProductControllerTest()
        {
            _productService = new Mock<IProductService>();
            _fixture = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == productId)!;
        }
        [Fact]
        public async void GetOneProduct_ReturnOkResult()
        {
            // arrange
            _productService.Setup(t => t.GetProductById(productId)).ReturnsAsync(_fixture);
            var controller = new ProductController(_productService.Object);
            // act
            var response = await controller.GetOneProduct(productId);
            var okResponse = response as OkObjectResult;
            // assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResponse!.StatusCode);


        }
        [Fact]
        public async void GetOneProduct_ShouldReturn1Result()
        {
            // arrange
            _productService.Setup(t => t.GetProductById(productId)).ReturnsAsync(_fixture);
            var controller = new ProductController(_productService.Object);
            // act
            var response = await controller.GetOneProduct(productId) as OkObjectResult;
            var responseResult = response!.Value;
            var responseValue = responseResult as ProductDto;
            // assert
            Assert.NotNull(responseResult);
            Assert.IsType<ProductDto>(responseResult);
            Assert.Equal(ProductFixture.getAllProducts().First().Id, responseValue!.Id);
            Assert.Equal("iphone", responseValue.Name);
            Assert.Equal(159.56, responseValue.Price);
        }
        [Fact]
        public async void GetOneProduct_ReturnNotFoundResult()
        {
            // arrange
            var invalidId = Guid.NewGuid();
            _productService.Setup(t => t.GetProductById(invalidId)).ThrowsAsync(new Exception("Product is not found"));
            var controller = new ProductController(_productService.Object);
            // act
            var response = await controller.GetOneProduct(invalidId);
            var NotFoundResponse = response as NotFoundObjectResult;
            // assert
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, NotFoundResponse!.StatusCode);
            Assert.Equal("Product is not found", NotFoundResponse.Value);

        }
    }
}