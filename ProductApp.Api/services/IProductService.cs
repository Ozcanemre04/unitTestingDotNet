using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Api.dto;

namespace ProductApp.Api.services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> GetProductById(Guid id);

        Task<ProductDto> CreateProduct(AddProductDto addProductDto);

        Task<string> DeleteProduct(Guid id);

        Task<ProductDto> UpdateProduct(Guid id ,UpdateProductDto updateProductDto);
    }
}