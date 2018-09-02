using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {
        private readonly IHttpContextAccessor _context;

        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var planoContasModel = new PlanoContaModel(_context);
            ViewBag.ListaPlanoContas = planoContasModel.ListaPlanoContas();
            return View();
        }

        public IActionResult Excluir(int id)
        {
            var planoConta = new PlanoContaModel(_context);
            planoConta.Excluir(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CriarPlanoConta(PlanoContaModel p_model)
        {
            if(ModelState.IsValid)
            {
                p_model._context = _context;
                p_model.Salvar();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult CriarPlanoConta(int? id)
        {
            if(id.HasValue)
            {
                var planoConta = new PlanoContaModel(_context);
                ViewBag.Registro = planoConta.CarregarRegistro(id.Value);
            }

            return View();
        }
    }
}