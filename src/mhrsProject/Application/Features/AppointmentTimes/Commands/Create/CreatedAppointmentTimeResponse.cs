using Core.Application.Responses;

namespace Application.Features.AppointmentTimes.Commands.Create;

public class CreatedAppointmentTimeResponse : IResponse
{
    public Guid Id { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
}
