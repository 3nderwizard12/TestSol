using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Business_Layer
{
	public class Empleado
	{
        public static Model_Layer.Result Add(Model_Layer.Empleado empleado)
        {
            Model_Layer.Result result = new Model_Layer.Result();

            try
            {
                using (DataAccess_Layer.DB_TestSolContext cnn = new DataAccess_Layer.DB_TestSolContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw(
                        "EXEC EmpleadoAdd @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Area, @FechaNacimiento, @Sueldo",
                        new SqlParameter("@Nombre", empleado.Nombre),
                        new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno),
                        new SqlParameter("@Area", empleado.Area),
                        new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento),
                        new SqlParameter("@Sueldo", empleado.Sueldo));

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while inserting the record in the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static Model_Layer.Result Delete(Model_Layer.Empleado empleado)
        {
            Model_Layer.Result result = new Model_Layer.Result();

            try
            {
                using (DataAccess_Layer.DB_TestSolContext cnn = new DataAccess_Layer.DB_TestSolContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw(
                        "EXEC EmpleadoDelete @IdEmpleado",
                        new SqlParameter("@IdEmpleado", empleado.IdEmpleado));

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while deleting the record in the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static Model_Layer.Result Update(Model_Layer.Empleado empleado)
        {
            Model_Layer.Result result = new Model_Layer.Result();

            try
            {
                using (DataAccess_Layer.DB_TestSolContext cnn = new DataAccess_Layer.DB_TestSolContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw(
                        "EXEC EmpleadoUpdate @IdEmpleado, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Area, @FechaNacimiento, @Sueldo",
                        new SqlParameter("@IdEmpleado", empleado.IdEmpleado),
                        new SqlParameter("@Nombre", empleado.Nombre),
                        new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno),
                        new SqlParameter("@Area", empleado.Area),
                        new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento),
                        new SqlParameter("@Sueldo", empleado.Sueldo));

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while updating the record in the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static Model_Layer.Result GetAll()
        {
            Model_Layer.Result result = new Model_Layer.Result();

            try
            {
                using (DataAccess_Layer.DB_TestSolContext cnn = new DataAccess_Layer.DB_TestSolContext())
                {
                    var query = cnn.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            Model_Layer.Empleado empleado = new Model_Layer.Empleado
                            {
                                IdEmpleado = row.IdEmpleado,
                                Nombre = row.Nombre,
                                ApellidoPaterno = row.ApellidoPaterno,
                                ApellidoMaterno = row.ApellidoMaterno,
                                Area = row.Area,
                                FechaNacimiento = row.FechaNacimiento.ToString(),
                                Sueldo = row.Sueldo
                            };

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while searching the record in the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static Model_Layer.Result GetById(int idEmpleado)
        {
            Model_Layer.Result result = new Model_Layer.Result();

            try
            {
                using (DataAccess_Layer.DB_TestSolContext cnn = new DataAccess_Layer.DB_TestSolContext())
                {
                    var query = cnn.Empleados.FromSqlRaw($"EmpleadoById {idEmpleado}").ToList().FirstOrDefault();

                    if (query != null)
                    {
                        Model_Layer.Empleado empleado = new Model_Layer.Empleado
                        {
                            IdEmpleado = query.IdEmpleado,
                            Nombre = query.Nombre,
                            ApellidoPaterno = query.ApellidoPaterno,
                            ApellidoMaterno = query.ApellidoMaterno,
                            Area = query.Area,
                            FechaNacimiento = query.FechaNacimiento.ToString(),
                            Sueldo = query.Sueldo
                        };
                        result.Object = empleado;

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error ocurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }
    }
}