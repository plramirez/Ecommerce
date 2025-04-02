using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class TarjetaDTO
    {

        [Required(ErrorMessage = "Ingrese nombre del titular")]
        public string? Titular { get; set; }

        [Required(ErrorMessage = "Ingrese numero de la tarjeta")]
        public string? Numero { get; set; }

        [Required(ErrorMessage = "Ingrese Fecha de Vencimiento")]
        public string? Vigencia { get; set; }

        [Required(ErrorMessage = "Ingrese codigo de seguridad")]
        public string? CVV { get; set; }
    }
}
