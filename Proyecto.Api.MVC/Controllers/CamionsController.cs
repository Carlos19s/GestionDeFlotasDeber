using GestionDeFlotas.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Consumer;

namespace Proyecto.Api.MVC.Controllers
{
    public class CamionsController : Controller
    {
        // GET: CamionsController
        public ActionResult Index()
        {
            var data= Crud<Camion>.GetAll();
            return View(data);
        }

        // GET: CamionsController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Camion>.GetById(id);
            return View(data);
        }

        // GET: CamionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CamionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Camion data)
        {
            try
            {
                Crud<Camion>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CamionsController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Camion>.GetById(id);
            return View(data);
        }

        // POST: CamionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Camion camion)
        {
            try
            {
                Crud<Camion>.Update(id,camion);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CamionsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Camion>.GetById(id);
            return View(data);
        }

        // POST: CamionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Camion data)
        {
            try
            {
                Crud<Camion>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> BuscarCamionesConFiltros(string? searchTerm, String? Placa)
        {
            try
            {
                // Llamamos al método en Crud que obtiene los mantenimientos con los filtros
                var data = await Crud<Camion>.GetCamionesConFiltros( searchTerm, Placa);

                // Pasamos los datos a la vista
                return View(data);
            }
            catch (Exception ex)
            {
                // En caso de error, lo mostramos en la vista
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return View();
            }
        }
    }
}
