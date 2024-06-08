using APBD_06.Context;
using APBD_06.DtoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_06.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApbdContext _apbdContext;

        public PatientService(ApbdContext apbdContext)
        {
            _apbdContext = apbdContext;
        }

        public async Task<IActionResult> GetDataAboutPatient(int idPatient)
        {
            var patient = await _apbdContext.Patients
                .Where(p => p.IdPatient == idPatient)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync();

            if (patient == null)
                return new NotFoundResult();

            var patientDto = new PatientDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Prescriptions = patient.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName
                        },
                        Medicaments = pr.PrescriptionMedicaments
                            .Select(pm => new MedicamentDto
                            {
                                IdMedicament = pm.Medicament.IdMedicament,
                                Name = pm.Medicament.Name,
                                Dose = pm.Dose,
                                Description = pm.Details
                            }).ToList()
                    }).ToList()
            };

            return new OkObjectResult(patientDto);
        }
    }
}
