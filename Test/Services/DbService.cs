using System.Data;
using Microsoft.Data.SqlClient;
using Template.Exceptions;
using Template.Models;

namespace Template.Services;

public class DbService : IDbService
{
    
    private readonly string _connectionString;
    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
    }
    public async Task<CustomDto> GetAppointmentsAsync(int patientId)
    {
        var query = @"SELECT A.date, P.first_name, P.last_name, P.date_of_birth, D.doctor_id, D.PWZ, S2.name, S.service_fee FROM Appointment A
JOIN Patient P on A.patient_id = P.patient_id
JOIN Doctor D on D.doctor_id = A.doctor_id
JOIN Appointment_Service S on A.appointment_id = S.appointment_id
JOIN Service S2 on S2.service_id = S.service_id
WHERE A.patient_id = @PatientId";
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        await connection.OpenAsync();

        command.Parameters.AddWithValue("@PatientId", patientId);
        var reader = await command.ExecuteReaderAsync();
        
        CustomDto? customDto = null;

        while (await reader.ReadAsync())
        {
            if (customDto is null)
            {
                customDto = new CustomDto()
                {
                    Date = reader.GetDateTime(0),
                    Services = new List<ServiceDetailsDto>()
                };
            }

            if (customDto.Patient is null)
            {
                customDto.Patient = new PatientDetailsDto()
                {
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    DateOfBirth = reader.GetDateTime(3)
                };
            }
            if (customDto.Doctor is null)
            {
                customDto.Doctor = new DoctorDetailsDto()
                {
                    DoctorId = reader.GetInt32(4),
                    Pwz = reader.GetString(5)
                };
            }
            string serviceName = reader.GetString(6);
            var service = customDto.Services.FirstOrDefault(e => e.Name.Equals(serviceName));

            if (service is null)
            {
                service = new ServiceDetailsDto()
                {
                    Name = serviceName,
                    ServiceFee = reader.GetInt32(7)
                };
                customDto.Services.Add(service);
            }
        }

        if (customDto is null)
        {
            throw new NotFoundEx("No custom appointments found");
        }
        return customDto;
    }

    public async Task AddNewSmth(int customerId, RequestDTO requestDto)
    {
        
    }
}