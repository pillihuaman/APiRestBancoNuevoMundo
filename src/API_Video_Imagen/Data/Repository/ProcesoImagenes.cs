using API_Video_Imagen.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Video_Imagen.Model;
using System.Data.SqlClient;
using System.Data;
using PI_Video_Imagen.Data.Base;

namespace API_Video_Imagen.Data.Repository
{
    public class ProcesoImagenes : IProcesoImagenes
    {
        public IEnumerable<BEFotos> ListaImagen()
        {

            Conecta Conecta = new Conecta();
             string Conexion=Conecta.GetConexion().ConnectionString;
            Queue<BEFotos> ListaFoto = new Queue<BEFotos>();
            try
            {

                var Parameter = new SqlParameter[3];
                Parameter[0] = new SqlParameter("@idfoto", SqlDbType.Int) { Value = 0 };
                Parameter[1] = new SqlParameter("@Error", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                Parameter[2] = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                using (var read = PI_Video_Imagen.Data.SqlHelper.ExecuteReader(Conexion, CommandType.StoredProcedure, "dbo.ListarRankingImagenes", Parameter))
                {

                    while (read.Read() && read.HasRows && !read.IsDBNull(read.GetOrdinal("hashCode")))
                    {
                        BEFotos Imagenobj = new BEFotos();
                        Imagenobj.idfoto =  read.GetInt64(read.GetOrdinal("idfoto"));
                        Imagenobj.Nombre = read.GetString(read.GetOrdinal("Nombre"));
                        Imagenobj.rutafoto = @"F:\Learn MVC\API_Video_Imagen\src\API_Video_Imagen\wwwroot\Imagen\in_vivo_izasascientific.jpg";
                        ListaFoto.Enqueue(Imagenobj);

                    }
                 
                }



            }

            catch (Exception ex)
            {
                throw ex;
            }

            return ListaFoto;

        }

        public BEFotos ListaImagen(string NombreImagen)
        {
            
            Conecta Conecta = new Conecta();
            string Conexion = Conecta.GetConexion().ConnectionString;
            BEFotos Foto = new BEFotos();
            bool Error = false;
            string Respuesta = string.Empty;
            try
            {

                var Parameter = new SqlParameter[3];
                Parameter[0] = new SqlParameter("@Nombre", SqlDbType.VarChar) { Value = NombreImagen.Trim() };
                Parameter[1] = new SqlParameter("@Error", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                Parameter[2] = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                using (var read = PI_Video_Imagen.Data.SqlHelper.ExecuteReader(Conexion, CommandType.StoredProcedure, "dbo.ListarRankingImagenesByName", Parameter))
                {
                    read.Read();
                    if (read.HasRows)
                    {

                        Foto.idfoto =   read.GetInt64(read.GetOrdinal("idfoto"));
                        Foto.Nombre = read.GetString(read.GetOrdinal("Nombre"));
                        if (Foto.Nombre != null && Foto.idfoto != 0)
                        {
                            Foto.rutafoto = Conecta.GetFilePath()  + Foto.Nombre.Trim();
                        }
                        Error= read.GetBoolean(read.GetOrdinal("Error"));
                        Respuesta = read.GetString(read.GetOrdinal("Respuesta"));

                    }
          
                   

                }



            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Foto;
        }

        public bool ValidaFotoProducto(long idfoto, long idproducto)
        {
            Conecta Conecta = new Conecta();
            string Conexion = Conecta.GetConexion().ConnectionString;
            BEFotos Foto = new BEFotos();
            bool Error = false;
            string Respuesta = string.Empty;
            try
            {

                var Parameter = new SqlParameter[4];
                Parameter[0] = new SqlParameter("@idfoto", SqlDbType.BigInt) { Value = idfoto };
                Parameter[1] = new SqlParameter("@idproducto", SqlDbType.BigInt) { Value = idproducto };
                Parameter[2] = new SqlParameter("@Error", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                Parameter[3] = new SqlParameter("@Respuesta", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                using (var read = PI_Video_Imagen.Data.SqlHelper.ExecuteReader(Conexion, CommandType.StoredProcedure, "dbo.Validar_insert_Foto", Parameter))
                {
                    read.Read();
                    if (read.HasRows)
                    {
                        Error = read.GetBoolean(read.GetOrdinal("Error"));
                        Respuesta = read.GetString(read.GetOrdinal("Respuesta"));
                        if (Error ==true)
                        {
                            Error = true;

                        }

                    }

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return Error;
        }
    }
}

