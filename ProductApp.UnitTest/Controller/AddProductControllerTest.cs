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
    public class AddProductControllerTest
    {
        [Fact]
        public async void AddProduct_ReturnOkResult()
        {
            // arrange
            var serviceMock = new Mock<IProductService>();
            var addProductDto = new AddProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = new Guid()
            };
            serviceMock.Setup(t => t.CreateProduct(addProductDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.AddProduct(addProductDto);
            var okResponse = response as OkObjectResult;
            var responseResult = okResponse!.Value;
            var responseValue = responseResult as ProductDto;
            // assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResponse.StatusCode);

        }

        [Fact]
        public async void AddProduct_Return1Result()
        {
            // arrange
            var serviceMock = new Mock<IProductService>();
            var addProductDto = new AddProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = new Guid()
            };
            serviceMock.Setup(t => t.CreateProduct(addProductDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.AddProduct(addProductDto) as OkObjectResult;
            var responseResult = response!.Value;
            var responseValue = responseResult as ProductDto;
            // assert
            Assert.IsType<ProductDto>(responseResult);
            Assert.NotNull(responseResult);
            Assert.Equal("Iphone", responseValue!.Name);
            Assert.Equal(34.45, responseValue.Price);
            Assert.Equal(new Guid(), responseValue.Id);
        }

        [Fact]
        public async void AddProduct_ReturnNameNotNullValidationError_And_BadRequest()
        {
            // arrange
            var serviceMock = new Mock<IProductService>();
            var invalidDto = new AddProductDto()
            {
                Price = 34.45
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = new Guid()
            };
            serviceMock.Setup(t => t.CreateProduct(invalidDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.AddProduct(invalidDto);
            var badRequestObjectResponse = response as BadRequestObjectResult;
            var responseResult = badRequestObjectResponse!.Value;
            var responseValue = responseResult as List<ValidationFailure>;
            // assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestObjectResponse.StatusCode);
            Assert.IsType<List<ValidationFailure>>(responseResult);
            Assert.Contains("name can not be null", responseValue!.Select(t => t.ErrorMessage));
            //or
            //Assert.Equal("name can not be null",responseValue.Select(t=>t.ErrorMessage).First());

        }

        [Fact]
        public async void AddProduct_ReturnNameLengthValidationError_And_BadRequest()
        {
            // arrange
            var serviceMock = new Mock<IProductService>();
            var invalidDto = new AddProductDto()
            {
                Name = "lo",
                Price = 34.45
            };
            var productDto = new ProductDto()
            {
                Name = "Ip",
                Price = 34.45,
                Id = new Guid()
            };
            serviceMock.Setup(t => t.CreateProduct(invalidDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.AddProduct(invalidDto);
            var badRequestObjectResponse = response as BadRequestObjectResult;
            var responseResult = badRequestObjectResponse!.Value;
            var responseValue = responseResult as List<ValidationFailure>;
            // assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestObjectResponse.StatusCode);
            Assert.IsType<List<ValidationFailure>>(responseResult);
            Assert.Contains("length must be between 3 and 40", responseValue!.Select(t => t.ErrorMessage));
            //or
            //Assert.Equal("length must be between 3 and 40",responseValue.Select(t=>t.ErrorMessage).First());  
        }

        [Fact]
        public async void AddProduct_ReturnPriceNotNullValidationError_And_BadRequest()
        {
            // arrange
            var serviceMock = new Mock<IProductService>();
            var invalidDto = new AddProductDto()
            {
                Name = "Iphone"
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = new Guid()
            };
            serviceMock.Setup(t => t.CreateProduct(invalidDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.AddProduct(invalidDto);
            var badRequestObjectResponse = response as BadRequestObjectResult;
            var responseResult = badRequestObjectResponse!.Value;
            var responseValue = responseResult as List<ValidationFailure>;
            // assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestObjectResponse.StatusCode);
            Assert.IsType<List<ValidationFailure>>(responseResult);
            Assert.Contains("Price can not be null", responseValue!.Select(t => t.ErrorMessage));

        }

        [Fact]
        public async void AddProduct_ReturnPriceMustbeDoubleValidationError_And_BadRequest()
        {
            // arrange
            var serviceMock = new Mock<IProductService>();
            var invalidDto = new AddProductDto()
            {
                Name = "Iphone",
                Price = 900
            };
            var productDto = new ProductDto()
            {
                Name = "Iphone",
                Price = 34.45,
                Id = new Guid()
            };
            serviceMock.Setup(t => t.CreateProduct(invalidDto)).ReturnsAsync(productDto);
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.AddProduct(invalidDto);
            var badRequestObjectResponse = response as BadRequestObjectResult;
            var responseResult = badRequestObjectResponse!.Value;
            var responseValue = responseResult as List<ValidationFailure>;
            // assert
            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(400, badRequestObjectResponse.StatusCode);
            Assert.IsType<List<ValidationFailure>>(responseResult);
            Assert.Contains("Price must be a double with a decimal point.", responseValue!.Select(t => t.ErrorMessage));

        }
    }
}