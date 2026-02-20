using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.Utils;
using App_VentasCompras_Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class DetalleUsuarioViewModel : ObservableObject
    {
        private int? idUsuario;
        private readonly int? _usuarioIdParam; // ID recibido desde afuera

        [ObservableProperty]
        Usuario usuario;

        [ObservableProperty]
        bool esUsuarioActual; // Para mostrar u ocultar botones

        private readonly UsuarioService _usuarioService;


        public DetalleUsuarioViewModel(UsuarioService usuarioService, int? usuarioId = null)
        {
            _usuarioService = usuarioService;
            _usuarioIdParam = usuarioId;
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task ObtenerUsuario()
        {
            var token = await SecureStorage.GetAsync("auth_token");
            
            if (string.IsNullOrEmpty(token)) return;

            idUsuario = GetUsuario.GetUserId(token);
            
            if (!idUsuario.HasValue) return; 

                // Verificar si el ID del usuario logueado es diferente al ID del usuario que se está mostrando
            if (_usuarioIdParam.HasValue && _usuarioIdParam.Value != idUsuario.Value)
            {
            // Mostrando a otro usuario (vendedor, etc.)
                Usuario = await _usuarioService.BuscarUsuario(_usuarioIdParam.Value);

                Console.WriteLine($"VentasExitosas: {Usuario.VentasExitosas}");
                Console.WriteLine($"Bueno: {Usuario.Bueno}");
                Console.WriteLine($"Regular: {Usuario.Regular}");
                Console.WriteLine($"Malo: {Usuario.Malo}");

                EsUsuarioActual = false;
            }
            else
            {
            // Mostrando al usuario logueado
                Usuario = await _usuarioService.BuscarUsuario(idUsuario.Value);

                EsUsuarioActual = true;
            }

        }


        [RelayCommand]
        public async Task Eliminar()
        {
            if (!EsUsuarioActual) return;
            try
            {
                bool confirm = await App.Current.MainPage.DisplayAlert("Confirmar", "¿Estás seguro de eliminar la cuenta?", "Sí", "No");

                if (confirm)
                {
                    await _usuarioService.EliminarUsuario(idUsuario.Value);
                    await App.Current.MainPage.DisplayAlert("Éxito", "Cuenta eliminada correctamente", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Hubo un problema: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task EditarUsuario()
        {
            if (!EsUsuarioActual) return;
            if (Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditarUsuarioPage(Usuario));
            }
        }
    }
}
