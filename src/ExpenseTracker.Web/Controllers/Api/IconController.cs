using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExpenseTracker.Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class IconController : ControllerBase
    {
        public  IActionResult Index()
        {
            var rootPath = Directory.GetCurrentDirectory();
            var path = Path.Combine(rootPath, "IconsList.json");
            var icons = JsonConvert.DeserializeObject<List<string>>(System.IO.File.ReadAllText(path));
            return Ok(new {
                data = icons
            });
        }
    }
}