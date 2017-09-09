using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API_Video_Imagen.Data.Interface;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Video_Imagen.Controllers
{
     

    [Route("api/[controller]")]
    public class MediaImagenController : Controller
    {
        private readonly IProcesoImagenes _IProcesoImagenes;

        public MediaImagenController(IProcesoImagenes _IProcesoImagenes) {
            this._IProcesoImagenes = _IProcesoImagenes;
        }
        // GET: /<controller>/
        //[HttpGet]
        //public IActionResult GetImg() {
        //    var listaImagens = _IProcesoImagenes.ListaImagen();
        //    byte[] image = null;
        //    foreach (var img in listaImagens)
        //    {
        //     image = System.IO.File.ReadAllBytes(img.Ruta);
        //        return File(image, "image/jpeg");
        //    }
        //    return File(image, "image/jpeg");
        //}

        //[HttpPost]
     
        public IActionResult GetImg(string Name)
        {
            byte[] image = System.IO.File.ReadAllBytes(@"F:\Learn MVC\API_Video_Imagen\src\API_Video_Imagen\wwwroot\Imagen\error.JPG"); 

            if (Name != null)
            {
                var listaImagens = _IProcesoImagenes.ListaImagen(Name);
            
                if (listaImagens.rutafoto != null && listaImagens.idfoto!=0)
                {

                    image = System.IO.File.ReadAllBytes(listaImagens.rutafoto);
                    return File(image, "image/jpeg");
                }
                else
                {
                    return File(image, "image/jpeg");
                }

            }
            else { return File(image, "image/jpeg"); }


        }


        [HttpPost]
        public IActionResult GetImgpost(string Name)
        {
            byte[] image = System.IO.File.ReadAllBytes(@"F:\Learn MVC\API_Video_Imagen\src\API_Video_Imagen\wwwroot\Imagen\error.JPG");

            if (Name != null)
            {
                var listaImagens = _IProcesoImagenes.ListaImagen(Name);

                if (listaImagens.rutafoto != null)
                {

                    image = System.IO.File.ReadAllBytes(listaImagens.rutafoto);
                    return File(image, "image/jpeg");
                }
                else
                {
                    return File(image, "image/jpeg");
                }

            }
            else { return File(image, "image/jpeg"); }


        }
    }
}
