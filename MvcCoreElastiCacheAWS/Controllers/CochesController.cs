using Microsoft.AspNetCore.Mvc;
using MvcCoreElastiCacheAWS.Models;
using MvcCoreElastiCacheAWS.Repository;
using MvcCoreElastiCacheAWS.Services;

namespace MvcCoreElastiCacheAWS.Controllers
{
    public class CochesController : Controller
    {
        private CochesRepository repo;
        private AWSCacheService service;
        public CochesController(CochesRepository repo, AWSCacheService service)
        {
            this.service = service;
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Coche> cars = this.repo.GetAllCoches();
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            return View(this.repo.FindCoche(id));
        }

        public async Task<IActionResult> Favoritos()
        {
            List<Coche> cars = await this.service.GetCochesFavoritosAsync();
            return View(cars);
        }

        public async Task<IActionResult> SeleccionarFavorito(int idcoche)
        {
            Coche car = this.repo.FindCoche(idcoche);
            await this.service.AddCocheFavoritoAsync(car);
            return RedirectToAction("Favoritos");
        }

        public async Task<IActionResult> DeleteFavorito(int idcoche)
        {
            await this.service.DeleteCocheFavoritoAsync(idcoche);
            return RedirectToAction("Favoritos");
        }

    }
}
