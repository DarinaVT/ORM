using System;
namespace CarCSV;
public class Program
{
    static void Main(string[] args)
    {
        var dbContext = new ApplicationDbContext();
        var reader = new ReaderCSV(dbContext);
        string fileName = @"D:\cars.csv";
        reader.Reader(fileName);

        //var carsBrandC = dbContext.GetCarsByBrandStartingWith('C').ToList();
        //Console.WriteLine($"Cars from brands starting with 'C': {carsBrandC.Count}");
        //carsBrandC.ForEach(c => Console.WriteLine($"{c.Brand?.Name} - {c.Model}"));

        //var carsAfter2015 = dbContext.GetCarsProducedAfter(2015).ToList();
        //Console.WriteLine($"\nCars produced after 2015: {carsAfter2015.Count}");

        //var eightCylinderCars = dbContext.GetCarsWithCylinders(8).ToList();
        //Console.WriteLine($"\n8-cylinder cars: {eightCylinderCars.Count}");
        //eightCylinderCars.ForEach(c => Console.WriteLine($"{c.Model} - {c.Cylinders} cylinders"));
    }
}