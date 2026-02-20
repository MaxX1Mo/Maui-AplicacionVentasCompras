using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_VentasCompras_Maui.Views;
using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class AdministrarCategoriasViewModel : ObservableObject
    {
        private readonly CategoriaService _categoriaService;

        [ObservableProperty]
        bool isRefreshing;

        public ObservableCollection<Categoria> Categorias { get; } = new();
        public AdministrarCategoriasViewModel()
        {
            _categoriaService = new CategoriaService();

            _ = CargarCategorias();     
        }
        
        public async Task CargarCategorias()
        {
            try
            {
                IsRefreshing = true;

                var categorias = await _categoriaService.GetListaCategorias();

                Categorias.Clear();

                if (categorias != null)
                {
                    foreach (var cat in categorias)
                        Categorias.Add(cat);
                }
            }
            finally
            {
                IsRefreshing = false;
            }

        }
        [RelayCommand]
        private async Task Refresh()
        {
            await CargarCategorias();
        }
        [RelayCommand]
        private async Task CrearCategoria()
        {
            await Application.Current.MainPage.Navigation
                .PushAsync(new CrearCategoriaPage());
            
        }

        // ✏ Editar
        [RelayCommand]
        private async Task EditarCategoria(Categoria categoria)
        {
            if (categoria == null) return;

            await Application.Current.MainPage.Navigation
                .PushAsync(new EditarCategoriaPage(categoria));
        }

        // 🗑 Eliminar
        [RelayCommand]
        private async Task EliminarCategoria(Categoria categoria)
        {
            if (categoria == null) return;

            bool confirmar = await Application.Current.MainPage.DisplayAlert(
                "Confirmar",
                $"¿Eliminar la categoría '{categoria.Nombre}'?",
                "Sí", "Cancelar");

            if (!confirmar) return;

            await _categoriaService.EliminarCategoria(categoria.IDCategoria.Value);
            Categorias.Remove(categoria);
        }
    }
}
