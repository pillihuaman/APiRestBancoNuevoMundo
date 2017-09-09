using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API_Video_Imagen.Data.Interface;
using System.IO;
using Newtonsoft.Json;
using API_Video_Imagen.Model.Entity;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Video_Imagen.Controllers
{


    [Route("api/[controller]")]
    public class CargarImagenController : Controller
    {
        private readonly IProcesoImagenes _IProcesoImagenes;
        private IHostingEnvironment _IEnviroment;
        public CargarImagenController(IProcesoImagenes _IProcesoImagenes, IHostingEnvironment _IEnviroment)
        {
            this._IProcesoImagenes = _IProcesoImagenes;
            this._IEnviroment = _IEnviroment;
        }


        public IActionResult GetImg(string Files)
        {
            byte[] image = System.IO.File.ReadAllBytes(@"F:\Learn MVC\API_Video_Imagen\src\API_Video_Imagen\wwwroot\Imagen\error.JPG");

            if (Files != null)
            {
                var listaImagens = _IProcesoImagenes.ListaImagen(Files);

                if (listaImagens.rutafoto != null && listaImagens.idfoto != 0)
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
        public JsonResult SaveImagen(string jsonProductoFoto)
        {


            if (Request.HasFormContentType)
            {
                var form = Request.Form;
                foreach (var formFile in form.Files)
                {
                    //PictureViewModels f = (PictureViewModels) JsonConvert.DeserializeObject(jsonProductoFoto);
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    PictureViewModels ConfigModelo = JsonConvert.DeserializeObject<PictureViewModels>(jsonProductoFoto, settings);
                    if (formFile != null)
                    {
                        if (ConfigModelo.idfoto != 0 && ConfigModelo.idproducto != 0 && ConfigModelo.CantidadDeregistros != 0)
                        {
                            var Valida = _IProcesoImagenes.ValidaFotoProducto(ConfigModelo.idfoto, ConfigModelo.idproducto);
                            // solo guarda si se completo el insert de las entidades
                            var PathToSave = Path.Combine(_IEnviroment.WebRootPath, "Imagen");
                            if (formFile.Length > 0)
                            {
                                if (Valida != false)
                                {
                                    using (var fileStream = new FileStream(Path.Combine(PathToSave, ConfigModelo.NombreImagen), FileMode.Create))
                                    {
                                        formFile.CopyTo(fileStream);
                                    }

                                }
                            }


                        }
                    }
                }


            }
            return Json(0);
        }



    }
}

