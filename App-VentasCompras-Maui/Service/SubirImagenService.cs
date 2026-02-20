using System.Net.Http.Headers;
using System.Text.Json;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Maui.Storage; // Necesario para FileResult
using System.Threading.Tasks;

namespace App_VentasCompras_Maui.Service
{
    // Se sube la imagen seleccionada a Cloudinary y devuelve la URL segura.
    public class SubirImagen
    {
        private readonly Cloudinary _cloudinary;
        
        public SubirImagen()
        {
            var account = new Account(
                "domwi0oke",
                "776767143276256",
                "WfsMIN-FFCh2ryzqiYlEAdTKXcI"
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> SubirImagenCloudinaryAsync(FileResult resultado)
        {
            if (resultado == null)
                return null;

            // Obtener el Stream del archivo
            using var sourceStream = await resultado.OpenReadAsync();

            //  Crear los parametros de subida
            var uploadParams = new ImageUploadParams()
            {
                // La clase FileDescription permite subir desde un Stream.
                // Le pasamos el nombre original del archivo y el Stream.
                File = new FileDescription(resultado.FileName, sourceStream),
            };

            //  Ejecutar la subida asincrona
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            //  Verificar el resultado y retornar la URL
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine($"Imagen subida a Cloudinary. URL: {uploadResult.SecureUrl}");

                // Retorna la URL segura (HTTPS)
                return uploadResult.SecureUrl.ToString();
            }
            else
            {
                Console.WriteLine($"Error al subir imagen a Cloudinary: {uploadResult.Error.Message}");
                return null;
            }
        }
        /*
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

            // Devuelve solo el nombre o ruta
            return rutaCompleta;
        }*/
    }
}
