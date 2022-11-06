using ProjectTest.DB;
using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTest.Operations
{
    public class EmployeeOperations
    {
        public static List<EmployeeModel> GetEmployees()
        {
            var db=new ZeroHungerDBEntities();
            var employees=new List<EmployeeModel>();

            foreach (var employee in db.Employees)
            {
                employees.Add(new EmployeeModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    DOB = employee.DOB,
                    Gender = employee.Gender,
                    Picture = employee.Picture,
                    User_Id = employee.User_Id
                });
            }

            return employees;
        }

        public static EmployeeModel GetEmployee(int id)
        {
            var db = new ZeroHungerDBEntities();
            var employee=new EmployeeModel();
            var emp=db.Employees.FirstOrDefault(x => x.Id == id);

            employee.Name = emp.Name;
            employee.Id = id;
            employee.Picture = emp.Picture;
            employee.Gender = emp.Gender;
            employee.DOB = emp.DOB;
            employee.User_Id=emp.User_Id;

            return employee;
        }

        public static void CreateEmployee(EmployeeModel emp)
        {
            var db = new ZeroHungerDBEntities();
            db.Employees.Add(new Employee()
            {
                Id= emp.Id,
                Name= emp.Name,
                Gender = emp.Gender,
                Picture= emp.Picture,
                DOB= emp.DOB,
                User_Id= emp.User_Id
            });

            db.SaveChanges();
        }

        public static List<UserEmployee> GetUnApprovedEmployees()
        {
            var db = new ZeroHungerDBEntities();
            var emps = new List<UserEmployee>();
            var unapproved = (from u in db.Users
                              join e in db.Employees on u.Id equals e.User_Id
                              where u.Is_Approved == "No"
                              select new
                              {
                                  empUsername = u.Username,
                                  empEmail = u.Email,
                                  empName = e.Name,
                                  empDOB = e.DOB,
                                  empGender=e.Gender,
                                  app_status = u.Is_Approved
                              }).ToList();

            foreach (var employee in unapproved)
            {
                emps.Add(new UserEmployee()
                {
                    Username = employee.empUsername,
                    Email = employee.empEmail,
                    Name = employee.empName,
                    DOB = employee.empDOB,
                    Gender = employee.empGender,
                    Is_Approved = employee.app_status
                });
            }

            return emps;
        }
    }
}