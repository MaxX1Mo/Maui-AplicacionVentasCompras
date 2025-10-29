using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Utils;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;


namespace App_VentasCompras_Maui.Service
{
    public class ProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService()
        {
            _httpClient = new HttpClient();
            // Configura la base URL de la API
            _httpClient.BaseAddress = new Uri(EndPoints.URLApi);

        }

        
        public async Task<List<Producto>> GetListaProductos()
        {
            // Recupera el token JWT almacenado en SecureStorage
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("No se encontró el token de autenticación.");
            }

            // Añade el token al encabezado de autorizacion
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Enviar la solicitud al endpoint de Lista Producto
            var response = await _httpClient.GetAsync(EndPoints.ListaProducto);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                return productos;   
            }
            else
            {
                throw new Exception("Fallo en la solicitud de datos (o puedes que no estes autorizado)");
            }
        }

        public async Task EditarProducto(Producto producto)
        {
            var jsonContent = JsonConvert.SerializeObject(producto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.PutAsync($"{EndPoints.EditarProducto}/{producto.IDProducto}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error en la edicion del producto");
            }
        }

        public async Task EliminarProducto(int idProducto)
        {
            var id = new { Id = idProducto };
            var jsonContent = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.DeleteAsync($"{EndPoints.EliminarProducto}/{idProducto}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Algo fallo en la eliminacion del producto");
            }
        }

        public async Task CrearProducto(ProductoCrearDTO producto)
        {
            var jsonContent = JsonConvert.SerializeObject(producto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.PostAsync(EndPoints.CrearProducto, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[ERROR] {response.StatusCode} - {error}");
                throw new Exception("Error en la creacion del producto");
            }
        }

        public async Task BuscarProducto(int idProducto)
        {
            var id = new { Id = idProducto };
            var jsonContent = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.GetAsync($"{EndPoints.BuscarProducto}/{idProducto}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Algo fallo en la busqueda del producto");
            }
        }
        
        public async Task<List<Producto>> ProductosPorUsuario(int idUsuario)
        {
            #region logica para autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token))
                throw new Exception("No se encontró el token de autenticación.");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion


            var response = await _httpClient.GetAsync($"{EndPoints.ListaProductoPorUsuario}?id={idUsuario}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Productos JSON: {json}");
                var productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                return productos;
            }
            else
            {
                throw new Exception("Fallo en la solicitud de datos (o puedes que no estes autorizado)");
            }
        }

        public async Task<List<Producto>> ProductosPorCategoria(string categoria)
        {
            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion
            var response = await _httpClient.GetAsync($"{EndPoints.ListaProductoPorCategoria}?categoria={categoria}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"[DEBUG] Productos JSON: {json}");
                var productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                return productos;
            }
            else
            {
                throw new Exception("Fallo en la solicitud de datos (o puedes que no estes autorizado)");
            }
        }

        public async Task<List<Producto>> ProductosPorNombre(string nombre)
        {
            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion
            var response = await _httpClient.GetAsync($"{EndPoints.ListaProductoPorNombre}?nombre={nombre}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine($"[DEBUG] Productos JSON: {json}");


                var productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                return productos;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[ERROR] {response.StatusCode} - {error}");
                throw new Exception("Fallo en la solicitud de datos (o puedes que no estes autorizado)");
            }
        }

        public async Task<List<Producto>> ProductosPorUbicacion(string provincia, string localidad)
        {
            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion


            var response = await _httpClient.GetAsync($"{EndPoints.ListaProductoPorUbicacion}?provincia={Uri.EscapeDataString(provincia)}&localidad={Uri.EscapeDataString(localidad)}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                return productos;
            }
            else
            {
                throw new Exception("Fallo en la solicitud de datos (o puedes que no estes autorizado)");
            }
        }
    }
}
