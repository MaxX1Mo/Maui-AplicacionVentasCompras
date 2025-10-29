using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class EditarUsuarioViewModel : ObservableObject
    {
        private readonly UsuarioService _usuarioService;
        [ObservableProperty]
        Usuario usuario;

        [ObservableProperty]
        private string nuevaPassword;

        public EditarUsuarioViewModel(Usuario usuario)
        {
            _usuarioService = new UsuarioService();
            Usuario = usuario;
            Usuario.Password = null;
    
        }
        [RelayCommand]
        public async Task GuardarCambios()
        {
            try
            {
                // Si el usuario escribió una nueva contraseña, la agregamos
                if (!string.IsNullOrWhiteSpace(NuevaPassword))
                {
                    Usuario.Password = NuevaPassword; // texto plano
                }
                else
                {
                    Usuario.Password = null; // para que el backend la ignore
                }
                await _usuarioService.EditarUsuario(Usuario);
                await Application.Current.MainPage.DisplayAlert("Exito", "Usuario guardado correctamente", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Hubo un problema: {ex.Message}", "OK");
            }
        }
        [RelayCommand]
        public async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}