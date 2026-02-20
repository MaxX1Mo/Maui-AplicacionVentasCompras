using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class CrearCategoriaViewModel : ObservableObject
    {
        private readonly CategoriaService _categoriaService;

        [ObservableProperty]
        private string nombre;

        public CrearCategoriaViewModel(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [RelayCommand]
        private async Task Guardar()
        {
            
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Ingrese un nombre válido", "OK");
                return;
            }

            var categoria = new CategoriaDTO
            {
                Nombre = Nombre
                
            };

            await _categoriaService.CrearCategoria(categoria);

            await Application.Current.MainPage.DisplayAlert(
                "Éxito", "Categoría creada", "OK");

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
