using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PrescriptionDetails.Commands.Update;


public class UpdatePrescriptionDetailCommandValidator : AbstractValidator<UpdatePrescriptionDetailCommand>
{
    public UpdatePrescriptionDetailCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.PrescriptionId).NotEmpty();
        RuleFor(c => c.MedicineId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Dose).NotEmpty();
        RuleFor(c => c.Period).NotEmpty();
        RuleFor(c => c.UsageType).NotEmpty();
        RuleFor(c => c.UsageCount).NotEmpty();
    }
}
