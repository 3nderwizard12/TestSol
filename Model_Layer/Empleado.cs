using System;
namespace Model_Layer
{
	public class Empleado
	{
        public int IdEmpleado { get; set; }

        public string Nombre { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public string Area { get; set; } = null!;

        public string? FechaNacimiento { get; set; }

        public string Sueldo { get; set; } = null!;

        public List<object>? Empleados { get; set; }
    }
}