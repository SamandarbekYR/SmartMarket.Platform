using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarket.Service.Interfaces.Worker.Workers;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers
{
    [Route("api/super-admin/worker")]
    [ApiController]
    public class WorkerController(IWorkerService service) : SuperAdminController
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
