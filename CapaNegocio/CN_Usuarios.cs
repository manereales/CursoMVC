using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> listar()
        {
            return objCapaDato.Listar();
        }

        public int Registrar(Usuario usuario, out string mensaje)
        {




            mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El nombre del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El apellido del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El correo del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {



                string clave = CN_Recursos.GenerarClave();

                string asunto = "Creacion de Cuenta";

                string enviar_correo = "<h3>su cuenta fue creada correctamente</h3></br><p>Su contraseña para acceder es: !clave!</p>";

                enviar_correo = enviar_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(usuario.Correo, asunto, enviar_correo);

                if (respuesta == true)
                {
                    usuario.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(usuario, out mensaje);
                }
                else
                {
                    mensaje = "no se puede enviar el correo";
                    return 0;
                }

              

               
            }
            else
            {
                return 0;
            }
        }

        public bool editar(Usuario usuario, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El nombre del usuario no puede ser vacio";

            }else if (string.IsNullOrEmpty(usuario.Apellido) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El apellido del usuario no puede ser vacio";

            }else if (string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                mensaje = "El correo del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objCapaDato.Editar(usuario, out mensaje);
            }
            else
            {
                return false;
            }


           
        }


        public bool Eliminar(int id, out string mensaje)
        {
            return objCapaDato.Eliminar(id, out mensaje);
        }


    }
}
