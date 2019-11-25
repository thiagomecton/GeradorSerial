using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using GeradorSerial.Models;

namespace GeradorSerial.Controllers
{
    public class LogonsController : Controller
    {
        private MyContext db = new MyContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string cpf, string senha)
        {
            ViewBag.Cpf = cpf;

            var usuario = db.Usuarios.SingleOrDefault(c => c.Cpf == cpf);

            if (usuario == null)
            {
                ViewBag.Erro = "Usuário nao encontrado";
                return View();
            }

            if (usuario.Senha != senha)
            {
                ViewBag.Erro = "Senha Inválida";
                return View();
            }

            if (usuario.Ativo == false)
            {
                ViewBag.Erro = "Usuário Inativo";
                return View();
            }

            Session["UsuarioId"] = usuario.Id;
            Session["TipoUsuario"] = usuario.Tipo;

            FormsAuthentication.SetAuthCookie(cpf, false);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View(new AlterarSenhaModel());
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = int.Parse(Session["UsuarioId"].ToString());
                var usuario = db.Usuarios.Find(usuarioId);

                if (usuario.Senha != alterarSenhaModel.SenhaAtual)
                {
                    ModelState.AddModelError("SenhaAtual", "Senha atual não confere.");
                    return View();
                }

                usuario.Senha = alterarSenhaModel.NovaSenha;
                db.SaveChanges();

                ViewBag.Success = "Senha alterada com sucesso!";
                ModelState.Clear();
            }

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
