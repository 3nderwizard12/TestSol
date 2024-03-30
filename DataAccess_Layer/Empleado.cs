using System;
using System.Collections.Generic;

namespace DataAccess_Layer
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string Area { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string Sueldo { get; set; } = null!;
    }
}
