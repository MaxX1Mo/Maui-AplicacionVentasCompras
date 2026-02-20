using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class CrearUsuarioViewModel : ObservableObject
    {
        private readonly UsuarioService usuarioService;

        [ObservableProperty]
        UsuarioCrearDTO usuario;

        public ObservableCollection<string> RolOpciones { get; } = new ObservableCollection<string>
    {
        "Admin",
        "Usuario"
    };
        [ObservableProperty]
        private string? rolSeleccionado;

        public CrearUsuarioViewModel()
        {
            usuarioService = new UsuarioService();
            Usuario = new UsuarioCrearDTO(); 
            RolSeleccionado = "Usuario";     

        }
        partial void OnRolSeleccionadoChanged(string value)
        {

            Usuario.Rol = value;
        }

        [RelayCommand]
        private async Task Crear()
        {

            try
            {
                await usuarioService.CrearUsuario(Usuario);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario creado correctamente", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Hubo un problema: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task Volver()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
