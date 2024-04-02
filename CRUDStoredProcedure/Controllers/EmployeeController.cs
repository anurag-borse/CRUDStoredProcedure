using CRUDStoredProcedure.DAL;
using CRUDStoredProcedure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDStoredProcedure.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Employee_DAL _employeeDAL;

        public EmployeeController(Employee_DAL employee_DAL)
        {
            
            _employeeDAL = employee_DAL;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = _employeeDAL.GetAll();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return View();
        }
    }
}
