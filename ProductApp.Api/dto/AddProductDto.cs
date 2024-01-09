using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Api.dto
{
    public class AddProductDto
    {
        public string? Name { get; set;}
        public double? Price { get; set;}  
    }
}