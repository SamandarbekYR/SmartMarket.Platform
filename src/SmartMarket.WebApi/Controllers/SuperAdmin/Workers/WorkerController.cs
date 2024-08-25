using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Workers;
using SmartMarket.Service.Interfaces.Workers;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController(IWorkerService service) : ControllerBase
    {
        private readonly IWorkerService _workerService = service;
        [HttpPost]
        public async Task<IActionResult> Add(AddWorkerDto workerDto)
        {
            await _workerService.AddAsync(workerDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok( await _workerService.GetAllAsync());

    }
}
