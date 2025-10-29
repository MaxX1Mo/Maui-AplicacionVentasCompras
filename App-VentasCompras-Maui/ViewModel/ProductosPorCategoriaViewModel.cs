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
    public partial class ProductosPorCategoriaViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;
        private readonly CategoriaService _categoriaService;

        public ObservableCollection<Producto> Productos { get; } = new();
        public ObservableCollection<Categoria> Categorias { get; } = new();

        [ObservableProperty]
        Categoria categoriaSeleccionada;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        Producto productoSeleccionado;

        public string Nombre { get; set; }
        
        public ProductosPorCategoriaViewModel(ProductoService productoService, CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
            _ = CargarCategorias();
            _productoService = productoService;
        }
        

        public async Task CargarCategorias()
        {
            var categorias = await _categoriaService.GetListaCategorias(); // endpoint que devuelve lista
            if (categorias != null)
            {
                Categorias.Clear();
                foreach (var cat in categorias)
                    Categorias.Add(cat);
            }
            //
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se encontraron categorías", "OK");
                return;
            }
            //
        }

        [RelayCommand]
        private async Task GetProductos()
        {
            if (CategoriaSeleccionada == null) return;

            var productos = await _productoService.ProductosPorCategoria(CategoriaSeleccionada.Nombre);
            Productos.Clear();
            foreach (var prod in productos)
                Productos.Add(prod);
        }
        [RelayCommand]
        private async Task Buscar()
        {
            if (CategoriaSeleccionada == null) return;
            await GetProductos();
        }
        partial void OnCategoriaSeleccionadaChanged(Categoria value)
        {
            if (value != null)
                _ = GetProductos(); // lanza la busqueda automaticamente
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
