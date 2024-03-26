﻿using FluentValidation;
using SparkNest.UI.MVC.Models.Product;

namespace SparkNest.UI.MVC.Validators
{
    public class ProductUpdateVMValidator : AbstractValidator<ProductUpdateVM>
    {
        public ProductUpdateVMValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Feature.Color).NotEmpty().NotNull();
            RuleFor(x => x.Price).NotEmpty().NotNull().ScalePrecision(2, 9);
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
        }
    }
}
