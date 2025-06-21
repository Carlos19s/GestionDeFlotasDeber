using GestionDeFlotas.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Consumer;

namespace Proyecto.Api.MVC.Controllers
{
    public class ConductorsController : Controller
    {
        // GET: ConductorsController
        public ActionResult Index()
        {
            var data= Crud<Conductor>.GetAll();
            return View(data);
        }

        // GET: ConductorsController/Details/5
        public ActionResult Details(int id)
        {
            var data= Crud<Conductor>.GetById(id);
            return View(data);
        }

        // GET: ConductorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConductorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Conductor data)
        {
            try
            {
                Crud<Conductor>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConductorsController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Conductor>.GetById(id);
            return View(data);
        }

        // POST: ConductorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Conductor data )
        {
            try
            {
                Crud<Conductor>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConductorsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Conductor>.GetById(id);
            return View(data);
        }

        // POST: ConductorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Conductor data)
        {
            try
            {
                Crud<Conductor>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> BuscarConductoresConFiltros(string? searchTerm, String? Licencia)
        {
            try
            {
                // Llamamos al método en Crud que obtiene los mantenimientos con los filtros
                var data = await Crud<Conductor>.GetConductoresConFiltros(searchTerm, Licencia);

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
