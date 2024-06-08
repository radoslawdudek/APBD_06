using APBD_06.Models;
using APBD_06.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_06.Controllers;

[Route("api/patients")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetDataAboutPatient(int idPatient)
    {
        var result = await _patientService.GetDataAboutPatient(idPatient);
        return result;
    } 
}