using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hospitals.Commands.Delete;
public class DeleteHospitalCommandValidator : AbstractValidator<DeleteHospitalCommand>
{
    public DeleteHospitalCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
