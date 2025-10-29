using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Utils;
using Newtonsoft.Json;
using System.Text;

namespace App_VentasCompras_Maui.Service
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService()
        {
            _httpClient = new HttpClient();
            // Configura la base URL de la API
            _httpClient.BaseAddress = new Uri(EndPoints.URLApi);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var jsonContent = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Enviar la solicitud al endpoint de login
            var response = await _httpClient.PostAsync(EndPoints.Login, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON en tu modelo LoginM
                var tokenResponse = JsonConvert.DeserializeObject<LoginM>(jsonResponse);

                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
                {
                    // Guardar solo el token en SecureStorage
                    await SecureStorage.SetAsync("auth_token", tokenResponse.Token);

                    return tokenResponse.Token;
                }
                else
                {
                    throw new Exception("Token no valido en la respuesta del servidor.");
                }
            }
            else
            {
                throw new Exception("Login failed");
            }
        }
        
        public async Task<bool> RegistrarAsync(string email, string username, string password,
            string nombre, string apellido, string nrocelular,
            string pais, string localidad, string provincia, string? codigopostal, string? nrocalle, string? calle)
        {
            var usuario = new
            {
                Email = email,
                Username = username,
                Password = password,
                Nombre = nombre,
                Apellido = apellido,
                Nrocelular = nrocelular,
                Pais = pais,
                Localidad = localidad,
                Provincia = provincia,
                CodigoPostal = codigopostal,
                NroCalle = nrocalle, 
                Calle = calle,
            };

            var jsonContent = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Enviar la solicitud al endpoint de login
            var response = await _httpClient.PostAsync(EndPoints.Registrar, content);

            return response.IsSuccessStatusCode;
        }
    }
}
