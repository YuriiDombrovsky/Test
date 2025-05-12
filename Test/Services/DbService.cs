using System.Data;
using Microsoft.Data.SqlClient;
using Template.Models;

namespace Template.Services;

public class DbService : IDbService
{
    
    private readonly string _connectionString;
    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
    }
    public async Task<CustomDTO> GetSmth(int customerId)
    {
        var query = "";
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        await connection.OpenAsync();

        command.Parameters.AddWithValue("@CustomerId", customerId);
        var reader = await command.ExecuteReaderAsync();
        
        CustomDTO? customDto = null;

        while (await reader.ReadAsync())
        {
            if (customDto is null)
            {
                customDto = new CustomDTO();
            }

            customDto.Name = reader.GetString(1);
        }
        return customDto;
    }

    public async Task AddNewSmth(int customerId, RequestDTO requestDto)
    {
        
    }
}