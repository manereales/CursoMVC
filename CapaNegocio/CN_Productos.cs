using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Productos
    {
        CD_Productos objProductos = new CD_Productos();

        public List<Producto> ListaProductos()
        {
            return objProductos.ListarProductos();
        }

        public int RegistrarProducto(Producto producto, out string mensaje)
        {
            mensaje = string.Empty;

            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
            {
                mensaje = "el nombre del producto no debe estar vacio";
            }
            else if (string.IsNullOrEmpty(producto.descripcion) || string.IsNullOrWhiteSpace(producto.descripcion))
            {
                mensaje = "la descripcion del producto no debe estar vacio";
            }
            else if (producto.oMarca.IdMarca == 0)
            {
                mensaje = "debe seleccionar una marca";
            }
            else if (producto.oCategoria.IdCategoria == 0)
            {
                mensaje = "debe seleccionar una categoria";
            }
            else if (producto.Precio == 0)
            {
                mensaje = "Debe ingresar el precio del producto";
            }
            else if (producto.Stock == 0)
            {
                mensaje = "Debe ingresar el stock del producto";
            }

            if (string.IsNullOrEmpty(mensaje))
            {
                return objProductos.RegistrarProducto(producto, out mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool EditarProducto(Producto producto, out string mensaje)
        {
            mensaje=string.Empty;

            if (string.IsNullOrEmpty(producto.Nombre) || string.IsNullOrWhiteSpace(producto.Nombre))
            {
                mensaje = "el nombre del producto no debe estar vacio";
            }
            else if (string.IsNullOrEmpty(producto.descripcion) || string.IsNullOrWhiteSpace(producto.descripcion))
            {
                mensaje = "la descripcion del producto no debe estar vacio";
            }
            else if (producto.oMarca.IdMarca == 0)
            {
                mensaje = "debe seleccionar una marca";
            }
            else if (producto.oCategoria.IdCategoria == 0)
            {
                mensaje = "debe seleccionar una categoria";
            }
            else if (producto.Precio == 0)
            {
                mensaje = "Debe ingresar el precio del producto";
            }
            else if (producto.Stock == 0)
            {
                mensaje = "Debe ingresar el stock del producto";
            }


            if (string.IsNullOrEmpty(mensaje))
            {
                return objProductos.EditarProducto(producto, out mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Producto producto, out string mensaje)
        {
            return objProductos.GuardarDatosImagen(producto, out mensaje);
        }

        public bool EliminarProducto(int id, out string mensaje)
        {
            
            return objProductos.EliminarProducto(id, out mensaje);
        }

    }
}
