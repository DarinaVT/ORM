using Microsoft.Data.SqlClient;
using System;

class Program
{
    public static void Main()
    {
        string dbName = "Shop";
        //string tableCustomers = "Customers";
        //string tableProducts = "Products";

        ////checks database's existence
        //Database exists = new Database();

        ////creates database if it doesn't exist yet
        //if (!exists.DatabaseExistence(dbName))
        //{
        //    Database create = new Database();
        //    create.CreateDatabase(dbName);
        //}

        ////creates table for customers
        //Tables customers = new Tables(dbName);
        //customers.CreateTableCustomers(tableCustomers);

        ////creates table for products
        //Tables products = new Tables(dbName);
        //products.CreateTableProducts(tableProducts);

        //////adds data to tables
        Data data = new Data(dbName);

        //data.CustomerData("Customers", "Liam Blackwood");
        //data.CustomerData("Customers", "Seraphina Caldwell");
        //data.CustomerData("Customers", "Dorian Holloway");
        //data.CustomerData("Customers", "Elowen Vance");
        //data.CustomerData("Customers", "Cassius Thorne");

        //data.ProductsData("Products", "Mechanical Gaming Keyboard", 1);
        //data.ProductsData("Products", "4K Ultrawide Monitor", 2);
        //data.ProductsData("Products", "Noise-Canceling Studio Headphones", 3);
        //data.ProductsData("Products", "Mechanical Gaming Keyboard", 4);
        //data.ProductsData("Products", "Smart LED Desk Lamp", 5);
        //data.ProductsData("Products", "4K Ultrawide Monitor", 3);
        data.ProductsData("Products", "", 3);
        data.ProductsData("Products", "Apple AirPods 3rd gen", 2);

        //execute query
        Queries queries = new Queries(dbName);
        queries.TimesBought();
    }
}

class Database
{
    private string ConnectionString { get; set; } = "Server=DESKTOP-ENRVS12;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

    public bool DatabaseExistence(string database)
    {
        string cmd = "IF EXISTS (SELECT 1 FROM sys.databases WHERE name = @database) SELECT 1 ELSE SELECT 0";

        using (SqlConnection sql = new SqlConnection(ConnectionString))
        {
            sql.Open();
            using (SqlCommand sqlCommand = new SqlCommand(cmd, sql))
            {
                sqlCommand.Parameters.AddWithValue("@database", database);
                return Convert.ToInt32(sqlCommand.ExecuteScalar()) == 1;
            }
        }
    }
    public void CreateDatabase(string database)
    {
        string cmd = $"CREATE DATABASE {database}";

        using (SqlConnection sql = new SqlConnection(ConnectionString))
        {
            sql.Open();
            using (SqlCommand sqlCommand = new SqlCommand(cmd, sql))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Database created");
            }
        }
    }
}

class Tables
{
    private string ConnectionString { get; set; }
    public Tables(string database)
    {
        ConnectionString = $"Server=DESKTOP-ENRVS12;Database={database};Trusted_Connection=True;TrustServerCertificate=True;";
    }
    public void CreateTableCustomers(string tableName)
    {
        string cmd = $@"CREATE TABLE {tableName} 
                      (
                      ID INT PRIMARY KEY IDENTITY(1,1),
                      Name VARCHAR(50)
                      )";

        using (SqlConnection sql = new SqlConnection(ConnectionString))
        {
            try
            {
                sql.Open();
                SqlCommand sqlCommand = new SqlCommand(cmd, sql);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }
    }

    public void CreateTableProducts(string tableName)
    {
        string cmd = $@"CREATE TABLE {tableName}    
                      (
                      ID INT PRIMARY KEY IDENTITY(1,1),
                      ProductName VARCHAR(100),
                      BuyerID INT,
                      FOREIGN KEY (BuyerID) REFERENCES Customers(ID)
                      )";

        using (SqlConnection sql = new SqlConnection(ConnectionString))
        {
            try
            {
                sql.Open();
                SqlCommand sqlCommand = new SqlCommand(cmd, sql);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }
    }
}

class Data
{
    private string ConnectionString { get; set; }
    public Data(string database)
    {
        ConnectionString = $"Server=DESKTOP-ENRVS12;Database={database};Trusted_Connection=True;TrustServerCertificate=True;";
    }
    public void CustomerData(string tableName, string Name)
    {
        string cmd = $"INSERT INTO {tableName} (Name) VALUES (@Name)";

        using (SqlConnection sql = new SqlConnection(ConnectionString))
        {
            try
            {
                sql.Open();
                SqlCommand sqlCommand = new SqlCommand(cmd, sql);
                sqlCommand.Parameters.AddWithValue("@Name", Name);
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"Data inserted into {tableName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }
    }
    public void ProductsData(string tableName, string productName, int BuyerID)
    {
        string cmd = $"INSERT INTO {tableName} (ProductName, BuyerID) VALUES (@ProductName, @BuyerID)";
        //rollback when productName is null
        using (SqlConnection sql = new SqlConnection(ConnectionString))
        {
            sql.Open();
            using (SqlTransaction transaction = sql.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(productName))
                    {
                        throw new ArgumentException("Product name cannot be null, rolling back transaction");
                    }
                    using (SqlCommand sqlCommand = new SqlCommand(cmd, sql, transaction))
                    {
                        sqlCommand.Parameters.AddWithValue("@ProductName", productName);
                        sqlCommand.Parameters.AddWithValue("@BuyerID", BuyerID);
                        sqlCommand.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    Console.WriteLine($"Data inserted into {tableName}");
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    sql.Close();
                }
            }
        }
    }
}
class Queries
{
    private string ConnectionString { get; set; }
    public Queries(string database)
    {
        ConnectionString = $"Server=DESKTOP-ENRVS12;Database={database};Trusted_Connection=True;TrustServerCertificate=True;";
    }
    public void TimesBought()
    {
        string cmd = "SELECT ProductName, COUNT(*) AS TimesBought\r\nFROM Products\r\nGROUP BY ProductName\r\nORDER BY TimesBought DESC";
        using (SqlConnection sql = new SqlConnection(ConnectionString))
        {
            try
            {
                sql.Open();
                SqlCommand sqlCommand = new SqlCommand(cmd, sql);
                var data = sqlCommand.ExecuteReader();
                while (data.Read())
                {
                    string productName = data.GetString(0);
                    int amount = data.GetInt32(1);
                    Console.WriteLine($"{productName} was bought {amount} time(s)");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }
    }
}
