using FluentValidation;
using Posthuman.Core.Models.DTO;
using Posthuman.Core.Repositories;

namespace Posthuman.Core.Models.Validators
{
    /// <summary>
    /// Registers rules for validating input for user registration
    /// TODO: move to different project
    /// </summary>
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator(IUsersRepository usersRepository) 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password);

            // TODO - it's probably not good to use usersRepositories here... 
            // And its async method... Maybe whole method should be async?
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var isEmailInUse = usersRepository.GetByEmail(value).Result;
                    if (isEmailInUse != null) 
                        context.AddFailure("Email", "Email is already in use");
                });
        }
    }
    
}
