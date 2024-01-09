using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.Api.models
{
    public class Product
    {
        public Guid Id { get; set;}
        public required string Name { get; set;}
        public required double Price { get; set;}
    }
}