using System.Net.Http;
using System.Reflection.Metadata;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal;
using Proyecto.Api.MVC.Models;
using Proyecto.API.Consumer;
using Sensores.Modelos;
using static Proyecto.Api.MVC.Models.Reporte;

namespace Proyecto.Api.MVC.Controllers
{
    public class AlertasController : Controller
    {
        // GET: AlertasController
        public ActionResult Index()
        {
            var data = Crud<Alerta>.GetAll();
            return View(data);
        }

        // GET: AlertasController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Alerta>.GetById(id);
            return View(data);
        }

        // GET: AlertasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlertasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alerta data)
        {
            try
            {
                Crud<Alerta>.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlertasController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = Crud<Alerta>.GetById(id);
            return View(data);
        }

        // POST: AlertasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Alerta data)
        {
            try
            {
                Crud<Alerta>.Update(id, data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AlertasController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Alerta>.GetById(id);
            return View(data);
        }

        // POST: AlertasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Alerta data)
        {
            try
            {
                Crud<Alerta>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: AlertasController/GenerarMantenimientoPredictivo
        public ActionResult GenerarMantenimientoPredictivo()
        {
            var data = Crud<Alerta>.AlertasPredictivas();
            return View(data);
        }

        public IActionResult ExportarPDF()
        {
            var alertas = Crud<Alerta>.GetAll();
            var pdfBytes = Reporte.Descargar(alertas);
            return File(pdfBytes, "application/pdf", "AlertasMantenimiento.pdf");
        }

    }
}
