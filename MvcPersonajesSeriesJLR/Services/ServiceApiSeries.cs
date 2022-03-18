using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using MvcPersonajesSeriesJLR.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcPersonajesSeriesJLR.Services
{
    public class ServiceApiSeries
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;
        public ServiceApiSeries(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }
        public async Task<List<Personaje>> GetPersonajes()
        {
            string request = "/api/personajes"; 
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }
        public async Task<List<Personaje>> FindPersonajeSerie(int idserie)
        {
            string request = "/api/Series/PersonajesSerie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }
        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/series/" + idserie;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }
        public async Task InsertarSerieAsync(string nombre, string imagen, double puntuacion, int año)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "/api/series";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie();
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion = puntuacion;
                serie.Año = año;
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }
        public async Task InsertarPersonajeAsync(string nombre, string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idserie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }
        
        public async Task UpdateSerieAsync(int idserie, string nombre, string imagen, double puntuacion, int año)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie();
                serie.IdSerie = idserie;
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion = puntuacion;
                serie.Año = año;
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }
        public async Task DeleteSerieAsync(int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/series/" + idserie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/personajes/" + idpersonaje;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
