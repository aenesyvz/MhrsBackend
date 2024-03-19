using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MedicineCompanies.Queries.GetList;
public class GetListMedicineCompanyListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string TaxOffice { get; set; }
    public string TaxNumber { get; set; }
    public string ImageUrl { get; set; }
}
