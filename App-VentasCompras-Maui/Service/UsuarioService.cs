using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Utils;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace App_VentasCompras_Maui.Service
{
    public class UsuarioService
    {
        private readonly HttpClient _httpClient;
        public UsuarioService()
        {
            _httpClient = new HttpClient();
            // Configura la base URL de la API
            _httpClient.BaseAddress = new Uri(EndPoints.URLApi);
        }

        public async Task<List<Usuario>> GetListaUsuario()
        {
            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.GetAsync(EndPoints.ListaUsuario);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<List<Usuario>>(json);
                return usuario;
            }
            else
            {
                throw new Exception("Fallo en la solicitud de datos");
            }
        }

        public async Task EditarUsuario(Usuario usuario)
        {
            var jsonContent = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.PutAsync($"{EndPoints.EditarUsuario}/{usuario.IDUsuario}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[ERROR] {response.StatusCode} - {error}");
                throw new Exception("Error en la edicion del usuario");
            }
        }

        public async Task EliminarUsuario(int idUsuario)
        {
            var id = new { Id = idUsuario };
            var jsonContent = JsonConvert.SerializeObject(id);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.DeleteAsync($"{EndPoints.EliminarUsuario}/{idUsuario}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Algo fallo en la eliminacion del usuario");
            }
        }

        public async Task<Usuario> BuscarUsuario(int idUsuario)
        {

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.GetAsync($"{EndPoints.BuscarUsuario}/{idUsuario}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Algo fallo en la busqueda el usuario");
            }
            var json = await response.Content.ReadAsStringAsync();
            var usuario = JsonConvert.DeserializeObject<Usuario>(json);

            return usuario;

        }

        public async Task CrearUsuario(UsuarioCrearDTO usuario)
        {
            var jsonContent = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.PostAsync(EndPoints.CrearUsuario, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"[ERROR] {response.StatusCode} - {error} y el json {content}");
                
                throw new Exception("Error en la creacion del usuario");
            }
        }

        public async Task RegistroUsuario(Usuario usuario)
        {
            var jsonContent = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            #region autenticacion
            var token = await SecureStorage.GetAsync("auth_token");
            if (string.IsNullOrEmpty(token)) { throw new Exception("No se encontró el token de autenticación."); }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            #endregion

            var response = await _httpClient.PostAsync(EndPoints.Registrar, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error en la creacion del usuario");
            }
        }



    }
}
