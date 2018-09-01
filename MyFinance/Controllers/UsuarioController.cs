using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(int? id)
        {
            if(id.HasValue && id.Value.Equals(0))
            {
                HttpContext.Session.SetString("NomeUsuarioLogado",string.Empty);
                HttpContext.Session.SetString("IdUsuarioLogado",string.Empty);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Registrar(UsuarioModel p_user)
        {
            if(ModelState.IsValid)
            {
                p_user.RegistrarUsuario();
                return RedirectToAction("Sucesso");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult ValidarLogin(UsuarioModel p_usuario)
        {
            bool login = p_usuario.ValidarLogin();

            if(login)
            {
                //TempData["NomeUsuarioLogado"] = p_usuario.Nome;

                HttpContext.Session.SetString("NomeUsuarioLogado",p_usuario.Nome);
                HttpContext.Session.SetString("IdUsuarioLogado",p_usuario.Id.ToString());

                return RedirectToAction("Menu","Home");
            }


            TempData["MensagemLoginInvalido"] = "Dados inválidos";
            return RedirectToAction("Login");
        }

        public IActionResult Sucesso()
        {
            return View();
        }



    }
}