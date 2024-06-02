using EShopping.API.DataTransferObjects.Requests;

using FluentValidation;

namespace EShopping.API.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDTO>
    {
        public UpdateProductValidator()
        {
            RuleFor(product => product.Name).NotNull().NotEmpty();
            RuleFor(product => product.Price).NotNull().GreaterThan(0);
        }
    }
}
