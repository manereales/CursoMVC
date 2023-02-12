using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Productos
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    //string query = "select IdProducto, Nombre, descripcion, IdMarca, IdCategoria, Precio, Stock, Activo from Producto";


                    StringBuilder builder = new StringBuilder();

                    builder.AppendLine("select p.IdProducto, p.Nombre, p.descripcion,");
                    builder.AppendLine("m.IdMarca, m.Descripcion[DesMarca],");
                    builder.AppendLine("c.IdCategoria, c.Descripcion[DesCategoria],");
                    builder.AppendLine("p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo");
                    builder.AppendLine("from Producto p");
                    builder.AppendLine("inner join Marca m on m.IdMarca = p.IdMarca");
                    builder.AppendLine("inner join Categoria c on c.IdCategoria = p.IdCategoria");


                    SqlCommand cmd = new SqlCommand(builder.ToString(), con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion =  dr["DesMarca"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-ES")),
                                Stock = Convert.ToInt32(dr["Stock"]),                               
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }




                }
            }
            catch (Exception)
            {

                lista = new List<Producto>();
            }

            return lista;
        }


        public int RegistrarProducto(Producto producto, out string mensaje)
        {
            int id = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", con);
                    {
                        cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("descripcion", producto.descripcion);
                        cmd.Parameters.AddWithValue("IdMarca", producto.oMarca.IdMarca);
                        cmd.Parameters.AddWithValue("IdCategoria", producto.oCategoria.IdCategoria);
                        cmd.Parameters.AddWithValue("Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("Activo", producto.Activo);

                        cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        id = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                        mensaje = cmd.Parameters["Mensaje"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                id = 0;
                mensaje = ex.Message;
            }

            return id;
        }


        public bool EditarProducto(Producto producto, out string mensaje)
        {
            var resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", con);

                    cmd.Parameters.AddWithValue("IdProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("descripcion", producto.descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", producto.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", producto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("Activo", producto.Activo);

                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    resultado =  Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].ToString();

                }
            }
            catch (Exception ex)
            {

                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }


        public bool GuardarDatosImagen(Producto producto, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    string query = "update Producto set RutaImagen = @RutaImagen, NombreImagen = @NombreImagen where IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@RutaImagen", producto.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", producto.NombreImagen);
                 

                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    if (cmd.ExecuteNonQuery()>0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        mensaje = "no se pudo actualizar imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }



        public bool EliminarProducto(int id, out string mensaje)
        {
            var resultado = false;
            mensaje = string.Empty;

            try
            {

                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", con);

                    cmd.Parameters.AddWithValue("IdProducto", id);

                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].ToString();

                }
            }
            catch (Exception ex)
            {

                resultado = false;
                mensaje = ex.Message;
            }

            return resultado;
        }

    }
}
