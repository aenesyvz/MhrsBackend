using Core.Application.Responses;

namespace Application.Features.AppointmentTimes.Commands.Delete;

public class DeletedAppointmentTimeResponse : IResponse
{
    public Guid Id { get; set; }
}
