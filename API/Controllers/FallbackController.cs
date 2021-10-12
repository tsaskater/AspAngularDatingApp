using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace API.Controllers
{
  public class FallbackController : Controller
  {
    private readonly IHostEnvironment _env;
    public FallbackController(IHostEnvironment env)
    {
      _env = env;
    }
    public ActionResult Index()
    {
      return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
        "wwwroot", "index.html"), "text/HTML");
    }
  }
}