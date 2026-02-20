using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace App_VentasCompras_Maui.Utils
{
    //clase para extraer el id user del token
    public static class GetUsuario
    { 
        public static int? GetUserId(string token) 
        {  
            var handler = new JwtSecurityTokenHandler(); 
            
            var jwtToken = handler.ReadJwtToken(token); 
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : (int?)null;
        }
        // Obtener el rol del usuario desde el token JWT 
        public static string? GetRol(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var claim = jwt.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Role ||
                c.Type == "rol" 
            );

            return claim?.Value;
        }
    }
}
