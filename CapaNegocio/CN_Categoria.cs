using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Categoria
    {
         private CD_Categoria categoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return categoria.Listar();
        }
        
        public int Registrar(Categoria objeto, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(objeto.Descripcion) || string.IsNullOrWhiteSpace(objeto.Descripcion))
            {
                Mensaje = "La descripcion de la categoria no puede ser vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return categoria.Registrar(objeto, out Mensaje);
            }
            else
            {
                return 0;
            }
            
        }

        public bool editar(Categoria objeto, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(objeto.Descripcion) || string.IsNullOrWhiteSpace(objeto.Descripcion))
            {
                mensaje = "La categoria no puede ser vacio";

            }
            

            if (string.IsNullOrEmpty(mensaje))
            {
                return categoria.Editar(objeto, out mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string mensaje)
        {
            return categoria.Eliminar(id, out mensaje);
        }

    }
}
