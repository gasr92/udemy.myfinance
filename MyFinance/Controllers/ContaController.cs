using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class ContaController : Controller
    {
        private readonly IHttpContextAccessor _context;

        public ContaController(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var contaModel = new ContaModel(_context);
            ViewBag.ListaConta = contaModel.ListaConta();

            return View();
        }

        [HttpGet]
        public IActionResult CriarConta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarConta(ContaModel p_model)
        {
            if(ModelState.IsValid)
            {
                p_model._context = _context;
                p_model.Insert();
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Excluir(int id)
        {
            var conta = new ContaModel(_context);
            conta.Excluir(id);

            return RedirectToAction("Index");
        }
    }
}