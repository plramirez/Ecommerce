using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Ecommerce.DTO;    
using Ecommerce.WebAssembly.Servicios.Contrato;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CarritoServicio : ICarritoServicio
    {
        private ILocalStorageService _localStorage;
        private ISyncLocalStorageService _syncLocalStorage;
        private IToastService _toastService;


        private List<CarritoDTO> _carrito = new List<CarritoDTO>();
        public event Action MostrarItems;

        public CarritoServicio(ILocalStorageService localStorage, ISyncLocalStorageService syncLocalStorageService ,IToastService toastService)
        {
            _localStorage = localStorage;
            _syncLocalStorage = syncLocalStorageService;
            _toastService = toastService;
        }

        public async Task AgregarCarrito(CarritoDTO modelo)
        {

            try
            {
                var carrito = await _localStorage.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito == null)
                    carrito = new List<CarritoDTO>();

                var encontrado = carrito.FirstOrDefault(c => c.Producto.IdProducto == modelo.Producto.IdProducto);

                if (encontrado != null)
                    carrito.Remove(encontrado);

                carrito.Add(modelo);
                await _localStorage.SetItemAsync("carrito", carrito);

                if (encontrado != null)
                    _toastService.ShowSuccess("Producto actualizado en el carrito");
                else
                    _toastService.ShowSuccess("Producto agregado al carrito");

                MostrarItems?.Invoke();
            }
            catch
            {
                _toastService.ShowError("Ocurrió un error al agregar el producto al carrito");
            }
            
        }

        public int CantidadProductos()
        {
            var carrito = _syncLocalStorage.GetItem<List<CarritoDTO>>("carrito");
            return carrito == null ? 0 : carrito.Count;
        }

        public async Task EliminarCarrito(int idProducto)
        {
            try
            {
                var carrito = await _localStorage.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito == null)
                {
                    var elemento = carrito.FirstOrDefault(c => c.Producto.IdProducto == idProducto);
                    if(elemento != null)
                    {
                        carrito.Remove(elemento);
                        await _localStorage.SetItemAsync("carrito", carrito);
                        MostrarItems.Invoke();
                    }
                }
                    
            }
            catch
            {

            }
        }

        public async Task<List<CarritoDTO>> DevolverCarrito()
        {
            var carrito = await _localStorage.GetItemAsync<List<CarritoDTO>>("carrito");
            if(carrito == null)
                carrito = new List<CarritoDTO>();

            return carrito;
        }

        public async Task LimpiarCarrito()
        {
            await _localStorage.RemoveItemAsync("carrito");
            MostrarItems.Invoke();
        }
    }
}
