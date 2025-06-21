using GestionDeFlotas.Modelos;
using iText.Commons.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.API.Consumer;

namespace Proyecto.Api.MVC.Controllers
{
    public class MantenimientoesController : Controller
    {
        // GET: MantenimientoesController
        public ActionResult Index()
        {
            var data= Crud<Mantenimiento>.GetAll();
            return View(data);
        }

        // GET: MantenimientoesController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Mantenimiento>.GetById(id);

            data.taller = Crud<Taller>.GetById(data.TallerId);

            data.camion = Crud<Camion>.GetById(data.CamionId);

            return View(data);
        }


        // GET: MantenimientoesController/Create
        public ActionResult Create()
        {
            ViewBag.Camiones = GetCamion();
            ViewBag.Talleres = GetTalleres();
            return View();
        }
        private List<SelectListItem> GetCamion()
        {
            var camiones = Crud<Camion>.GetAll(); // Esto obtiene todos los camiones
            return camiones.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),  // El valor será el código del camión
                Text = p.Marca,
            }).ToList();
        }
        private List<SelectListItem> GetTalleres()
        {
            var talleres = Crud<Taller>.GetAll();
            return talleres.Select(a => new SelectListItem
            {
                Value = a.id.ToString(),
                Text = $"{a.Nombre}"
            }).ToList();
        }
        

        // POST: MantenimientoesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mantenimiento data)
        {
            try
            {
                Crud<Mantenimiento>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientoesController/Edit/5
        public ActionResult Edit(int id)
        {
            var data= Crud<Mantenimiento>.GetById(id);
            return View();
        }

        // POST: MantenimientoesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Mantenimiento data)
        {
            try
            {
                Crud<Mantenimiento>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MantenimientoesController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Mantenimiento>.GetById(id);
            return View(data);
        }

        // POST: MantenimientoesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Mantenimiento data)
        {
            try
            {
                Crud<Mantenimiento>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: MantenimientoesController/BuscarMantenimientosConFiltros
        public async Task<ActionResult> BuscarMantenimientosConFiltros(int? tallerId, string searchTerm,Boolean pendiente)
        {
            try
            {
                // Llamamos al método en Crud que obtiene los mantenimientos con los filtros
                var data = await Crud<Mantenimiento>.GetMantenimientosConFiltros(tallerId, searchTerm,pendiente);

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
