using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;

namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly HttpClient _http;
        public ProductoServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO modelo)
        {
            var response = await _http.PostAsJsonAsync("Producto/Crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Editar(ProductoDTO modelo)
        {
            var response = await _http.PutAsJsonAsync("Producto/Editar", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int id)
        {
            return await _http.DeleteFromJsonAsync<ResponseDTO<bool>>($"Producto/Eliminar/{id}");
        }

        public async Task<ResponseDTO<ProductoDTO>> Obtener(int id)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<ProductoDTO>>($"Producto/Obtener/{id}");
        }

        public async Task<ResponseDTO<List<ProductoDTO>>> Catalogo(string categoria, string buscar)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>(
                               $"Producto/Catalogo/{categoria}/{buscar}");
        }

        public async Task<ResponseDTO<List<ProductoDTO>>> Lista(string buscar)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>(
                               $"Producto/Lista/{buscar}");
        }
    }
}
