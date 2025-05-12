namespace Template.Models;

public class CustomDto
{
    
    public DateTime Date { get; set; }
    public PatientDetailsDto? Patient { get; set; } = null;
    public DoctorDetailsDto? Doctor { get; set; } = null;
    public List<ServiceDetailsDto> Services { get; set; } = [];

}

public class PatientDetailsDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}

public class DoctorDetailsDto
{
   public int DoctorId { get; set; }
    public string Pwz { get; set; } = string.Empty;
}

public class ServiceDetailsDto
{
    public string Name { get; set; } = string.Empty;
    public decimal ServiceFee { get; set; }
}