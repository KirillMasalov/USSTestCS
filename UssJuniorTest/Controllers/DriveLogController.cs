using Microsoft.AspNetCore.Mvc;
using UssJuniorTest.Core.DTO;
using UssJuniorTest.Infrastructure.Services.DrivesLogsAggregationService;

namespace UssJuniorTest.Controllers;

[Route("api/driveLog")]
public class DriveLogController : ControllerBase
{
    private DrivesLogsAggregationService _logAggregationService;

    public DriveLogController(DrivesLogsAggregationService logsAggregationService)
    {
        _logAggregationService = logsAggregationService;
    }


    [HttpGet("")]
    public async Task<IActionResult> GetDriveLogsAggregation(DrivesLogsGetModel inputModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var resultData = await _logAggregationService.GetDataFromQuery(new DrivesLogsQueryParameters(inputModel));

        if (resultData.Count() == 0)
            return NoContent();

        return Ok(resultData);
    }
}