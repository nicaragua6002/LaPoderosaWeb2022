using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LaPoderosaWeb.Models;

namespace LaPoderosaWeb
{
    /// <summary>
    /// Descripción breve de LaPoderosaWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class LaPoderosaWS : System.Web.Services.WebService
    {
        //Instanciamos el acceso a los datos
        LaPoderosaModelContainer db = new LaPoderosaModelContainer();

        //Lista de categorias
        [WebMethod]
        public List<CategoriaSW> ListarCategorias()
        {
            return db.Categorias.Select(x=> new CategoriaSW() { 
                Id=x.Id
                ,Nombre= x.Nombre
                ,CantProd=x.Productos.Count
            }).ToList();
        }

        //Lista de Proveedores
        [WebMethod]
        public List<ProveedorSW> ListarProvedores()
        {
            return db.Proveedores.Select(x => new ProveedorSW()
            {
                Id = x.Id
                ,
                Nombre = x.Nombre
                ,
                CantProd = x.Productos.Count
            }).ToList();
        }

        //Lista de TODOS los productos
        [WebMethod]
        public List<ProductoSW> TodoProductos()
        {
            return db.Productos.Select(x => new ProductoSW()
            {
                Id = x.Id,
                NombreProducto = x.NombreProducto,
                CantidadPorUnidad = x.CantidadPorUnidad,
                PrecioUnidad=x.PrecioUnidad,
                UnidadesEnExistencia=x.UnidadesEnExistencia,
                CategoriaId=x.CategoriaId,
                ProveedorId=x.ProveedorId
            }).ToList();
        }

        //Listar productos por categoria
        [WebMethod]
        public List<ProductoSW> ProductosByCategoria(int idCategoria)
        {
            return db.Productos.Where(x=>x.CategoriaId== idCategoria).Select(x => new ProductoSW()
            {
                Id = x.Id,
                NombreProducto = x.NombreProducto,
                CantidadPorUnidad = x.CantidadPorUnidad,
                PrecioUnidad = x.PrecioUnidad,
                UnidadesEnExistencia = x.UnidadesEnExistencia,
                CategoriaId = x.CategoriaId,
                ProveedorId = x.ProveedorId
            }).ToList();
        }

        //Listar productos por proveedor
        [WebMethod]
        public List<ProductoSW> ProductosByProveedor(int idProveedor)
        {
            return db.Productos.Where(x => x.ProveedorId == idProveedor).Select(x => new ProductoSW()
            {
                Id = x.Id,
                NombreProducto = x.NombreProducto,
                CantidadPorUnidad = x.CantidadPorUnidad,
                PrecioUnidad = x.PrecioUnidad,
                UnidadesEnExistencia = x.UnidadesEnExistencia,
                CategoriaId = x.CategoriaId,
                ProveedorId = x.ProveedorId
            }).ToList();
        }

        //Buscar productos por nombre
        [WebMethod]
        public List<ProductoSW> ProductosByName(string nombre)
        {
            return db.Productos.Where(x => x.NombreProducto.Contains(nombre)).Select(x => new ProductoSW()
            {
                Id = x.Id,
                NombreProducto = x.NombreProducto,
                CantidadPorUnidad = x.CantidadPorUnidad,
                PrecioUnidad = x.PrecioUnidad,
                UnidadesEnExistencia = x.UnidadesEnExistencia,
                CategoriaId = x.CategoriaId,
                ProveedorId = x.ProveedorId
            }).ToList();
        }

        //Burcar productos por Id
        [WebMethod]
        public ProductoSW ProductosById(int id)
        {
            return db.Productos.Where(x=>x.Id==id).Select(x => new ProductoSW()
            {
                Id = x.Id,
                NombreProducto = x.NombreProducto,
                CantidadPorUnidad = x.CantidadPorUnidad,
                PrecioUnidad = x.PrecioUnidad,
                UnidadesEnExistencia = x.UnidadesEnExistencia,
                CategoriaId = x.CategoriaId,
                ProveedorId = x.ProveedorId
            }).FirstOrDefault();
        }
        //Clases publicas

        public class CategoriaSW {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int CantProd { get; set; }
        }
        public class ProveedorSW {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int CantProd { get; set; }
        }
        public class ProductoSW {
            public int Id { get; set; }
            public string NombreProducto { get; set; }
            public string CantidadPorUnidad { get; set; }
            public double PrecioUnidad { get; set; }
            public short UnidadesEnExistencia { get; set; }
            public int CategoriaId { get; set; }
            public int ProveedorId { get; set; }

        }

    }
}
