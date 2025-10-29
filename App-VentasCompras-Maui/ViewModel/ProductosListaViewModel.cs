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
using Microsoft.IdentityModel.Tokens;


namespace App_VentasCompras_Maui.ViewModel
{
    public partial class ProductosListaViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;

        public ObservableCollection<Producto> Productos { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Producto productoSeleccionado;

        bool isBusy;

        public ProductosListaViewModel(ProductoService productoService)
        {

            _productoService = productoService;
        }

        [RelayCommand]
        private async Task GetProductos()
        {
            
            if (!isBusy)
            {
                try
                {
                    isBusy = true;

                    var productos = await _productoService.GetListaProductos();

                    if (productos != null)
                    {
                        if (Productos.Count != 0)
                            Productos.Clear();

                        foreach (var producto in productos)
                            Productos.Add(producto);
                    }

                    isBusy = false;
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "Ok");

                }
                finally
                {
                    isBusy = false;
                }

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
