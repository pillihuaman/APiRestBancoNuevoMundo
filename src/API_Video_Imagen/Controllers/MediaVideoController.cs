using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Video_Imagen.Controllers
{
    [Route("api/[controller]")]
    public class MediaVideoController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult GetVideo()
        {

            byte[] Video = System.IO.File.ReadAllBytes(@"F:\Learn MVC\API_Video_Imagen\src\API_Video_Imagen\wwwroot\Video\Acabdenacer.mp4");
            return File(Video, "video/mp4");
        }
    }
}
