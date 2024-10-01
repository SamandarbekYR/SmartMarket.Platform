using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartMarket.WebApi.Controllers.Common
{
    
 //   [Authorize(Roles = "SuperAdmin, Admin, SimpleAdmin")]
    public class BaseController : ControllerBase
    { }
}
