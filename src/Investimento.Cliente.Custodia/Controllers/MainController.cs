using KissLog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Investimento.Cliente.Custodia.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected readonly ILogger _logger;
        protected MainController(ILogger logger)
        {
            _logger = logger;
        }

        protected IActionResult CustomResponse(string data)
        {
            return Content(data, "application/json");
        }
    }
}
