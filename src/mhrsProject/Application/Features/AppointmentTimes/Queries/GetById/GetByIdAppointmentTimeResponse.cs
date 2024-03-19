using Core.Application.Responses;

namespace Application.Features.AppointmentTimes.Queries.GetById;

public class GetByIdAppointmentTimeResponse : IResponse
{
    public Guid Id { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
}
