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

            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model Data is Invalid";


                }
                bool result = _employeeDAL.Insert(employee);
                if (!result)
                {

                    TempData["errorMessage"] = "Failed to Add Employee";
                    return View();
                }
                else
                {
                    TempData["successMessage"] = "Employee Added Successfully";
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Employee employee = _employeeDAL.GetEmployeeById(id);
                if (employee.ID == 0)
                {

                    TempData["errorMessage"] = $"Employee Not Found with id : {id}";
                    return RedirectToAction("Index");
                }
                return View(employee);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model Data is Invalid";
                    return View();
                }
                bool result = _employeeDAL.Update(employee);
                if (!result)
                {
                    TempData["errorMessage"] = "Failed to Update Employee";
                    return View();
                }
                else
                {
                    TempData["successMessage"] = "Employee Updated Successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                bool result = _employeeDAL.Delete(id);
                if (!result)
                {
                    TempData["errorMessage"] = "Failed to Delete Employee";
                }
                else
                {
                    TempData["successMessage"] = "Employee Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

        }





    }
}
