using FluentValidation;
using Posthuman.Core.Models.DTO;
using System;

namespace Posthuman.Core.Models.Validators
{
    public class TodoItemDTOValidator : AbstractValidator<TodoItemDTO>
    {
        public TodoItemDTOValidator() 
        {
            RuleFor(ti => ti.Title)
                .NotEmpty();

            RuleFor(ti => ti.Deadline)
                .GreaterThanOrEqualTo(DateTime.Now);
        }
    }
}
