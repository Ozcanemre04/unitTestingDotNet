using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApp.Api.controller;
using ProductApp.Api.dto;
using ProductApp.Api.services;

namespace ProductApp.UnitTest.Controller
{
    public class UpdateProductControllerTest
    {
        [Fact]
        public async void UpdateProduct_okResult_and_returnResult()
        {
            // arrange
            var productId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var updateProductDto = new UpdateProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = productId
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.UpdateProduct(productId, updateProductDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);

            // act
            var response = await controller.UpdateProduct(productId, updateProductDto);
            var okResponse = response as OkObjectResult;
            var responseResult = okResponse!.Value;
            var responseValue = responseResult as ProductDto;
            // asset
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResponse.StatusCode);
            Assert.IsType<ProductDto>(responseResult);
            Assert.NotNull(responseResult);
            Assert.Equal("Iphone", responseValue!.Name);
            Assert.Equal(34.45, responseValue.Price);
            Assert.Equal(productId, responseValue.Id);
        }
        [Fact]
        public async void UpdateProduct_NameNull_okResult_and_returnResult()
        {
            // arrange
            var productId = new Guid();
            var updateProductDto = new UpdateProductDto()
            {
                Price = 34.45,
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = productId
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.UpdateProduct(productId, updateProductDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);

            // act
            var response = await controller.UpdateProduct(productId, updateProductDto);
            var okResponse = response as OkObjectResult;
            var responseResult = okResponse!.Value;
            var responseValue = responseResult as ProductDto;
            // asset
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResponse.StatusCode);
            Assert.IsType<ProductDto>(responseResult);
            Assert.NotNull(responseResult);
            Assert.Equal("Iphone", responseValue!.Name);
            Assert.Equal(34.45, responseValue.Price);
            Assert.Equal(productId, responseValue.Id);
        }
        [Fact]
        public async void UpdateProduct_PriceNull_okResult_and_returnResult()
        {
            // arrange
            var productId = new Guid();
            var updateProductDto = new UpdateProductDto()
            {
                Name = "Iphone",
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = productId
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.UpdateProduct(productId, updateProductDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.UpdateProduct(productId, updateProductDto);
            var okResponse = response as OkObjectResult;
            var responseResult = okResponse!.Value;
            var responseValue = responseResult as ProductDto;
            // asset
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResponse.StatusCode);
            Assert.IsType<ProductDto>(responseResult);
            Assert.NotNull(responseResult);
            Assert.Equal("Iphone", responseValue!.Name);
            Assert.Equal(34.45, responseValue.Price);
            Assert.Equal(productId, responseValue.Id);
        }
        [Fact]
        public async void UpdateProduct_ReturnNameLengthValidationError_And_BadRequest()
        {
            // arrange
            var productId = new Guid();
            var updateProductDto = new UpdateProductDto()
            {
                Name = "Ip",
                Price = 34.45,
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = productId
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.UpdateProduct(productId, updateProductDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.UpdateProduct(productId, updateProductDto);
            var badRequestResponse = response as BadRequestObjectResult;
            var responseResult = badRequestResponse!.Value;
            var responseValue = responseResult as List<ValidationFailure>;
            // asset
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestResponse.StatusCode);
            Assert.IsType<List<ValidationFailure>>(responseResult);
            Assert.Contains("length must be between 3 and 40", responseValue!.Select(t => t.ErrorMessage));

        }
        [Fact]
        public async void UpdateProduct_ReturnPriceMustBeDoubleValidationError_And_BadRequest()
        {
            // arrange
            var productId = new Guid();
            var updateProductDto = new UpdateProductDto()
            {
                Name = "Iphone",
                Price = 34,
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = productId
            };
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.UpdateProduct(productId, updateProductDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.UpdateProduct(productId, updateProductDto);
            var badRequestResponse = response as BadRequestObjectResult;
            var responseResult = badRequestResponse!.Value;
            var responseValue = responseResult as List<ValidationFailure>;
            // asset
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestResponse.StatusCode);
            Assert.IsType<List<ValidationFailure>>(responseResult);
            Assert.Contains("Price must be a double with a decimal point.", responseValue!.Select(t => t.ErrorMessage));

        }
        [Fact]
        public async void UpdateProduct_ShouldReturnNotFound()
        {
            // arrange
            var productId = new Guid();
            var updateProductDto = new UpdateProductDto()
            {
                Name = "Iphone",
                Price = 34.98,
            };

            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(s => s.UpdateProduct(productId, updateProductDto)).ThrowsAsync(new Exception("Product is not found"));
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.UpdateProduct(productId, updateProductDto);
            var notFoundResponse = response as NotFoundObjectResult;
            // asset
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, notFoundResponse!.StatusCode);
            Assert.Equal("Product is not found", notFoundResponse.Value);
        }
    }
}