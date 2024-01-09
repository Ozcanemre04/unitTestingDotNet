using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Api.dto;
using ProductApp.Api.services;
using ProductApp.Api.Validator;

namespace ProductApp.Api.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> AllProduct()
        {
            try
            {
               var result = await _productService.GetAllProducts();
               return Ok(result);
                
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetOneProduct([FromRoute] Guid id)
        {
            try
            {
               var result = await _productService.GetProductById(id);
               return Ok(result);
                
            }
            catch (Exception ex)
            {
                if(ex.Message =="Product is not found")
                   return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
        {
            try
            {
               var result = await _productService.DeleteProduct(id);
               return Ok(result);
                
            }
            catch (Exception ex)
            {
                if(ex.Message =="Product is not found")
                   return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
        {
            try
            {
               var validator = new AddProductValidator();
               var validationResult = await validator.ValidateAsync(addProductDto);
               if(!validationResult.IsValid)
                  return BadRequest(validationResult.Errors);
               var result = await _productService.CreateProduct(addProductDto);
               return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id,UpdateProductDto updateProductDto)
        {
            try
            {
               var validator = new UpdateProductValidator();
               var validationResult = await validator.ValidateAsync(updateProductDto);
               if(!validationResult.IsValid)
                  return BadRequest(validationResult.Errors);
                var result = await _productService.UpdateProduct(id,updateProductDto);
                return Ok(result);

            }
            catch (Exception ex)
            {
                
                if(ex.Message =="Product is not found")
                   return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }
}
}