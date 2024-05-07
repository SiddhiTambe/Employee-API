using Azure;
using FullStack.API.Data;
using FullStack.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        ////Get All Employee's Data
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDbContext.Employees.ToListAsync();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        ////Get Employee Data by Employee ID
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            employeeRequest.Id = Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        ////Update Employee Data
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeData(Employee employeeUpdateRequest)
        {
            var employeeData = await _fullStackDbContext.Employees.Where(a => a.Id == employeeUpdateRequest.Id).FirstOrDefaultAsync();
            if (employeeData == null)
            {
                return NotFound();
            }
            employeeData.Name = employeeUpdateRequest.Name;
            employeeData.Email = employeeUpdateRequest.Email;
            employeeData.Phone = employeeUpdateRequest.Phone;
            employeeData.Salary = employeeUpdateRequest.Salary;
            employeeData.Department = employeeUpdateRequest.Department;

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeeData);

        }

        ////Update Employee Data
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, Employee updateEmployeeRequest)
        {
            var employeeData = await _fullStackDbContext.Employees.FindAsync(id);
            if (employeeData == null)
            {
                return NotFound();
            }
            employeeData.Name = updateEmployeeRequest.Name;
            employeeData.Email = updateEmployeeRequest.Email;
            employeeData.Phone = updateEmployeeRequest.Phone;
            employeeData.Salary = updateEmployeeRequest.Salary;
            employeeData.Department = updateEmployeeRequest.Department;

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeeData);
        }

        ////Delete Employee Data

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmployeeData(Guid Id)
        {
            //Find the employee data by Id
            var employeeData = await _fullStackDbContext.Employees.Where(a => a.Id == Id).FirstOrDefaultAsync();
            if (employeeData == null)
            {
                return NotFound();
            }

            //Remove the employee data from the database
            _fullStackDbContext.Employees.Remove(employeeData);

            await _fullStackDbContext.SaveChangesAsync();
            return Ok(employeeData);
        }

    }

}
