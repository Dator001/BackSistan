using System.Reflection.Metadata.Ecma335;

namespace PruebaSistranLatam.Models
{
    public class Cliente
    {
        public int idCliente { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public int idTipoDocumento { get; set; }
        public string? tipoDocumento { get; set; }
        public string? numeroDocumento { get; set; }
        public DateTime? fechaNaciomiento { get; set; }
        public string? telefono1 { get; set; }
        public string? telefono2 { get; set; }
        public string? correo1 { get; set; }
        public string? correo2 { get; set; }
        public int idCiudad { get; set; }
        public string? nombreCiudad { get; set; }
        public string? direccion1 { get; set; }
        public string? direccion2 { get; set; }
    }
}
