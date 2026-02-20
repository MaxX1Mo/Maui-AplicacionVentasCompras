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

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class ProductosPorUbicacionViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;

        public ObservableCollection<Producto> Productos { get; } = new();

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        Producto productoSeleccionado;


        [ObservableProperty]
        private string provincia;

        [ObservableProperty]
        private string localidad;

        public ProductosPorUbicacionViewModel(ProductoService productoService)
        {

            _productoService = productoService;
        }


        [RelayCommand]
        private async Task GetProductos()
        {
            try
            {
                var productos = new List<Producto>();
                if (!string.IsNullOrEmpty(Provincia))
                {
                    productos = await _productoService.ProductosPorProvincia(Provincia);
                }
                if (!string.IsNullOrEmpty(Localidad) && !string.IsNullOrEmpty(Provincia))
                {
                    productos = await _productoService.ProductosPorUbicacion(Provincia, Localidad);
                }
                //var productos = await _productoService.ProductosPorUbicacion(Provincia, Localidad);

                if (productos != null)
                {
                    Productos.Clear();

                    foreach (var producto in productos)
                    {
                        Productos.Add(producto);
                    }

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task Buscar()
        {

            if (!string.IsNullOrEmpty(Provincia) || !string.IsNullOrEmpty(Localidad))
            {
                await GetProductos();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Falta completar los campos", "OK");
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
