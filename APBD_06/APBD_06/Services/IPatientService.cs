using Microsoft.AspNetCore.Mvc;

namespace APBD_06.Services;

public interface IPatientService
{
    Task<IActionResult> GetDataAboutPatient(int idPatient);
}