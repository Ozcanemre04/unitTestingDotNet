using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    public class DeleteProductControlerTest
    {
        public DeleteProductControlerTest()
        {

        }
        [Fact]
        public async void DeleteProduct_ReturnOkResult()
        {
            // arrange
            var productId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(t => t.DeleteProduct(productId)).ReturnsAsync("Product is deleted");
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.DeleteProduct(productId);
            var okResponse = response as OkObjectResult;
            // assert
            Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResponse!.StatusCode);


        }
        [Fact]
        public async void DeleteProduct_ShouldReturnResult()
        {
            // arrange
            var productId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(t => t.DeleteProduct(productId)).ReturnsAsync("Product is deleted");
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.DeleteProduct(productId) as OkObjectResult;
            // assert
            Assert.NotNull(response!.Value);
            Assert.Equal("Product is deleted", response.Value);

        }
        [Fact]
        public async void DeleteProduct_ShouldReturnNotFoundResult()
        {
            // arrange
            var productId = Guid.NewGuid();
            var serviceMock = new Mock<IProductService>();
            serviceMock.Setup(t => t.DeleteProduct(productId)).ThrowsAsync(new Exception("Product is not found"));
            var controller = new ProductController(serviceMock.Object);
            // act
            var response = await controller.DeleteProduct(productId);
            var notfoundResponse = response as NotFoundObjectResult;
            // assert
            Assert.Equal(404, notfoundResponse!.StatusCode);
            Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal("Product is not found", notfoundResponse.Value);

        }
    }
}