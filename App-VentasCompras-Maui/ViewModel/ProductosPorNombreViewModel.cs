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
    public partial class ProductosPorNombreViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<Producto> Productos { get; } = new();


        [ObservableProperty]
        Producto productoSeleccionado;


        [ObservableProperty]
        private string nombreProducto;

        bool isBusy;

        public ProductosPorNombreViewModel(ProductoService productoService)
        {

            _productoService = productoService;
        }


        [RelayCommand]
        private async Task GetProductos()
        {
            try
            {
                var productos = await _productoService.ProductosPorNombre(NombreProducto);

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
            if (!string.IsNullOrWhiteSpace(NombreProducto))
            {
                await GetProductos();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El campo Nombre de Producto está vacío", "OK");
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
