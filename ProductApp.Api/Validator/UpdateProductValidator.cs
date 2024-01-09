using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using ProductApp.Api.dto;

namespace ProductApp.Api.Validator
{
    public class UpdateProductValidator:AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
           RuleFor(u=>u.Name)
            .Length(3,40).WithMessage("length must be between 3 and 40");

           RuleFor(x => x.Price)
            .Must(BeDoubleWithDecimal).WithMessage("Price must be a double with a decimal point.");
            

        }
       private bool BeDoubleWithDecimal(double? value)
    {
        if(value != 0)
           return value % 1 !=0;

        return true;
          
    }
    }
}