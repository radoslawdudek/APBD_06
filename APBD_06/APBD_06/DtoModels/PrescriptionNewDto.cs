namespace APBD_06.DtoModels;

public class PrescriptionNewDto
{
    public PatientDto Patient { get; set; }
    public List<MedicamentPatientDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}