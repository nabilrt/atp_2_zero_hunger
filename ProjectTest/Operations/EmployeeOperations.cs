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
                                  empId=e.Id,
                                  empUsername = u.Username,
                                  empEmail = u.Email,
                                  empName = e.Name,
                                  empDOB = e.DOB,
                                  empGender=e.Gender,
                                  empUserId=e.User_Id,
                                  app_status = u.Is_Approved
                              }).ToList();

            foreach (var employee in unapproved)
            {
                emps.Add(new UserEmployee()
                {
                    Id=employee.empId,
                    Username = employee.empUsername,
                    Email = employee.empEmail,
                    Name = employee.empName,
                    DOB = employee.empDOB,
                    Gender = employee.empGender,
                    User_Id=employee.empUserId,
                    Is_Approved = employee.app_status
                });
            }

            return emps;
        }

        public static List<UserEmployee> GetApprovedEmployees()
        {
            var db = new ZeroHungerDBEntities();
            var emps = new List<UserEmployee>();
            var approved = (from u in db.Users
                              join e in db.Employees on u.Id equals e.User_Id
                              where u.Is_Approved == "Yes"
                              select new
                              {
                                  empId = e.Id,
                                  empUsername = u.Username,
                                  empEmail = u.Email,
                                  empName = e.Name,
                                  empDOB = e.DOB,
                                  empGender = e.Gender,
                                  empUserId = e.User_Id,
                                  app_status = u.Is_Approved
                              }).ToList();

            foreach (var employee in approved)
            {
                emps.Add(new UserEmployee()
                {
                    Id = employee.empId,
                    Username = employee.empUsername,
                    Email = employee.empEmail,
                    Name = employee.empName,
                    DOB = employee.empDOB,
                    Gender = employee.empGender,
                    User_Id = employee.empUserId,
                    Is_Approved = employee.app_status
                });
            }

            return emps;
        }

        public static bool updateEmployee(UserEmployee ue)
        {
            var db = new ZeroHungerDBEntities();
            var user=(from u in db.Users where u.Id==ue.User_Id select u).SingleOrDefault();
            var emp = (from e in db.Employees where e.Id == ue.Id select e).SingleOrDefault();
            user.Username = ue.Username;
            user.Email = ue.Email;
            user.Password = ue.Password;
            emp.Name = ue.Name;
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public static bool updateDP(EmployeeModel emp)
        {
            var db = new ZeroHungerDBEntities();
            var employee = (from e in db.Employees where e.Id == emp.Id select e).SingleOrDefault();
            employee.Picture = emp.Picture;
            if (db.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}