using APBD_06.DtoModels;
using APBD_06.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_06.Services;

public interface IPrescriptionService
{
    Task<IActionResult> AddNewPrescription(PrescriptionNewDto prescriptionNewDto);
}