﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaPoderosaWeb.Models;

namespace LaPoderosaWeb.Controllers
{
    [Authorize]
    public class ProductoesController : Controller
    {
        private LaPoderosaModelContainer db = new LaPoderosaModelContainer();

        // GET: Productoes
        public ActionResult Index()
        {
            var productos = db.Productos.Include(p => p.Categoria).Include(p => p.Proveedor);
            return View(productos.ToList());
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre");
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreProducto,CantidadPorUnidad,PrecioUnidad,UnidadesEnExistencia,Costo,CategoriaId,ProveedorId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", producto.CategoriaId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", producto.CategoriaId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreProducto,CantidadPorUnidad,PrecioUnidad,UnidadesEnExistencia,Costo,CategoriaId,ProveedorId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Nombre", producto.CategoriaId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
