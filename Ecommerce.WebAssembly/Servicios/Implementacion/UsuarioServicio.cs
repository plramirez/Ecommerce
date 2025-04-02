using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;
using System.Reflection;


namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class UsuarioServicio :IUsuarioServicio
    {
        private readonly HttpClient _http;
        public UsuarioServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<SesionDTO>> Autorizacion(LoginDTO modelo)
        {
            var response = await _http.PostAsJsonAsync("Usuario/Autorizacion", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<SesionDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Editar(UsuarioDTO modelo)
        {
            var response = await _http.PutAsJsonAsync("Usuario/Editar", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Eliminar(int id)
        {
            return await _http.DeleteFromJsonAsync<ResponseDTO<bool>>($"Usuario/Eliminar/{id}");
        }

        public async Task<ResponseDTO<UsuarioDTO>> Obtener(int id)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<UsuarioDTO>>($"Usuario/Obtener/{id}");
        }

        public async Task<ResponseDTO<UsuarioDTO>> Crear(UsuarioDTO modelo)
        {
            var response = await _http.PostAsJsonAsync("Usuario/Crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<UsuarioDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<List<UsuarioDTO>>> Lista(string rol, string buscar)
        {
            return await _http.GetFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>($"Usuario/Lista/{rol}/{buscar}");
        }
    }
}
