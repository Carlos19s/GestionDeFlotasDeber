using GestionDeFlotas.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Consumer;

namespace Proyecto.Api.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: UsuariosController
        public ActionResult Index()
        {
            var data = Crud<Usuario>.GetAll();
            return View(data);
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Usuario>.GetById(id);
            return View(data);
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario data)
        {
            try
            {
                Crud<Usuario>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Usuario>.GetById(id);
            return View(data);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario data)
        {
            try
            {
                Crud<Usuario>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Usuario>.GetById(id);
            return View(data);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Usuario data)
        {
            try
            {
                Crud<Usuario>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> BuscarUsuariosConFiltros(string? searchTerm)
        {
            try
            {
                // Llamamos al método en Crud que obtiene los mantenimientos con los filtros
                var data = await Crud<Usuario>.GetUsuariosConFiltros( searchTerm);

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
