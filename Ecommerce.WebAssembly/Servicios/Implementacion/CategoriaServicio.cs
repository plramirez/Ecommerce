using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;


namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly HttpClient _http;
        public CategoriaServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<CategoriaDTO>> Crear(CategoriaDTO modelo)
        {
            var response = await _http.PostAsJsonAsync("Categoria/Crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<CategoriaDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Editar(CategoriaDTO modelo)
        {
            var response = await _http.PutAsJsonAsync("Categoria/Editar", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int id)
        {
            return await _http.DeleteFromJsonAsync<ResponseDTO<bool>>($"Categoria/Eliminar/{id}");
        }

        public async Task<ResponseDTO<CategoriaDTO>> Obtener(int id)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<CategoriaDTO>>($"Categoria/Obtener/{id}");
        }

        public async Task<ResponseDTO<List<CategoriaDTO>>> Lista(string buscar)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<List<CategoriaDTO>>>("Categoria/Lista");
        }

    }
}
