using MyEntityAssignment.Models;
using MyEntityAssignment.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MyEntityAssignment.Models.Leave;

namespace MyEntityAssignment.Handler
{
   public class RunHandler
    {
        public static void RunMyApp()
        {
            var employeeId= 1;
            Console.Clear();
            Console.WriteLine("\n Welcome to Application! \n\n" +
                              " What do you want to do?\n" +
                              "\n" +
                              " 1 - Apply for a leave.\n" +
                              " 2 - See my old leave-applications.\n" +
                              " 3 - See Leave-applications by Name.\n" +
                              " 4 - See Leave-Applications of a special month.\n" +
                              " 5 - See all Leave-Applications.\n");
            var answer = Console.ReadLine();
            if (answer == "1")
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("  ******Welcome To Leave-Application****** \n\n" +
                                      "Which type of leave do you want to apply for? Please type in the relevant number \n" +
                                      "[1] CareOfChild.\n" +
                                      "[2] SickLeave.\n" +
                                      "[3] PaidLeave. \n" +
                                      "[4] UnPaidLeave. \n");

                    var choice = Console.ReadLine();
                    LeaveCategory leavecategory = (LeaveCategory)Convert.ToInt32(choice);

                    Console.WriteLine("\nGood! Please enter the start date and write in this format: YYYY/MM/DD \n" +
                                      "For example: 2022/06/20");


                    DateTime start = Convert.ToDateTime(Console.ReadLine());

                    Console.WriteLine("Now enter the end date :");

                    DateTime finish = Convert.ToDateTime(Console.ReadLine());

                    using StaffDbContext context = new StaffDbContext();
                    _ = context.Leaves.Add(new Leave()
                    {
                        StartDate = start,
                        FinishDate = finish,
                        LeaveType = leavecategory,
                        RegistrationDate = DateTime.Now,
                        EmployeeId = employeeId

                    });

                    context.SaveChanges();
                    BackToMenu();
                }
                catch (Exception)
                {
                    
                    Console.WriteLine("Error! Please write the correct input");
                    BackToMenu();
                    throw;
                }
                
            }
            if (answer == "2")
            {
                using (var Staffdb = new StaffDbContext())
                {
                    var apps = from l in Staffdb.Leaves
                               where l.EmployeeId == employeeId
                               select l;
                    Console.Clear();
                    Console.WriteLine("You have applied for leaves on these dates:");
                    Console.WriteLine(new string('-', (50)));

                    foreach (var item in apps)
                    {
                        Console.WriteLine($"Leave-Category: {item.LeaveType} Start-date: " +
                            $"{item.StartDate.ToShortDateString()} End-date: {item.FinishDate.ToShortDateString()}");
                    }
                    Console.WriteLine(new string('-', (50)));
                    BackToMenu();

                }
                return;
            }
                if (answer == "3")
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Please choose Id for employee you want to see");
                    GetAllEmployees();
                    var selectedEmployee = int.Parse(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine($"Leave applications for {selectedEmployee}:");

                    using (var dbStaff = new StaffDbContext())
                    {
                        List<Leave> applications = dbStaff.Leaves.ToList();
                        var apps = from l in dbStaff.Leaves
                                   where l.EmployeeId == selectedEmployee
                                   select l;

                        foreach (var item in apps)
                        {
                            Console.WriteLine($"{item.Employee} {item.LeaveType} " +
                                $"Start date: {item.StartDate.ToShortDateString()} End date: " +
                                $"{item.FinishDate.ToShortDateString()}");
                        }
                        Console.WriteLine(new string('-', (50)));
                        BackToMenu();
                    }
                }
                catch (Exception)
                {

                    RunMyApp();
                    throw;
                }  
            }

            if (answer == "4")
            {
                try
                {
                    int month;
                    Array values = Enum.GetValues(typeof(Months));
                    Console.Clear();
                    Console.WriteLine("Please choose the month you want to see Leave-applications for:\n");

                    foreach (Months item in values)
                    {
                        Console.WriteLine($"{(int)item}.{Enum.GetName(typeof(Months), item)}");
                    }

                    month = int.Parse(Console.ReadLine());
                    if ((month < 0) || (month > 12))
                    {
                        Console.Clear();
                        Console.WriteLine("You have entered invalid data! Please enter and start again!");
                        Console.ReadLine();
                        BackToMenu();
                    }
                    else
                        using (var dbStaff = new StaffDbContext())
                        {
                            List<Employee> employees = dbStaff.Employees.ToList();
                            var apps = from l in dbStaff.Leaves
                                       where l.StartDate.Month == month
                                       select l;
                            foreach (var item in apps)
                            {
                                Console.WriteLine($"Employees: {item.EmployeeId} Leave-Category: {item.LeaveType}" +
                                    $" Start-date: {item.StartDate.ToShortDateString()} End-date: {item.FinishDate.ToShortDateString()}");
                            }
                            Console.WriteLine(new string('-', (50)));
                            BackToMenu();

                        }
                }
                catch (Exception)
                {
                    RunMyApp();
                    throw;
                }
            }
            if (answer == "5")
            {
                try
                {
                    Console.Clear();
                    GetAllEmployees();
                    Console.WriteLine("Current Leave-Applications\n");
                    using (var Staffdb = new StaffDbContext())
                    {
                        List<Leave> myList = Staffdb.Leaves.ToList();
                        foreach (Leave item in myList)
                        {
                            Console.WriteLine("Employee ID: {0} \t|Leave-Category: {1} \t|" +
                                " From: {2} \t|to: {3} |", item.EmployeeId, item.LeaveType, 
                                item.StartDate.ToShortDateString(), item.FinishDate.ToShortDateString());
                        }
                    }
                    Console.WriteLine(new string('-', (50)));
                    BackToMenu();
                    return;
                }
                catch (Exception)
                {
                    RunMyApp();
                    throw;
                }
            }
            else if (answer != "1" || answer != "2" || answer != "3" || answer != "4" || answer != "5")
            {
                Console.Clear();
                Console.WriteLine("\nYou have entered invalid input!\n");
             
                BackToMenu();
            }
            
        }


        public static void GetAllEmployees()
        {
            try
            {
                using (var dbStaff = new StaffDbContext())
                {
                    List<Employee> employees = dbStaff.Employees.ToList();
                    foreach (Employee emp in employees)
                    {
                        Console.WriteLine("Namn: {0}\t Id: {1}", emp.Name, emp.EmployeeId);
                    }
                }
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("There is no employee in database");
                throw;
            }
        }

        public static void BackToMenu()
        {
            Console.WriteLine("Click any key to start");
            Console.ReadKey();
            RunMyApp();
        }
    }
}
