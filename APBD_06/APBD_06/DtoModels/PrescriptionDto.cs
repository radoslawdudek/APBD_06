namespace APBD_06.DtoModels;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
   
}