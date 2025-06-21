using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Proyecto.API.Consumer;
using Sensores.Modelos;

namespace Proyecto.Api.MVC.Controllers
{
    public class SensorsController : Controller
    {
        // GET: SensorsController
        public async Task<IActionResult> Index()
        {

            // Luego obtienes los sensores
            var sensores = Crud<Sensor>.GetAll();
            return View(sensores);
        }

        // GET: SensorsController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Sensor>.GetById(id);
            return View(data);
        }

        // GET: SensorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SensorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sensor data)
        {
            try
            {
                Crud<Sensor>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SensorsController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Sensor>.GetById(id); 
            return View(data);
        }

        // POST: SensorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Sensor data)
        {
            try
            {
                Crud<Sensor>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SensorsController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Sensor>.GetById(id);
            return View(data);
        }

        // POST: SensorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Sensor data)
        {
            try
            {
                Crud<Delete>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(data);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Simular()
        {
            try
            {
                Crud<Sensor>.SimularSensoresAutomáticamente();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelarSimulacion()
        {
            try
            {
                Crud<Sensor>.CancelarSimulacion();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
