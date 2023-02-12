using CapaEntidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Marca
    {
        public List<Marca> Listar()
        {
            var lista = new List<Marca>();

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdMarca, Descripcion, Activo from Marca";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Marca()
                            {
                                IdMarca = Convert.ToInt32(dr["IdMarca"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }

                }
            }
            catch (Exception)
            {

               lista = new List<Marca>();
            }

            return lista;
        }

     
        public int Registrar(Marca marca, out string mensaje)
        {
            var idmarca = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarMarca", con);
                    {
                        cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
                        cmd.Parameters.AddWithValue("Activo", marca.Activo);
                        cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();

                    idmarca = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].ToString();
                }
            }
            catch (Exception ex)
            {
                idmarca = 0;
                mensaje = ex.Message;
            }

            return idmarca;

        }


        public bool Editar(Marca marca, out string mensaje)
        {
            var resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarMarca", con);
                    
                        cmd.Parameters.AddWithValue("IdMarca", marca.IdMarca);
                        cmd.Parameters.AddWithValue("Descripcion", marca.Descripcion);
                        cmd.Parameters.AddWithValue("Activo", marca.Activo);

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
          

        public bool Eliminar(int id, out string mensaje)
        {
            var resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection con = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarMarca", con);
                    
                        cmd.Parameters.AddWithValue("IdMarca", id);

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

                resultado=false;
                mensaje = ex.Message;
            }

            return resultado;
        }
        
    }
}
