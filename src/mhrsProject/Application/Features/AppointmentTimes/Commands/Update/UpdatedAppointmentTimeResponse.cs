using Core.Application.Responses;

namespace Application.Features.AppointmentTimes.Commands.Update;

public class UpdatedAppointmentTimeResponse : IResponse
{
    public Guid Id { get; set; }
    public int Hour { get; set; }
    public int Minute { get; set; }
}
