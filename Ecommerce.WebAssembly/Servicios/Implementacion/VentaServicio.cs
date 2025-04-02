using Ecommerce.DTO;
using Ecommerce.WebAssembly.Servicios.Contrato;
using System.Net.Http.Json;


namespace Ecommerce.WebAssembly.Servicios.Implementacion
{
    public class VentaServicio : IVentaServicio
    {
        private readonly HttpClient _http;
        public VentaServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<VentaDTO>> Registrar(VentaDTO modelo)
        {
            var response = await _http.PostAsJsonAsync("Venta/Registrar", modelo);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<VentaDTO>>();
            return result!;
        }
    }
}
