using CarSim.BackEnd.Context;
using System.ComponentModel.DataAnnotations;

namespace CarSim.BackEnd.Models;

public class Car
{
    [Key]
    public string Plate { get; set; }
    public int wheels { get; } = 4; // 4 wheels
    public int Engine { get; set; }
    public CarBody Body { get; set; } // material of the car body
    public int SteeringWheel { get; set; } // degrees of steering wheel
    public bool BrakePedal { get; set; } // when true, car is braking
    public int Tank { get; set; }// percentage of fuel
    public CarFuelType FuelType { get; set; } // type of fuel 
    public bool Accelerator { get; set; } // when true, car is accelerating
    public CarType Type { get; set; }


    private Random rnd = new Random();
    private DataContext _context;

    public Car()
    {
        Tank = rnd.Next(0, 100);
        SteeringWheel = 0;
        Accelerator = false;
        BrakePedal = false;
        Engine = rnd.Next(1000, 3000);

        Array carBody = Enum.GetValues(typeof(CarBody));
        Body = (CarBody) carBody.GetValue(rnd.Next(carBody.Length));

        Array fuel = Enum.GetValues(typeof(CarFuelType));
        FuelType = (CarFuelType) fuel.GetValue(rnd.Next(fuel.Length));

        Array carModel = Enum.GetValues(typeof(CarType));
        Type = (CarType) carModel.GetValue(rnd.Next(carModel.Length));

        Plate = GeneratePlate();
    }

    public string GeneratePlate()
    {
        // generate a random plate that must be unique
        string plate = "";
        for (int i = 0; i < 3; i++)
        {
            plate += (char)rnd.Next(65, 90);
        }
        plate += "-";
        for (int i = 0; i < 3; i++)
        {
            plate += rnd.Next(0, 9);
        }
        
        return plate;
    }

    public async Task Accelerate()
    {
        Accelerator = true;
        await Task.Delay(1000);
        Accelerator = false;
    }
}

public enum CarType
{
    compact,
    sport,
    suv,
    truck
}

public enum CarFuelType
{
    gasoline,
    premiumFuel
}

public enum CarBody
{
    metal,
    plastic,
    carbonFiber,
    aluminum,
    steel,
    titanium
}
