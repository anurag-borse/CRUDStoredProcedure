using CRUDStoredProcedure.Models;
using System.Data;
using System.Data.SqlClient;

namespace CRUDStoredProcedure.DAL
{
    public class Employee_DAL
    {

        //anurag controller



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


        public bool Insert(Employee model)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
              _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Insert_Employee]";
                _command.Parameters.AddWithValue("@Firstname", model.Firstname);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _connection.Open();
              id =   _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0? true:false;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Get_EmployeeById]";
                _command.Parameters.AddWithValue("@ID", id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    employee.ID = Convert.ToInt32(dr["ID"]);
                    employee.Firstname = dr["Firstname"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).Date;
                    employee.Email = dr["Email"].ToString();
                    employee.Salary = Convert.ToDouble(dr["Salary"]);
                }
                _connection.Close();
            }
            return employee;
        

        }

        public bool Update(Employee employee)
        {
            int id = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Update_Employee]";
                _command.Parameters.AddWithValue("@ID", employee.ID);
                _command.Parameters.AddWithValue("@Firstname", employee.Firstname);
                _command.Parameters.AddWithValue("@LastName", employee.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", employee.Email);
                _command.Parameters.AddWithValue("@Salary", employee.Salary);
                _connection.Open();
                id = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return id > 0 ? true : false;

        }

        public bool Delete(int id)
        {
            int result = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[dbo].[usp_Delete_Employee]";
                _command.Parameters.AddWithValue("@ID", id);
                _connection.Open();
                result = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return result > 0 ? true : false;
        }
    }
}
