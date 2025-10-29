using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using App_VentasCompras_Maui.Views;
using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Utils;


namespace App_VentasCompras_Maui.ViewModel
{
    public partial class ProductosPorUsuarioViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;

        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<Producto> Productos { get; } = new();


        [ObservableProperty]
        Producto productoSeleccionado;

        private int? idUsuario;


        public ProductosPorUsuarioViewModel(ProductoService productoService)
        {

            _productoService = productoService;
        }

        public async Task ObtenerUsuario()
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token)) return;

            idUsuario = GetUsuario.GetUserId(token);
        }

        [RelayCommand]
        private async Task GetProductos()
        {

                await ObtenerUsuario();
                if (!idUsuario.HasValue)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener el ID del usuario", "OK");
                    return;
                }
            
            try
            {
                var productos = await _productoService.ProductosPorUsuario(idUsuario.Value);
                if (productos != null)
                {
                    if (Productos.Count != 0)
                    Productos.Clear();
                    
                    foreach (var producto in productos)
                        Productos.Add(producto);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
            }
        }


        [RelayCommand]
        private async Task GoToDetail()
        {
            if (productoSeleccionado == null)
            {
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new ProductoDetallePage(productoSeleccionado), true);
        }
    }
}
