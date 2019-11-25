using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GeradorSerial.Base;
using GeradorSerial.Models;

namespace GeradorSerial.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private MyContext db = new MyContext();

        public ActionResult Index()
        {
            if ((EnumTipoUsuario)Session["TipoUsuario"] != EnumTipoUsuario.Administrador)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(db.Usuarios.ToList());
        }

        public ActionResult Details(int? id)
        {
            if ((EnumTipoUsuario)Session["TipoUsuario"] != EnumTipoUsuario.Administrador)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuario = db.Usuarios.Find(id);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        public ActionResult Create()
        {
            if ((EnumTipoUsuario)Session["TipoUsuario"] != EnumTipoUsuario.Administrador)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Cpf,Email,Senha,Tipo,Ativo")] Usuario usuario)
        {
            if ((EnumTipoUsuario)Session["TipoUsuario"] != EnumTipoUsuario.Administrador)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                usuario.Cpf = usuario.Cpf?.GetNumbers();
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        public ActionResult Edit(int? id)
        {
            if ((EnumTipoUsuario)Session["TipoUsuario"] != EnumTipoUsuario.Administrador)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuario usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }

            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Cpf,Senha,Email,Tipo,Ativo")] Usuario usuario)
        {
            if ((EnumTipoUsuario)Session["TipoUsuario"] != EnumTipoUsuario.Administrador)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.Entry(usuario).Property(c => c.Cpf).EntityEntry.State = EntityState.Unchanged;
                db.Entry(usuario).Property(c => c.Senha).EntityEntry.State = EntityState.Unchanged;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
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
