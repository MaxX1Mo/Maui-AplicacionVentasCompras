using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class RegistrarseViewModel : ObservableObject
    {
        private readonly LoginService _login;

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string password;
        [ObservableProperty]
        private string confirmarPassword;

        [ObservableProperty]
        private string nombre;
        [ObservableProperty]
        private string apellido;
        [ObservableProperty]
        private string nrocelular;

        [ObservableProperty]
        private string pais;
        [ObservableProperty]
        private string provincia;
        [ObservableProperty]
        private string localidad;
        [ObservableProperty]
        private string? codigopostal;
        [ObservableProperty]
        private string? calle;
        [ObservableProperty]
        private string? nrocalle;

        public RegistrarseViewModel()
        {
            _login = new LoginService();
        }

        [RelayCommand]
        private async Task Registrar()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(ConfirmarPassword) ||
                string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido) || string.IsNullOrEmpty(Nrocelular) || 
                string.IsNullOrEmpty(Pais) || string.IsNullOrEmpty(Provincia) || string.IsNullOrEmpty(Localidad))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Falta campos por completar!!", "OK");
                return;
            }
            if (password != confirmarPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
                return;
            }

            var response = await _login.RegistrarAsync(email, username, password, nombre, apellido, nrocelular, pais, provincia, localidad, codigopostal, nrocalle, calle);

            if (response)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario registrado con éxito", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Hubo un problema con el registro", "OK");
            }
        }

        [RelayCommand]
        private async Task Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
