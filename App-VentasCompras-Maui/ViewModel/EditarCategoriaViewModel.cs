using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App_VentasCompras_Maui.ViewModel
{
    public partial class EditarCategoriaViewModel : ObservableObject
    {
        private readonly CategoriaService _categoriaService;
        private readonly Categoria _categoria;

        [ObservableProperty]
        private string nombre;

        public EditarCategoriaViewModel(Categoria categoria, CategoriaService categoriaService)
        {
            _categoria = categoria;
            _categoriaService = categoriaService;

            Nombre = categoria.Nombre;
        }

        [RelayCommand]
        private async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "El nombre no puede estar vacío", "OK");
                return;
            }

            _categoria.Nombre = Nombre;

            await _categoriaService.EditarCategoria(_categoria);

            await Application.Current.MainPage.DisplayAlert(
                "Éxito", "Categoría actualizada", "OK");

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
