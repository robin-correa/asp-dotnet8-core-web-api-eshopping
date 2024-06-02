using EShopping.API.DataTransferObjects.Requests;

using FluentValidation;

namespace EShopping.API.Validators
{
    public class StoreProductValidator : AbstractValidator<StoreProductRequestDTO>
    {
        public StoreProductValidator()
        {
            RuleFor(product => product.Name).NotNull().NotEmpty();
            RuleFor(product => product.Price).NotNull().GreaterThan(0);
        }
    }
}
