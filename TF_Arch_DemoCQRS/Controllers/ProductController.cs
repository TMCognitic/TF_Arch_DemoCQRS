using Microsoft.AspNetCore.Mvc;
using TF_Arch_DemoCQRS.Models.Commands;
using TF_Arch_DemoCQRS.Models.Entities;
using TF_Arch_DemoCQRS.Models.Forms;
using TF_Arch_DemoCQRS.Models.Queries;
using Tools.Cqrs;
using Tools.Cqrs.Command;
using Tools.Cqrs.Queries;

namespace TF_Arch_DemoCQRS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public ProductController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public IActionResult Index()
        {
            GetAllProductQuery query = new GetAllProductQuery();

            return View(_dispatcher.Dispatch(query));
        }

        public IActionResult Details(int id)
        {
            GetOneProductQuery query = new GetOneProductQuery(id);
            Produit? produit = _dispatcher.Dispatch(query);

            if (produit is null)
                return RedirectToAction("Index");

            return View(produit);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductForm form) 
        {
            if(!ModelState.IsValid)
                return View(form);

            Result result = _dispatcher.Dispatch(new CreateProductCommand(form.Nom, form.Description, form.Prix));

            if(result.IsFailure)
            {
                ModelState.AddModelError("", result.Message!);
                return View(form);
            }

            return RedirectToAction("Index");            
        }

        public IActionResult Enable(int id)
        {
            _dispatcher.Dispatch(new EnableProductCommand(id));
            return RedirectToAction("Index");
        }

        public IActionResult Disable(int id)
        {
            _dispatcher.Dispatch(new DisableProductCommand(id));
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _dispatcher.Dispatch(new DeleteProductCommand(id));
            return RedirectToAction("Index");
        }
    }
}
