using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using App_VentasCompras_Maui.Views;


namespace App_VentasCompras_Maui.ViewModel
{
    public partial class MenuViewModel : ObservableObject
    {
        public MenuViewModel()
        {
            
        }
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
    }
}
