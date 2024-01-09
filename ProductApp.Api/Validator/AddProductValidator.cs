using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ProductApp.Api.dto;

namespace ProductApp.Api.Validator
{
    public class AddProductValidator: AbstractValidator<AddProductDto>
    {
        public AddProductValidator()
        {
            RuleFor(u=>u.Name)
            .Length(3,40).WithMessage("length must be between 3 and 40")
            .NotNull()
            .WithMessage("name can not be null");

            RuleFor(u=>u.Price)
             .NotNull()
             .WithMessage("Price can not be null")
             .Must(BeDoubleWithDecimal).WithMessage("Price must be a double with a decimal point.");
            
        }
        private bool BeDoubleWithDecimal(double? value)
        {
            Console.WriteLine(value%1 !=0);
            return value % 1 !=0;
          
        }
    }
}