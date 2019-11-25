using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GeradorSerial.Models;

namespace GeradorSerial.Controllers
{
    [Authorize]
    public class ChavesController : Controller
    {
        private MyContext db = new MyContext();

        public ActionResult Index()
        {
            if ((EnumTipoUsuario)Session["TipoUsuario"] != EnumTipoUsuario.Administrador)
            {
                return RedirectToAction("Create");
            }

            return View(db.Chaves.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigodeAtivacao,Data,CidadeEstado,Posto,Linha,Observacao")] Chave chave)
        {
            if (ModelState.IsValid)
            {
                var chaveGerada = MD5.MD5Hash(chave.CodigodeAtivacao).Substring(6, 8);
                chave.ChavedeAtivacao = chaveGerada;
                chave.UsuarioId = int.Parse(Session["UsuarioId"].ToString());
                chave.Data = DateTime.Now;

                db.Chaves.Add(chave);
                db.SaveChanges();

                ViewBag.ChavedeAtivacao = chaveGerada;

                return View("ChaveDeAtivacao");
            }

            return View(chave);
        }

        public ActionResult Master()
        {
            var usuarioId = int.Parse(Session["UsuarioId"].ToString());
            var usuario = db.Usuarios.Find(usuarioId);

            var dateTimeNow = DateTime.Now;

            var masterKey = db.MasterKeys.FirstOrDefault(c => 
                c.UsuarioId == usuarioId && 
                c.Data.Year == dateTimeNow.Year && 
                c.Data.Month == dateTimeNow.Month && 
                c.Data.Day == dateTimeNow.Day);

            if (masterKey == null)
            {
                var input = dateTimeNow.ToString("ddMMyy") + usuario.Cpf;
                var chaveMaster = MD5.MD5Hash(input).Substring(15, 8).ToUpper();

                masterKey = new MasterKey();
                masterKey.UsuarioId = usuarioId;
                masterKey.Data = dateTimeNow;
                masterKey.ChaveMaster = chaveMaster;

                db.MasterKeys.Add(masterKey);
                db.SaveChanges();
            }

            ViewBag.ChavedeAtivacao = masterKey.ChaveMaster;

            return View();
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
