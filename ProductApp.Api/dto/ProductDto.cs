using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Api.dto
{
    public class ProductDto
    {
        public Guid Id { get; set;}
        public required string Name { get; set;}
        public required double Price { get; set;}    
    }
}