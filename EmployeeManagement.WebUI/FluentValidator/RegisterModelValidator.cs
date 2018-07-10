using EmployeeManagement.WebUI.Models;
using FluentValidation;

namespace EmployeeManagement.WebUI.FluentValidator
{
    public class RegisterModelValidator : AbstractValidator<EmployeeViewModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.FirstName)
                   .NotEmpty().WithMessage("FirstName is Required").Length(0, 50); 
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is Required").Length(0, 50);
            RuleFor(x => x.Id).Null();
        }
    }
}