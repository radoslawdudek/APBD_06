using APBD_06.DtoModels;
using APBD_06.Models;
using APBD_06.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_06.Controllers;

[Route("api/prescriptions")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPrescription([FromBody] PrescriptionNewDto prescriptionDto)
    {
        var result = await _prescriptionService.AddNewPrescription(prescriptionDto);
        return result;
    } 
    
}