using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using App_VentasCompras_Maui.Views;
using App_VentasCompras_Maui.Utils;
using App_VentasCompras_Maui.Service;


namespace App_VentasCompras_Maui.ViewModel
{
    public partial class MenuViewModel : ObservableObject
    {
        private string? rol;
        public MenuViewModel()
        {

            VerificarRol(); // Verificar el rol del usuario al inicializar el ViewModel

            GoToAdministrarCommand.NotifyCanExecuteChanged(); // Actualizar el estado del comando
        }
        public bool IsAdmin => CanAdministrar();
        //boton recientes, publicaciones de producto por fecha, es modo default
        [RelayCommand]
        public async Task GoToProductoLista()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductosListaPage());
        }

        //boton buscar productos por nombre
        [RelayCommand]
        public async Task GoToBuscarProductos()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductosPorNombrePage());
        }

        //boton buscar por categoria
        [RelayCommand]
        public async Task GoToBuscarPorCategoria()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductosPorCategoriaPage());
        }


        //boton buscar por ubicacion
        [RelayCommand]
        public async Task GoToBuscarPorUbicacion()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductosPorUbicacionPage());
        }

        //boton crear publicacion, crear producto
        [RelayCommand]
        public async Task GoToCrearPublicacion()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CrearProductoPage());
        }

        //boton mis publicaciones , productos creado por usuario logeado actual
        [RelayCommand]
        public async Task GoToMisPublicaciones()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProductosPorUsuarioPage());
        }

        //boton mi perfil, pagina para detalles del usuario logeado actual
        [RelayCommand]
        public async Task GoToMiPerfil()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetalleUsuarioPage());
        }

        // El método que verifica la condición
        private bool CanAdministrar()
        {
            // Verifica si el rol es exactamente "Admin"
            return rol?.Equals("Admin", StringComparison.OrdinalIgnoreCase) ?? false;
        }
        //Rol de administrador puede acceder
        // Al pasar el nombre del método CanAdministrar, el comando automáticamente 
        // lo usará para determinar si está habilitado.

        [RelayCommand(CanExecute = nameof(CanAdministrar))] // Aplica la restricción de rol
        public async Task GoToAdministrar()
        {
            string actionSheetTitle = "Seleccione una opción de administración";
            string adminUsuarios = " Usuarios";
            string adminCategorias = " Categorías";
            string cancel = "Cancelar";

            // Muestra el ActionSheet (menú contextual nativo de MAUI/móvil)
            string action = await Application.Current.MainPage.DisplayActionSheet(
                actionSheetTitle,
                cancel,
                null, // Botón Destructivo
                adminUsuarios,
                adminCategorias
            );

            // Ejecuta el comando correspondiente basado en la selección
            if (action == adminUsuarios)
            {
                await GoToAdministrarUsuariosCommand.ExecuteAsync(null);
            }
            else if (action == adminCategorias)
            {
                await GoToAdministrarCategoriasCommand.ExecuteAsync(null);
            }
        }

        // Estos se ejecutan una vez que el usuario selecciona una opción del menú.
        [RelayCommand]
        public async Task GoToAdministrarUsuarios()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdministrarUsuariosPage(new AdministrarUsuariosViewModel()));
        }

        [RelayCommand]
        public async Task GoToAdministrarCategorias()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdministrarCategoriasPage(new AdministrarCategoriasViewModel()));
        }

        [RelayCommand]
        public async Task Exit()
        {
            bool confirm = await App.Current.MainPage.DisplayAlert("Confirmar", "¿Estás seguro de salir?", "Sí", "No");

            if (confirm)
            {
                SecureStorage.Remove("auth_token");
                await Application.Current.MainPage.Navigation.PopAsync();
            }

        }
        
        public async Task VerificarRol()
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token)) return;
            rol = GetUsuario.GetRol(token);

            OnPropertyChanged(nameof(IsAdmin));
            // Una vez que el rol se establece, actualiza el estado del comando.
            GoToAdministrarCommand.NotifyCanExecuteChanged();
        }
    }
}
