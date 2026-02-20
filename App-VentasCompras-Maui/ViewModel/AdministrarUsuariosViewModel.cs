using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using App_VentasCompras_Maui.Views;
using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.Models;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class AdministrarUsuariosViewModel : ObservableObject
    {
        private readonly UsuarioService _usuarioService;

        public ObservableCollection<Usuario> Usuarios { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        bool isBusy;
        public AdministrarUsuariosViewModel()
        {
            
            _usuarioService = new UsuarioService();

            _ = GetUsuarios();
        }
        
        public async Task GetUsuarios()
        { 
            if (!isBusy)// evita llamadas simultaneas
            {
                try
                {
                    IsRefreshing = true;
                    isBusy = true;
                    var usuarios = await _usuarioService.GetListaUsuario();
                    if (usuarios != null)
                    {
                        if (Usuarios.Count != 0)
                            Usuarios.Clear();
                        foreach (var usuario in usuarios)
                            Usuarios.Add(usuario);
                    }
                    isBusy = false; 
                }
                catch (Exception ex)
                {
                   
                    await App.Current.MainPage.DisplayAlert("Error", $"Ocurrió un error al obtener los usuarios: {ex.Message}", "OK");
                }
                finally
                {
                    isBusy = false; 
                    IsRefreshing = false;
                }
               
            }
        }

        [RelayCommand]
        async Task Refresh()
        {
            await GetUsuarios();
        }

        [RelayCommand]
        private async Task CrearUsuario()
        {
            // Navegar a la página de creación de usuario
            var crearUsuarioPage = new CrearUsuarioPage();
            await Application.Current.MainPage.Navigation.PushAsync(crearUsuarioPage);
        }
        [RelayCommand]
        private async Task EditarUsuario(Usuario usuario)
        {
            if (usuario == null) return;
               
            await Application.Current.MainPage.Navigation.PushAsync(new EditarUsuarioPage(usuario));
        }

        [RelayCommand]
        private async Task EliminarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return;
            bool confirm = await App.Current.MainPage.DisplayAlert("Confirmar", $"¿Estás seguro de eliminar al usuario {usuario.Nombre}?", "Sí", "No");
            if (!confirm) return;
                try
                {
                    await _usuarioService.EliminarUsuario(usuario.IDUsuario.Value);
                    Usuarios.Remove(usuario);
                    await App.Current.MainPage.DisplayAlert("Éxito", "Usuario eliminado correctamente", "OK");
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"Hubo un problema al eliminar el usuario: {ex.Message}", "OK");
                }     

        }


    }
}
