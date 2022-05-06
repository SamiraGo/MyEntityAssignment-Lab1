using MyEntityAssignment.Models;
using MyEntityAssignment.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MyEntityAssignment.Data
{
    public class FillData
    {
        public static void StartMyApp()
        {
            using StaffDbContext context = new StaffDbContext();
            {
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(new List<Employee>()
                    {
                        new Employee(){ Name = "Samira" },
                        new Employee(){ Name = "Ilian" },
                        new Employee(){ Name = "Nick" },
                        new Employee() { Name = "Nelly" }
                });

                    context.SaveChanges();
                }

                if (!context.Leaves.Any())
                {
                    context.Leaves.AddRange(new List<Leave>()
                    {
                        new Leave()
                        {
                            EmployeeId = 1,
                            RegistrationDate = DateTime.Now.AddDays(-5),
                            StartDate = DateTime.Now.AddDays(+1),
                            FinishDate = DateTime.Now.AddDays(+20),
                            LeaveType = LeaveCategory.CareOfChild
                        },
                        new Leave()
                        {
                            EmployeeId = 2,
                            RegistrationDate = DateTime.Now.AddDays(+10),
                            StartDate = DateTime.Parse("2022-08-12"),
                            FinishDate = DateTime.Parse("2023-08-24"),
                            LeaveType = LeaveCategory.PaidLeave
                        },
                       new Leave()
                        {
                           EmployeeId = 3,
                           RegistrationDate = DateTime.Now,
                           StartDate = DateTime.Now.AddDays(-5),
                           FinishDate = DateTime.Now.AddDays(+20),
                           LeaveType = LeaveCategory.SickLeave

                       },
                        new Leave()
                        {
                            EmployeeId = 4,
                            RegistrationDate = DateTime.Now.AddDays(+21),
                            StartDate = DateTime.Parse("2022-07-08"),
                            FinishDate = DateTime.Parse("2022-08-08"),
                            LeaveType = LeaveCategory.UnPaidLeave
                        }  
                    });
                    context.SaveChanges();
                }
              }
           }
        }
    }
