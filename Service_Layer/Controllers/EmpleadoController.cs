using Microsoft.AspNetCore.Mvc;

namespace Service_Layer.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            Model_Layer.Result result = Business_Layer.Empleado.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else { return NotFound(result); }
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] Model_Layer.Empleado empleado)
        {
            Model_Layer.Result result = Business_Layer.Empleado.Add(empleado);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else { return NotFound(result); }
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Model_Layer.Empleado empleado = new Model_Layer.Empleado();

            empleado.IdEmpleado = id;

            Model_Layer.Result result = Business_Layer.Empleado.Delete(empleado);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else { return NotFound(result); }
        }

        [HttpPut("Update/{idEmpleado}")]
        public IActionResult Update(int idEmpleado, [FromBody] Model_Layer.Empleado empleado)
        {
            Model_Layer.Result result = Business_Layer.Empleado.Update(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else { return NotFound(result); }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            Model_Layer.Result result = Business_Layer.Empleado.GetById(id);

            if (result.Correct)
            {
                return Ok(result);
            }
            else { return NotFound(result); }
        }
    }
}