using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductApp.Api.data;
using ProductApp.Api.dto;
using ProductApp.Api.models;

namespace ProductApp.Api.services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public ProductService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<ProductDto> CreateProduct(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<string> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id)?? throw new Exception("Product is not found");
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return "Product is deleted";
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        { 
            var Products = await _context.Products.ToListAsync();
            var Dto = Products.Select(product=>_mapper.Map<ProductDto>(product));
            return Dto;
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var product = await _context.Products.FindAsync(id)?? throw new Exception("Product is not found");
            var dto = _mapper.Map<ProductDto>(product);
            return dto;
        }

        public async Task<ProductDto> UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(id)?? throw new Exception("Product is not found");
            
            if(updateProductDto.Price == 0){
               product.Price = product.Price;
               product.Name = updateProductDto.Name!;
               await _context.SaveChangesAsync();
               return _mapper.Map<ProductDto>(product);
               
            }
            
            _mapper.Map(updateProductDto,product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }
    }
}