using BankApplication.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.Controllers
{
    [Route(ApiRoutes.V1)]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
