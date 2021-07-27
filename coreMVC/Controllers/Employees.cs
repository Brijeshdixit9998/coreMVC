using coreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coreMVC.Controllers
{
    public class Employees : Controller
    {
        private readonly EmployeeContext _DBC;
        public Employees(EmployeeContext DB)
        {
            _DBC = DB;
        }
        public IActionResult EmpList()
        {
            var emplist = from a in _DBC.Employee
                          join b in _DBC.Departments
                          on a.DeptID equals b.ID
                          into Dep
                          from b in Dep.DefaultIfEmpty()
                          select new Employee
                          {
                              ID = a.ID,
                              Name = a.Name,
                              Fname = a.Fname,
                              Email = a.Email,
                              Mobile = a.Mobile,
                              DeptID = a.DeptID,
                              Description = a.Description,
                              Department = b == null ? "" : b.Department
                          };

            return View(emplist);
        }
        public IActionResult addemp(int ?id)
        {
            var Studentdata = _DBC.Employee.Where(x => x.ID == id).FirstOrDefault();
            deptddl();
            return View(Studentdata);
        }
        [HttpPost]
        public async Task<IActionResult> addemp(Employee emp)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    if (emp.ID == 0)
                    {
                        _DBC.Employee.Add(emp);
                        await _DBC.SaveChangesAsync();
                    }
                    else
                    {
                        _DBC.Entry(emp).State = EntityState.Modified;
                        await _DBC.SaveChangesAsync();
                    }
                    return RedirectToAction("EmpList");
                //}
            }
            catch (Exception ex)
            {
                return RedirectToAction("EmpList");
            }
        }
        public async Task<IActionResult> delete_emps(int id)
        {
            try
            {
                var emps = await _DBC.Employee.FindAsync(id);
                if (emps != null)
                {
                    _DBC.Remove(emps);
                    await _DBC.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
            }
            return RedirectToAction("EmpList");
        }
        public IActionResult add_dpt()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> add_dpt(Departments emp)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                if (emp.ID == 0)
                {
                    _DBC.Departments.Add(emp);
                    await _DBC.SaveChangesAsync();
                }
                else
                {
                    return RedirectToAction("add_dpt");
                }
                //}
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("add_dpt");
            }
        }
        private void deptddl()
        {
            List<Departments> dptlist = new List<Departments>();
            dptlist = _DBC.Departments.ToList();
            dptlist.Insert(0, new Departments { ID = 0, Department = "Please Select" });
            ViewBag.dptlist = dptlist;
        }
    }
        
}
