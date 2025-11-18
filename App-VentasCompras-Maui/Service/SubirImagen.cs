using System.Net.Http.Headers;
using System.Text.Json;


namespace App_VentasCompras_Maui.Service
{
   
    public class SubirImagen
    {
        public async Task<string> GuardarImagenLocalAsync(FileResult resultado)
        {
            if (resultado == null)
                return null;

            using var sourceStream = await resultado.OpenReadAsync();

            // Generar un nombre único para evitar sobreescribir
            var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(resultado.FileName)}";

            // Carpeta local de la app
            var rutaCarpeta = FileSystem.AppDataDirectory;

            // Ruta final
            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using var localFileStream = File.OpenWrite(rutaCompleta);
            await sourceStream.CopyToAsync(localFileStream);

            Console.WriteLine($"Imagen guardada en: {rutaCompleta}");

            // Devuelve solo el nombre o ruta, como necesites guardar en tu DB/API
            return rutaCompleta;
        }
    }
}
