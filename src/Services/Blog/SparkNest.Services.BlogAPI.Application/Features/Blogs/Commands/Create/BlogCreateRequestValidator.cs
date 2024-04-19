using FluentValidation;

namespace SparkNest.Services.BlogAPI.Application.Features.Blogs.Commands.Create
{
    public class BlogCreateRequestValidator : AbstractValidator<BlogCreateRequest>
    {
        public BlogCreateRequestValidator()
        {
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull();
        }
    }
}
