using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataContext;

public class DataSeed
{
    private readonly CarsDbContext _context;
    private readonly ICsvFileReader _reader;

    public Dictionary<(string Make, string Model, string Year), MakeModelYear> MakeDict { get; set; } = new();
    public Dictionary<string, Location> LocationDict { get; set; } = new();
    public Dictionary<string, ElectricUtility> UtilitiesDict { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public DataSeed(CarsDbContext context, ICsvFileReader reader)
    {
        _context = context;
        _reader = reader;
    }
    public void LoadData()
    {
        if (_context.ElectricCars.Any())
        {
            return;

        }

        var carsFromFile = _reader.GetData().ToList();
        MakeDict = _context.MakeModelYears
            .AsNoTracking()
            .ToDictionary(x => (x.Make, x.Model, x.ModelYear));

        LocationDict = _context.Locations
            .AsNoTracking()
            .ToDictionary(x => x.PostalCode);

        UtilitiesDict = _context.ElectricUtilities
            .AsNoTracking()
            .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

        var newMakes = new List<MakeModelYear>();
        var newLocations = new List<Location>();
        var newUtilities = new List<ElectricUtility>();

        var carsToAdd = new List<ElectricCar>();
        var carUtilitiesToAdd = new List<ElectricVehicleUtility>();

        foreach (var record in carsFromFile)
        {
            var makeKey = (record.Make, record.ModelName, record.ModelYear);
            if (!MakeDict.TryGetValue(makeKey, out var make))
            {
                make = new MakeModelYear
                {
                    Make = record.Make,
                    Model = record.ModelName,
                    ModelYear = record.ModelYear
                };
                MakeDict[makeKey] = make;
                newMakes.Add(make);
            }

            var postalCode = record.PostalCode;
            if (!LocationDict.TryGetValue(postalCode, out var location))
            {
                var coords = DataClean.GetCoordinates(record.VehicleLocation);
                location = new Location
                {
                    State = record.State,
                    City = record.City,
                    County = record.County,
                    PostalCode = record.PostalCode,
                    Latitude = coords[1],
                    Longitude = coords[0]
                };
                LocationDict[postalCode] = location;
                newLocations.Add(location);
            }

            var car = new ElectricCar
            {
                Vin = record.VIN,
                DOLVehicleId = record.DOLVehicleID,
                MakeModelYear = make,
                Location = location,
                ElectricVehicleType = DataClean.ShortenType(record.ElectricVehicleType),
                CAFVEligibility = DataClean.ShortenEligibility(record.CAFV),
                ElectricRange = int.TryParse(record.ElectricRange, out var range) ? range : null,
                BaseMsrp = int.TryParse(record.BaseMSRP, out var msrp) ? msrp : null,
                LegislativeDistric = DataClean.GetDistrictCode(record.LegislativeDistrict),
                CensusTract2020 = record.CensusTract
            };
            carsToAdd.Add(car);

            var utilities = DataClean.GetUtilities(record.ElectricUtility);
            foreach (var utilityName in utilities)
            {
                if (!UtilitiesDict.TryGetValue(utilityName, out var utility))
                {
                    utility = new ElectricUtility { Name = utilityName };
                    UtilitiesDict[utilityName] = utility;
                    newUtilities.Add(utility);
                }

                var vehicleUtility = new ElectricVehicleUtility
                {
                    ElectricCar = car,
                    ElectricUtility = utility
                };
                carUtilitiesToAdd.Add(vehicleUtility);
            }
        }

        _context.AddRange(newMakes);
        _context.AddRange(newLocations);
        _context.AddRange(newUtilities);
        _context.SaveChanges();

        _context.AddRange(carsToAdd);
        _context.AddRange(carUtilitiesToAdd);

        _context.SaveChanges();
    }

}