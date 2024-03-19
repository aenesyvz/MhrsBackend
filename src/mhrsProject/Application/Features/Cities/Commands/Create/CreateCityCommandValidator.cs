﻿using FluentValidation;

namespace Application.Features.Cities.Commands.Create;

public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(c => c.PlateCode).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}
