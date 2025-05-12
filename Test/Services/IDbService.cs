using Template.Models;

namespace Template.Services;

public interface IDbService
{
    Task<CustomDTO> GetSmth(int customerId);
    Task AddNewSmth(int customerId, RequestDTO requestDto);
}