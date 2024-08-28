using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Service.DTOs.Workers.WorkerRole;
using SmartMarket.Service.Interfaces.Worker.WorkerRole;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers
{
    [Route("api/super-admin/worker-role")]
    [ApiController]
    public class WorkerRoleController(IWorkerRoleService workerRole) : SuperAdminController
    {
        public IWorkerRoleService _workerRole = workerRole;

        [HttpPost]
        public async Task<IActionResult> Add(AddWorkerRoleDto workerRoleDto)
            => Ok(await _workerRole.AddAsync(workerRoleDto));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _workerRole.GetAllAsync());
    }
}
