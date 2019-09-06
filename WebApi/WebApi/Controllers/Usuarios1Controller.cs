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
    public class Usuarios1Controller : ApiController
    {
        private SubastaEntities3 db = new SubastaEntities3();

        // GET: api/Usuarios1
        public IQueryable<Usuarios> GetUsuarios()
        {
            return db.Usuarios;
        }

        // GET: api/Usuarios1/5

        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult GetUsuarios(string Nombre)
        {
            var usuarios = from a in db.Usuarios where a.Nombre==Nombre select new
            {
                IdUsuario = a.IdUsuario,
                Nombre = a.Nombre,
                Apellidos = a.Apellidos,
                Email = a.Email,
                Contrase = a.Contrase
            };
            foreach (var c in usuarios)
            {
                System.Console.WriteLine(c.IdUsuario + "  " + c.Nombre+" "+c.Apellidos+" "+c.Email+" "+c.Contrase);
            }
            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        // PUT: api/Usuarios1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuarios(int id, Usuarios usuarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            db.Entry(usuarios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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

        // POST: api/Usuarios1


        // DELETE: api/Usuarios1/5
        [ResponseType(typeof(Usuarios))]
        public IHttpActionResult DeleteUsuarios(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuarios);
            db.SaveChanges();

            return Ok(usuarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuariosExists(int id)
        {
            return db.Usuarios.Count(e => e.IdUsuario == id) > 0;
        }
    }
}