using API_Video_Imagen.Model;
using PI_Video_Imagen.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API_Video_Imagen.Data.Interface
{
    public interface IProcesoImagenes
    {
        IEnumerable<BEFotos> ListaImagen();
        BEFotos ListaImagen(string NombreImagen);
        bool ValidaFotoProducto(Int64 idfoto, Int64 idproducto);

    }
}
