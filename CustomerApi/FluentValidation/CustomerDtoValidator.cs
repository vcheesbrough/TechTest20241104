using FluentValidation;
using Shared.ApiModels;

namespace CustomerApi.FluentValidation;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public const string DefaultRuleSet = "default";
    public const string CreateRuleSet = "create";
    public const string UpdateRuleSet = "update";

    public CustomerDtoValidator()
    {
        RuleSet(DefaultRuleSet, () =>
        {
            RuleFor(customer => customer.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(customer => customer.Name)
                .NotEmpty()
                .MinimumLength(5);
        });
        RuleSet(CreateRuleSet, () =>
        {
            RuleFor(customer => customer.Id)
                .Empty();
        });
        RuleSet(UpdateRuleSet, () =>
        {
            RuleFor(customer => customer.Id)
                .NotEmpty();
        });
    }
}