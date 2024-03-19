using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Diseases.Commands.Create;
public class CreatedDiseaseResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid PolyclinicId { get; set; }
    public string Name { get; set; }
}
