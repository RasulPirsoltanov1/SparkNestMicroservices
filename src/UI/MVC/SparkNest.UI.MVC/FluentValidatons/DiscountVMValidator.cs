using FluentValidation;
using SparkNest.UI.MVC.Models.Discount;

namespace SparkNest.UI.MVC.Validators
{
    public class DiscountVMValidator : AbstractValidator<DiscountVM>
    {
        public DiscountVMValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull();
        }
    }
}
