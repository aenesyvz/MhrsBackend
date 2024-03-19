﻿using FluentValidation;

namespace Application.Features.Districts.Commands.Create;

public class CreateDistrictCommandValidator : AbstractValidator<CreateDistrictCommand>
{
    public CreateDistrictCommandValidator()
    {
        RuleFor(c => c.CityId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}
