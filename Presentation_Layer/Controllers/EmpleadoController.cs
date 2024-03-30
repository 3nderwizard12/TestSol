using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Presentation_Layer.Controllers
{
    public class EmpleadoController : Controller
    {
        private IHostingEnvironment _environment;
        private IConfiguration _configuration;

        public EmpleadoController(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            Model_Layer.Empleado resultEmpleado = new Model_Layer.Empleado();

            resultEmpleado.Empleados = new List<object>();

            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Model_Layer.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        Model_Layer.Empleado ResultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_Layer.Empleado>(resultItem.ToString());
                        resultEmpleado.Empleados.Add(ResultItemList);
                    }
                }
            }
            return View(resultEmpleado);
        }

        [HttpGet]
        public ActionResult Form(int? idEmpleado)
        {
            Model_Layer.Empleado empleado = new Model_Layer.Empleado();

            if (idEmpleado == null)
            {
                return View(empleado);
            }
            else
            {
                Model_Layer.Result result = new Model_Layer.Result();
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlWebApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("GetById/" + idEmpleado);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadFromJsonAsync<Model_Layer.Result>();
                        readTask.Wait();

                        Model_Layer.Empleado resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_Layer.Empleado>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        empleado = (Model_Layer.Empleado)result.Object;
                    }
                }
                return View(empleado);
            }
        }

        [HttpPost]
        public ActionResult Form(Model_Layer.Empleado empleado)
        {
            if (empleado.IdEmpleado == 0)
            {
                //add
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlWebApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PostAsJsonAsync<Model_Layer.Empleado>("Add", empleado);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Registro correctamente insertado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al actualizar el registro";
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                //update
                using (var client = new HttpClient())
                {
                    string urlApi = _configuration["urlWebApi"];
                    client.BaseAddress = new Uri(urlApi);

                    var postTask = client.PutAsJsonAsync<Model_Layer.Empleado>("Update/" + empleado.IdEmpleado, empleado);
                    postTask.Wait();

                    var result = postTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Registro correctamente actualizado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al actualizar el actualizado";
                        return PartialView("Modal");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int idEmpleado)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration["urlWebApi"];
                client.BaseAddress = new Uri(urlApi);

                var postTask = client.GetAsync("Delete/" + idEmpleado);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Registro correctamente Eliminado";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar el registro";
                    return PartialView("Modal");
                }
            }
        }
    }
}