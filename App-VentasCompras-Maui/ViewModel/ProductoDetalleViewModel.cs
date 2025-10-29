using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App_VentasCompras_Maui.Service;
using App_VentasCompras_Maui.Views;
using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Utils;
using Microsoft.Maui.ApplicationModel.Communication;

namespace App_VentasCompras_Maui.ViewModel
{
    public partial class ProductoDetalleViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;
        private readonly UsuarioService _usuarioService;

        [ObservableProperty]
        Producto producto;

        [ObservableProperty]
        bool esPropietario;

// variable para saber si hubo contacto con el vendedor para asi habilitar la seccion opiniones
        [ObservableProperty]  
        bool contactado;
        [ObservableProperty]
        bool checkBueno;

        [ObservableProperty]
        bool checkRegular;

        [ObservableProperty]
        bool checkMalo;

        [ObservableProperty]
        bool compro;

        Usuario user { get; set; }
        private int? idUsuario;

        public ProductoDetalleViewModel(ProductoService productoService, UsuarioService usuarioService)
        {
            _productoService = productoService;
            _usuarioService = usuarioService;
        }
        public async Task Inicializar(Producto producto)
        {
            Producto = producto;
            await VerificarPropietarioAsync();
            user = await _usuarioService.BuscarUsuario(Producto.IDUsuario.Value);

        }

        public async Task VerificarPropietarioAsync()
        {
            var token = await SecureStorage.GetAsync("auth_token");
            
            if (!string.IsNullOrEmpty(token))
            {
                idUsuario = GetUsuario.GetUserId(token);

                if (Producto.IDUsuario == idUsuario)
                    EsPropietario = true;
                else
                    EsPropietario = false;
            }
        }

    [RelayCommand]
        public async Task Eliminar(int id)
        {
            try
            {
                bool confirm = await App.Current.MainPage.DisplayAlert("Confirmar", "¿Estás seguro de eliminar el producto?", "Sí", "No");

                if (confirm)
                {
                    await _productoService.EliminarProducto(id);
                    await App.Current.MainPage.DisplayAlert("Éxito", "Producto eliminado correctamente", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Hubo un problema: {ex.Message}", "OK");
            }
        }
        [RelayCommand]
        private async Task EditarProducto()
        {
            if (Application.Current.MainPage != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new EditarProductoPage(Producto));
            }
        }
        [RelayCommand]
        private async Task GoContactar()
        {
            Contactado = false;
            string opcion = await App.Current.MainPage.DisplayActionSheet(
                "Contactar al propietario",
                "Cancelar",
                null,
                "Enviar correo",
                "Llamar por teléfono",
                "Enviar WhatsApp"
);

            switch (opcion)
            {
                case "Enviar correo":
                    var mailto = $"mailto:{producto.Email}?subject=Interesado%20en%20tu%20producto&body=Hola,%20me%20interesa%20tu%20producto%20en%20la%20app.";
                    Contactado = true;
                    await Launcher.Default.OpenAsync(mailto);
                    break;

                case "Llamar por teléfono":
                    var tel = $"tel:{producto.NroCelular}";
                    Contactado = true;
                    await Launcher.Default.OpenAsync(tel);
                    break;

                case "Enviar WhatsApp":
                    var numero = producto.NroCelular.StartsWith("+") ? producto.NroCelular : $"+{producto.NroCelular}";
                    var mensaje = Uri.EscapeDataString("Hola, estoy interesado en tu producto publicado en la app.");
                    var urlWhatsApp = $"https://wa.me/{numero}?text={mensaje}";

                    if (await Launcher.Default.CanOpenAsync(urlWhatsApp))
                    {
                        Contactado = true;
                        await Launcher.Default.OpenAsync(urlWhatsApp);
                    }
                    else
                    {
                        Contactado = false;
                        await App.Current.MainPage.DisplayAlert("Error", "No se pudo abrir WhatsApp", "OK");
                    }
                    break;
            }
        }


        [RelayCommand]
        private async Task VerPerfilVendedor()
        {
            if (Producto != null && Producto.IDUsuario > 0)
            {
                Console.WriteLine($"ID USUARIO PRODUCTO: {Producto.IDUsuario}");
                await Application.Current.MainPage.Navigation.PushAsync(new DetalleUsuarioPage(Producto.IDUsuario));
            }
        }
        [RelayCommand]
        private async Task GuardarValoracion()
        {
            if (!CheckBueno && !CheckRegular && !CheckMalo && !Compro)
            {
                await App.Current.MainPage.DisplayAlert("Atención", "Debes seleccionar al menos una opción.", "OK");
                return;
            }

            var usuario = user;

            // Asegurá que los valores no sean null
            usuario.Bueno ??= 0;
            usuario.Regular ??= 0;
            usuario.Malo ??= 0;
            usuario.VentasExitosas ??= 0;

            // Sumar la opinión correspondiente
            if (CheckBueno) usuario.Bueno++;
            else if (CheckRegular) usuario.Regular++;
            else if (CheckMalo) usuario.Malo++;

            // Si hubo compra exitosa, sumar a VentasExitosas
            if (Compro) usuario.VentasExitosas++;

            try
            {
                usuario.Password = null; // para que el backend la ignore

                var usuarioService = new UsuarioService();
                await usuarioService.EditarUsuario(usuario);

                await App.Current.MainPage.DisplayAlert("Gracias", "Tu valoración fue registrada con éxito.", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"No se pudo guardar la valoración: {ex.Message}", "OK");
            }
        }

        //para que no se seleccione varias opciones a la vez
        partial void OnCheckBuenoChanged(bool value)
        {
            if (value) { CheckRegular = false; CheckMalo = false; }
        }
        partial void OnCheckRegularChanged(bool value)
        {
            if (value){ CheckBueno = false; CheckMalo = false; }
        }
        partial void OnCheckMaloChanged(bool value)
        { 
            if (value){ CheckBueno = false; CheckRegular = false; }
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}