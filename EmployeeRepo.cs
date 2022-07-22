using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Day33_EmployeePayroll_ADO.NET
{
    public class EmployeeRepo
    {

        //static as connection will be made only once in application
        List<EmployeeModel> employeeDetailsList = new List<EmployeeModel>();
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog =demoDB; Integrated Security = True;";
        SqlConnection connection = new SqlConnection(connectionString);

        public void GetAllemployee()
        {

            try
            {
                using (this.connection)
                {
                    string query = "select * from Persons";
                    SqlCommand sqlCommand = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = sqlCommand.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            EmployeeModel employeeModel = new EmployeeModel();
                            employeeModel.EmployeeID = dr.GetInt32(0);
                            employeeModel.EmployeeName = dr.GetString(1);
                            employeeModel.PhoneNumber = dr.GetString(2);
                            employeeModel.Department = dr.GetString(3);
                            employeeModel.Gender = dr.GetString(4);
                            employeeModel.Email = dr.GetString(5);
                            employeeModel.City = dr.GetString(6);
                            employeeModel.State = dr.GetString(7);
                            employeeModel.Country = dr.GetString(8);
                            employeeModel.Pay = dr.GetInt32(9);


                            //display retrieved record
                            employeeDetailsList.Add(employeeModel);


                        }
                        foreach (var employee in employeeDetailsList)
                        {
                            Console.WriteLine($"{employee.EmployeeID} | {employee.EmployeeName} | {employee.PhoneNumber} | {employee.Gender} | {employee.Department}");

                        }

                    }
                    else
                    {
                        throw new Exception("No data found");
                    }
                    dr.Close();
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }




        }
    }
}