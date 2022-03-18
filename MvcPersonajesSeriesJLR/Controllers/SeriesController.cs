using Microsoft.AspNetCore.Mvc;
using MvcPersonajesSeriesJLR.Models;
using MvcPersonajesSeriesJLR.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPersonajesSeriesJLR.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceApiSeries service;
        public SeriesController(ServiceApiSeries service)
        {
            this.service = service;
        }
        public async Task<ActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }
        public async Task<ActionResult> Details(int idserie)
        {
            Serie serie = await this.service.FindSerieAsync(idserie);
            return View(serie);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Serie serie)
        {
            await this.service.InsertarSerieAsync(serie.Nombre, serie.Imagen, serie.Puntuacion, serie.Año);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int idserie)
        {
            Serie serie = await this.service.FindSerieAsync(idserie);
            return View(serie);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Serie serie)
        {
            await this.service.UpdateSerieAsync(serie.IdSerie, serie.Nombre, serie.Imagen, serie.Puntuacion, serie.Año);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int idserie)
        {
            await this.service.DeleteSerieAsync(idserie);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> PersonajesSeries(int idserie)
        {
            List<Personaje> personajes = await this.service.FindPersonajeSerie(idserie);
            return View(personajes);
        }
        public async Task<ActionResult> NewPersonaje()
        {
            List<Personaje> personajes = await this.service.GetPersonajes();
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View(personajes);
        }
        [HttpPost]
        public async Task<ActionResult> NewPersonaje(Personaje personaje)
        {
            await this.service.InsertarPersonajeAsync(personaje.Nombre, personaje.Imagen, personaje.IdSerie);
            List<Personaje> personajes = await this.service.GetPersonajes();
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;
            return View(personaje); 
        }
        public async Task<ActionResult> DeletePersonaje(int idpersonaje)
        {
            await this.service.DeletePersonajeAsync(idpersonaje);
            return RedirectToAction("Index");
        }
    }
}
