﻿
using API_Video_Imagen.Model;
using API_Video_Imagen.Model.Entity;
using PI_Video_Imagen.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API_Video_Imagen.Data.Interface
{
     public interface IFotos
    {
        PictureViewModels Insert_BEFoto(BEFotos  BEImagen , BEProducto BEProducto);
    }
}
