using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Marcas
    {
        CD_Marca objMarca = new CD_Marca();

        public List<Marca> lista()
        {
            return objMarca.Listar();
        }

        public int registrar(Marca marca, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                mensaje = "La descripcion no puede estar vacia";   
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objMarca.Registrar(marca, out mensaje);
            }
            else
            {
                return 0;
            }
            
        }

        public bool Editar(Marca marca, out string mensaje)
        {
            mensaje =string.Empty;

            if (string.IsNullOrEmpty(marca.Descripcion) || string.IsNullOrWhiteSpace(marca.Descripcion))
            {
                mensaje = "La descripcion no puede estar vacia";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objMarca.Editar(marca, out mensaje);
            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string mensaje)
        {
            mensaje=string.Empty;
            
            return objMarca.Eliminar(id, out mensaje);
        }

    }
}
