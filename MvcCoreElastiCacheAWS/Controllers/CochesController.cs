using Microsoft.AspNetCore.Mvc;
using MvcCoreElastiCacheAWS.Repository;

namespace MvcCoreElastiCacheAWS.Controllers
{
    public class CochesController : Controller
    {
        private CochesRepository repo;
        public CochesController(CochesRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(this.repo.GetAllCoches());
        }

        public IActionResult Details(int id)
        {
            return View(this.repo.FindCoche(id));
        }

    }
}
