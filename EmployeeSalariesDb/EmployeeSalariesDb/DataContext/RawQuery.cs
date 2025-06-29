using Infrastructure;
using Microsoft.Data.SqlClient;

namespace DataContext;

public class RawQuery
{
    public void RawQueryCreateTable()
    {
        string createTableSql = @"
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OvertimeReport' AND xtype='U')
        BEGIN
            CREATE TABLE OvertimeReport
            (
                EmployeeId INT,
                Gender NVARCHAR(10),
                BaseSalary FLOAT,
                OvertimePay FLOAT,
                LongevityPay FLOAT,
                Grade NVARCHAR(10),
                Division NVARCHAR(100),
                Department NVARCHAR(100)
            );
        END";

        using (var connection = new SqlConnection(GlobalParam.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(createTableSql, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Table OvertimeReport created (if it didn't exist).");
            }
        }
    }
    public void RawQueryInsertInto()
    {
        string sql = @"
        INSERT INTO OvertimeReport (EmployeeId, Gender, BaseSalary, OvertimePay, LongevityPay, Grade, Division, Department)
        SELECT e.Id, e.Gender, e.BaseSalary, e.OvertimePay, e.LongevityPay, e.Grade, d.Division, dep.Department
        FROM Employees e
        INNER JOIN Divisions d ON e.DivisionNameId = d.Id
        INNER JOIN Departments dep ON e.DepartmentNameId = dep.Id
        WHERE e.OvertimePay > @OvertimeThreshold";

        using (var connection = new SqlConnection(GlobalParam.ConnectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@OvertimeThreshold", 1000);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} rows inserted into OvertimeReport.");
            }
        }

    }
}