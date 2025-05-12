using Template.Models;

namespace Template.Services;

public interface IDbService
{
    Task<CustomDto> GetAppointmentsAsync(int patientId);
    Task AddNewSmth(int customerId, RequestDTO requestDto);
}