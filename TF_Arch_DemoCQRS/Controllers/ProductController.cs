using Microsoft.AspNetCore.Mvc;
using TF_Arch_DemoCQRS.Models.Entities;
using TF_Arch_DemoCQRS.Models.Queries;
using Tools.Cqrs.Queries;

namespace TF_Arch_DemoCQRS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IQueryHandler<GetAllProductQuery, IEnumerable<Produit>> _getAllQueryHandler;
        private readonly GetOneProductQueryHandler _getOneProductQueryHandler;

        public ProductController(IQueryHandler<GetAllProductQuery, IEnumerable<Produit>> getAllQueryHandler, GetOneProductQueryHandler getOneProductQueryHandler)
        {
            _getAllQueryHandler = getAllQueryHandler;
            _getOneProductQueryHandler = getOneProductQueryHandler;
        }

        public IActionResult Index()
        {
            GetAllProductQuery query = new GetAllProductQuery();

            return View(_getAllQueryHandler.Execute(query));
        }

        public IActionResult Details(int id)
        {
            GetOneProductQuery query = new GetOneProductQuery(id);
            Produit? produit = _getOneProductQueryHandler.Execute(query);

            if (produit is null)
                return RedirectToAction("Index");

            return View(produit);
        }
    }
}
