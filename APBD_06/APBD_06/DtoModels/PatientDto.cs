namespace APBD_06.DtoModels;

public class PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionDto> Prescriptions { get; set; }
    public int IdDoctor { get; set; }
}