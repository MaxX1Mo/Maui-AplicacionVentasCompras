using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Utils;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace App_VentasCompras_Maui.Service
{
    public class CategoriaService
    {
        private readonly HttpClient _httpClient;
        public CategoriaService()
        {
            _httpClient = new HttpClient();
            // Configura la base URL de la API
            _httpClient.BaseAddress = new Uri(EndPoints.URLApi);
        }
        public async Task<List<Categoria>> GetListaCategorias()
        {
            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            // Enviar la solicitud al endpoint de Lista Producto
            var response = await _httpClient.GetAsync(EndPoints.ListaCategoria);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var categorias = JsonConvert.DeserializeObject<List<Categoria>>(json);
                return categorias;
            }
            else
            {
                throw new Exception("Fallo en la solicitud de datos (o puedes que no estes autorizado)");
            }
        }

        public async Task EditarCategoria(Categoria categoria)
        {
            var jsonContent = JsonConvert.SerializeObject(categoria);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.PutAsync($"{EndPoints.EditarCategoria}/{categoria.IDCategoria}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error en la edicion de la categoria");
            }
        }

        public async Task EliminarCategoria(int idCategoria)
        {
            var id = new { Id = idCategoria };
            var jsonContent = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.DeleteAsync($"{EndPoints.EliminarCategoria}/{idCategoria}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Algo fallo en la eliminacion de la categoria");
            }
        }

        public async Task CrearCategoria(CategoriaDTO categoria)
        {
            var jsonContent = JsonConvert.SerializeObject(categoria);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.PostAsync(EndPoints.CrearCategoria, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[ERROR] {response.StatusCode} - {error}");
                throw new Exception("Error en la creacion de la categoria");
            }
        }

        public async Task BuscarCategoria(int idCategoria)
        {
            var id = new { Id = idCategoria };
            var jsonContent = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.GetAsync($"{EndPoints.BuscarCategoria}/{idCategoria}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Algo fallo en la busqueda de la categoria");
            }
        }

    }
}
