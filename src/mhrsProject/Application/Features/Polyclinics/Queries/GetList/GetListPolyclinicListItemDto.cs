using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Polyclinics.Queries.GetList;
public class GetListPolyclinicListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
