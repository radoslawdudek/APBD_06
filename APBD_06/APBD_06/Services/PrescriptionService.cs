using APBD_06.Context;
using APBD_06.DtoModels;
using APBD_06.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APBD_06.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly ApbdContext _apbdContext;

        public PrescriptionService(ApbdContext apbdContext)
        {
            _apbdContext = apbdContext;
        }

        public async Task<IActionResult> AddNewPrescription([FromBody] PrescriptionNewDto prescriptionNewDto)
        {
            if (prescriptionNewDto.DueDate < prescriptionNewDto.Date)
                return new BadRequestObjectResult("DueDate must be greater than or equal to Date");

            if (prescriptionNewDto.Medicaments.Count > 10)
                return new BadRequestObjectResult("A prescription can include a maximum of 10 medicaments");

            var patient = await _apbdContext.Patients
                .FirstOrDefaultAsync(p => p.IdPatient == prescriptionNewDto.Patient.IdPatient);

            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = prescriptionNewDto.Patient.FirstName,
                    LastName = prescriptionNewDto.Patient.LastName,
                    Birthdate = prescriptionNewDto.Patient.Birthdate
                };
                _apbdContext.Patients.Add(patient);
                await _apbdContext.SaveChangesAsync();
            }

            var prescription = new Prescription
            {
                Date = prescriptionNewDto.Date,
                DueDate = prescriptionNewDto.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = prescriptionNewDto.Patient.IdDoctor
            };

            foreach (var med in prescriptionNewDto.Medicaments)
            {
                var medicament = await _apbdContext.Medicaments
                    .FirstOrDefaultAsync(m => m.IdMedicament == med.IdMedicament);

                if (medicament == null)
                    return new BadRequestObjectResult($"Medicament with Id {med.IdMedicament} does not exist");

                var prescriptionMedicament = new PrescriptionMedicament
                {
                    IdMedicament = med.IdMedicament,
                    Medicament = medicament,
                    Dose = med.Dose,
                    Details = med.Description
                };
                prescription.PrescriptionMedicaments.Add(prescriptionMedicament);
            }

            _apbdContext.Prescriptions.Add(prescription);
            await _apbdContext.SaveChangesAsync();

            return new OkObjectResult("Prescription added successfully");
        }
    }
}
