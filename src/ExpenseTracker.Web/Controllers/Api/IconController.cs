using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Web.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExpenseTracker.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class IconController : ControllerBase
    {
        private readonly Icons icons;
        public IconController(Icons icons)
        {
            this.icons = icons;
        }
        public IActionResult Index()
        {
            return Ok(new
            {
                data = icons.Items
            });
        }
    }
}