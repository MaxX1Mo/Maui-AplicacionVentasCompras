using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using App_VentasCompras_Maui.Utils;


namespace App_VentasCompras_Maui.ViewModel
{
    public partial class CrearProductoViewModel : ObservableObject
    {
        private readonly ProductoService _service;
        private readonly CategoriaService _categoriaService;

        [ObservableProperty]
        ProductoCrearDTO producto;

        [ObservableProperty]
        private string imagenUrl;

        // Opciones para pickers
        public ObservableCollection<string> EstadoProductoOpciones { get; } =
            new ObservableCollection<string> { "Nuevo", "Como_nuevo", "Con_detalles", "Defectuoso" };

        public ObservableCollection<string> EstadoVentaOpciones { get; } =
            new ObservableCollection<string> { "Finalizado", "Activo", "Detenido" };

        [ObservableProperty]
        private string estadoProductoSeleccionado;
        [ObservableProperty]
        private string estadoVentaSeleccionado;

        public ObservableCollection<Categoria> Categorias { get; } = new();

        [ObservableProperty]
        Categoria categoriaSeleccionada;

        [ObservableProperty]
        private string nombreCategoria;

        private int? idUsuario;

        public CrearProductoViewModel(CategoriaService categoriaService)
        {
            _service = new ProductoService();
            Producto = new ProductoCrearDTO();

            EstadoVentaSeleccionado = "Activo";
            _categoriaService = categoriaService;
            _ = CargarCategorias(); // cargar al iniciar
        }


        private async Task CargarCategorias()
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
        private void SeleccionarCategoria(Categoria categoria)
        {
            if (categoria == null) return;

            NombreCategoria = categoria.Nombre;
            Producto.NombreCategoria = categoria.Nombre;
        }


        public async Task ObtenerUsuario()
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (string.IsNullOrEmpty(token)) return;

            idUsuario = GetUsuario.GetUserId(token);
        }

        [RelayCommand]
        private async Task Crear()
        {
            await ObtenerUsuario();
            if (!idUsuario.HasValue)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se pudo obtener el ID del usuario", "OK");
                return;
            }
            try
            {
                producto.IDUsuario = idUsuario.Value;
                await _service.CrearProducto(producto);
                await Application.Current.MainPage.DisplayAlert("Exito", "Producto creado", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Hubo un problema: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task SeleccionarYSubirImagen()
        {
            // Seleccionar una imagen de la galería
            var resultado = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images
            });
            
            if (resultado != null) 
            {
                var servicioImagen = new SubirImagen();
                string urlImagen = await servicioImagen.SubirImagenCloudinaryAsync(resultado); 

                if (!string.IsNullOrEmpty(urlImagen))
                {
                    ImagenUrl = urlImagen;
                    Producto.Imagen = urlImagen;
                    await Application.Current.MainPage.DisplayAlert("Éxito", "Imagen subida correctamente", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo subir la imagen", "OK");
                }
            }
        }

        // Cuando cambien las selecciones, reflejarlas en el modelo
        partial void OnEstadoProductoSeleccionadoChanged(string value)
        {
            Producto.EstadoProducto = value;
        }
        partial void OnEstadoVentaSeleccionadoChanged(string value)
        {
            Producto.EstadoVenta = value;
        }
        partial void OnCategoriaSeleccionadaChanged(Categoria value)
        {
            if (value != null)
                SeleccionarCategoria(value);
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
