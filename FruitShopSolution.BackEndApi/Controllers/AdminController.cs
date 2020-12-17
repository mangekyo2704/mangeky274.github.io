using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitShopSolution.Application.Catalog.Admin;
using FruitShopSolution.ViewModel.Catalog.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopSolution.BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        private bool checkLogin = false;
        [HttpPost("authenticate")]
        public IActionResult Accuracy([FromBody] AdminLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result =  _adminService.Accuracy(request);
            if (request == null)
                return BadRequest();
            return Ok();
        }
    }
}
