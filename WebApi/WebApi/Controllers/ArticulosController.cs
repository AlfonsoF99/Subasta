using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ArticulosController : ApiController
    {
        private SubastaEntities3 db = new SubastaEntities3();

        // GET: api/Articulos
        public IQueryable<Articulos> GetArticulos()
        {
            return db.Articulos;
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public IHttpActionResult GetArticulos(int id)
        {
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return NotFound();
            }

            return Ok(articulos);
        }

        // PUT: api/Articulos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticulos(int id, Articulos articulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articulos.IdArt)
            {
                return BadRequest();
            }

            db.Entry(articulos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticulosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Articulos
        [ResponseType(typeof(Articulos))]
        public IHttpActionResult PostArticulos(Articulos articulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var articulo = from a in db.Articulos
                           where a.IdArt == articulos.IdArt
                           select new
                           {
                               IdArt = a.IdArt
                           };
            //if(articulo==0)
            //{
                db.Articulos.Add(articulos);
                db.SaveChanges();
            //}

            return CreatedAtRoute("DefaultApi", new { id = articulos.IdArt }, articulos);
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public IHttpActionResult DeleteArticulos(int id)
        {
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return NotFound();
            }

            db.Articulos.Remove(articulos);
            db.SaveChanges();

            return Ok(articulos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticulosExists(int id)
        {
            return db.Articulos.Count(e => e.IdArt == id) > 0;
        }
    }
}