using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using API_Video_Imagen.Data.Interface;
using API_Video_Imagen.Model.Entity;
using PI_Video_Imagen.Data.Base;

namespace API_Video_Imagen.Controllers
{
    public class FotosController : Controller
    {
        // GET: /<controller>/
        private IHostingEnvironment _IEnviroment;
        private IFotos _IFotos;
        public FotosController(IHostingEnvironment _IEnviroment, IFotos _IFotos)
        {
            this._IEnviroment = _IEnviroment;
            this._IFotos = _IFotos;
        }
        [HttpGet]
        public IActionResult AgregarNuevaFoto()

        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AgregarNuevaFoto(
            IFormFile Files, PictureViewModels PictureViewModels)
        {
            string NombreImagenInicial = string.Empty;
            string NombreImagen = string.Empty;
            if (Files.Name.Length >= 5)
            {
                if (Files.Name.Contains(".jpg") || Files.Name.Contains(".JPEG") || Files.Name.Contains(".gif") || Files.Name.Contains(".png") || Files.Name.Contains(".tif"))
                {
                    NombreImagenInicial = Files.Name.Replace(" ", "");
               

            ///////
       
            var Insert_Fotos = new PictureViewModels();
      
            if (PictureViewModels!= null)
            {
                var GetValue = new BEFotos();
                var GetValuep = new BEProducto();
                GetValue.descripcion = PictureViewModels.descripcionImagen;
                GetValue.Nombre= PictureViewModels.NombreImagen;
                GetValue.posicionPortada = PictureViewModels.posicionPortada;
                GetValuep.Descripcion = PictureViewModels.DescripcionProducto;
                GetValuep.fechaexpiracion = PictureViewModels.fechaexpiracion;
                GetValuep.fechaproduccion = PictureViewModels.fechaproduccion;
                GetValuep.Nombre = PictureViewModels.NombreProducto;
                GetValuep.Precio = PictureViewModels.Precio;
                GetValuep.Stock= PictureViewModels.Stock;
                var Gui = new Guid();
                NombreImagen = PictureViewModels.NombreImagen.Replace(" ", "") + Gui.ToString();

                 Insert_Fotos = _IFotos.Insert_BEFoto(GetValue, GetValuep);
            }


            // solo guarda si se completo el insert de las entidades
            var PathToSave = Path.Combine(_IEnviroment.WebRootPath, "Imagen");
            if (Files.Length > 0)
            {
               
             

                if (Insert_Fotos.CantidadDeregistros > 0)
                {
                    using (var SaveFile = new FileStream(Path.Combine(PathToSave, NombreImagen+NombreImagenInicial), FileMode.Create))
                    {
                        await Files.CopyToAsync(SaveFile);
                    }
                }


            }
                }

            }

            /////////////////
            //var file = Request.;
            return View();
        }

    }
}