﻿using Blazored.LocalStorage;
using Ecommerce.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Ecommerce.WebAssembly.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public AutenticacionExtension(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task ActualizarEstadoAutenticacion(SesionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if(sesionUsuario != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, sesionUsuario.NombreCompleto),
                    new Claim(ClaimTypes.Email, sesionUsuario.Correo),
                    new Claim(ClaimTypes.Role, sesionUsuario.Rol)
                }, "JwAuth"));

                await _localStorage.SetItemAsync("SesionUsuario", sesionUsuario);

            }
            else
            {
                claimsPrincipal = _sinInformacion;
                await _localStorage.RemoveItemAsync("SesionUsuario");
            }
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _localStorage.GetItemAsync<SesionDTO>("SesionUsuario");

            if(sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, sesionUsuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, sesionUsuario.NombreCompleto),
                new Claim(ClaimTypes.Email, sesionUsuario.Correo),
                new Claim(ClaimTypes.Role, sesionUsuario.Rol)
            }, "JwAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
