using Core.Application.Dtos;

namespace Application.Features.AppointmentTimes.Queries.GetList;

public class GetListAppointmentTimeListItemDto : IDto
{
    public Guid Id { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
}