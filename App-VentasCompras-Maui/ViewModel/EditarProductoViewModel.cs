using App_VentasCompras_Maui.Models;
using App_VentasCompras_Maui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace App_VentasCompras_Maui.ViewModel
{
    public partial class EditarProductoViewModel : ObservableObject
    {
        private readonly ProductoService _productoService;
        private readonly CategoriaService _categoriaService;
        [ObservableProperty]
        Producto producto;

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

        public EditarProductoViewModel(Producto producto, CategoriaService categoriaService)
        {
            _productoService = new ProductoService();
            Producto = producto;
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

        [RelayCommand]
        public async Task GuardarProducto()
        {
            try
            {
                await _productoService.EditarProducto(Producto);
                await Application.Current.MainPage.DisplayAlert("Exito", "Producto guardado correctamente", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Hubo un problema: {ex.Message}", "OK");
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
        public async Task GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
