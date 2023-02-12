using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    public class MantenedorController : Controller
    {


        #region Categoria

        public ActionResult Categoria()
        {
            return View();
        }


        [HttpGet]
        public JsonResult listarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();

            oLista = new CN_Categoria().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarCategoria(Categoria objeto)
        {

            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Categoria().editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Marca
        //Marca

        public ActionResult Marca()
        {
            return View();
        }


        public JsonResult ListarMarcas()
        {
            List<Marca> lista = new List<Marca>();

            lista = new CN_Marcas().lista();

            return Json(new { data = lista } , JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarMarca(Marca marca)
        {
            object resultado;
            string mensaje = string.Empty;

            if (marca.IdMarca == 0)
            {
                resultado = new CN_Marcas().registrar(marca, out mensaje);
            }
            else
            {
                resultado = new CN_Marcas().Editar(marca, out mensaje);
            }

            return Json(new {resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarMarca(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;
            

            respuesta = new CN_Marcas().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Producto
        //Productos

        [HttpGet]
        public ActionResult Producto()
        {
            return View(); 
        }

        public JsonResult ListarProducto()
        {
            var lista = new List<Producto>();

            lista = new CN_Productos().ListaProductos();

            return Json( new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {
            CN_Productos productos = new CN_Productos();

            string mensaje = string.Empty;
            
            bool operacionExitosa = true;
            bool guardarImagenExito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);

            decimal Precio;

            if (decimal.TryParse(oProducto.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-Es"), out Precio))
            {
                oProducto.Precio = Precio;
            }
            else
            {
                return Json(new {operacionExitosa = false, mensaje = "El formato del precio debe ser ##,##"}, JsonRequestBehavior.AllowGet);
            }



            if (oProducto.IdProducto == 0)
            {
                int idProductoGenerado = productos.RegistrarProducto(oProducto, out mensaje);

                if (idProductoGenerado != 0)
                {
                    oProducto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacionExitosa = false;
                }

            }
            else
            {
                operacionExitosa = productos.EditarProducto(oProducto, out mensaje);
            }


            if (operacionExitosa == true)
            {
                if (archivoImagen != null)
                {
                    string ruta = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_Imagen = string.Concat(oProducto.IdProducto.ToString(), extension);

                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta, nombre_Imagen));
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        guardarImagenExito = false;
                    }

                    if (guardarImagenExito == true)
                    {
                        oProducto.RutaImagen = ruta;
                        oProducto.NombreImagen = nombre_Imagen;
                        bool rspta = new CN_Productos().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }

                }
            }

            return Json(new { operacionExitosa = operacionExitosa, idgenerado = oProducto.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oProducto = new CN_Productos().ListaProductos().Where(x => x.IdProducto == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);

            return Json(new { conversion = conversion, textoBase64 = textoBase64, extension = Path.GetExtension(oProducto.NombreImagen) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarProducto(int id)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            resultado = new CN_Productos().EliminarProducto(id, out mensaje);

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}