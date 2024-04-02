using CRUDStoredProcedure.Models;
using System.Data;
using System.Data.SqlClient;

namespace CRUDStoredProcedure.DAL
{
    public class Employee_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetAll()
        {
               List<Employee> employeeList = new List<Employee   >();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Get_Employees]";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();

                while(dr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(dr["ID"]);
                    employee.Firstname = dr["Firstname"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                    employee.Email = dr["Email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["Salary"]);
                    employeeList.Add(employee);
                }
                _connection.Close();
            }
            return employeeList;
        }

    }
}
