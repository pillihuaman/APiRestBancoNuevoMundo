﻿
using API_Video_Imagen.Data.Interface;
using API_Video_Imagen.Model.Entity;
using PI_Video_Imagen.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace API_Video_Imagen.Data.Repository
{
    public class FotosRepositorio : IFotos
    {
        public PictureViewModels Insert_BEFoto(BEFotos BEImagen , BEProducto BEProducto)
        {

            PictureViewModels Cantidadregistros = new PictureViewModels();
            int cantidadregistro = 0;
            using (var conexion = new SqlConnection())
            {
                API_Video_Imagen.Data.Repository.Conecta Conecta = new API_Video_Imagen.Data.Repository.Conecta();
                string Conexion = Conecta.GetConexion().ConnectionString;
                         
                try
                {

                    var Parameter = new SqlParameter[13];
                    Parameter[0] = new SqlParameter("@Nombre", SqlDbType.VarChar, 300) { Value = BEImagen.Nombre };
                    Parameter[1] = new SqlParameter("@rutafoto", SqlDbType.VarChar, 300) { Value = ""};
                    Parameter[2] = new SqlParameter("@posicionPortada", SqlDbType.Int) { Value = BEImagen.posicionPortada };
                    Parameter[3] = new SqlParameter("@descripcion", SqlDbType.VarChar, 500) { Value = BEImagen.descripcion };
                    Parameter[4] = new SqlParameter("@NombreProducto", SqlDbType.VarChar, 500) { Value = BEProducto.Nombre };
                    Parameter[5] = new SqlParameter("@DescripcionProducto", SqlDbType.VarChar, 600) { Value = BEProducto.Descripcion };
                    Parameter[6] = new SqlParameter("@Precio", SqlDbType.Decimal) { Value = BEProducto.Precio };
                    Parameter[7] = new SqlParameter("@Stock", SqlDbType.Int) { Value = BEProducto.Stock };
                    Parameter[8] = new SqlParameter("@fechaproduccion", SqlDbType.DateTime) { Value = BEProducto.fechaproduccion };
                    Parameter[9] = new SqlParameter("@fechaexpiracion", SqlDbType.DateTime) { Value = BEProducto.fechaexpiracion };
                    Parameter[10] = new SqlParameter("@Error", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    Parameter[11] = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };
                    Parameter[12] = new SqlParameter("@CantidadRegistrado ", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    using (var read = PI_Video_Imagen.Data.SqlHelper.ExecuteReader(Conexion, CommandType.StoredProcedure, "dbo.Insert_Foto", Parameter))
                    {
                        while (read.Read())
                        {
                            Cantidadregistros.idfoto =Int64.Parse(read.GetInt64(read.GetOrdinal("IdFoto")).ToString());
                            Cantidadregistros.CantidadDeregistros = Int32.Parse(read.GetInt32(read.GetOrdinal("CantidadRegistrado")).ToString());
                            Cantidadregistros.idproducto = Int64.Parse(read.GetInt64(read.GetOrdinal("IdFoto")).ToString());


                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

    


            }

                return Cantidadregistros;
  
        }
    }
}
