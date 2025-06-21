using GestionDeFlotas.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Consumer;

namespace Proyecto.Api.MVC.Controllers
{
    public class TallersController : Controller
    {
        // GET: TallersController
        public ActionResult Index()
        {
            var data = Crud<Taller>.GetAll();
            return View(data);
        }

        // GET: TallersController/Details/5
        public ActionResult Details(int id)
        {
            var data= Crud<Taller>.GetById(id);
            return View(data);
        }

        // GET: TallersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TallersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Taller data)
        {
            try
            {
                Crud<Taller>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TallersController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Taller>.GetById(id);
            return View(data);
        }

        // POST: TallersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Taller data)
        {
            try
            {
                Crud<Taller>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TallersController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Taller>.GetById(id);
            return View(data);
        }

        // POST: TallersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Taller data)
        {
            try
            {
                Crud<Taller>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> BuscarTalleresConFiltros(int? Tallerid ,string? searchTerm, String? Ciudad)
        {
            try
            {
                // Llamamos al método en Crud que obtiene los mantenimientos con los filtros
                var data = await Crud<Taller>.GetTalleresConFiltros(Tallerid,searchTerm, Ciudad);

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
