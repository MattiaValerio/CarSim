using CarSim.BackEnd.Context;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace CarSim.BackEnd.Models;

public class Car
{
    [Key]
    public string Plate { get; set; }
    public int Wheels { get; } = 4; // 4 wheels
    public int Engine { get; set; }
    public CarBody Body { get; set; } // material of the car body
    public int SteeringWheel { get; set; } // degrees of steering wheel
    public bool BrakePedal { get; set; } // when true, car is braking
    public int Tank { get; set; } // percentage of fuel
    public CarFuelType FuelType { get; set; } // type of fuel 
    public bool Accelerator { get; set; } // when true, car is accelerating
    public CarType Type { get; set; }
    public HornType Horn { get; set; }

    public int Speed { get; set; }


    private Random rnd = new Random();

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

        if (Type == CarType.truck)
        {
            Horn = HornType.trumpet;
        }
        else
        {

            if (rnd.Next(0, 9) == 0)
            {
                Horn = HornType.trumpet;
            }
            else
            {
                Horn = HornType.horn;
            }
        }

        Plate = GeneratePlate();
    }

    public string GeneratePlate()
    {
        // generate a random plate that must be unique
        string plate = "";
        for (int i = 0; i < 2; i++)
        {
            plate += (char) rnd.Next(65, 90);
        }
        plate += "-";

        for (int i = 0; i < 3; i++)
        {
            plate += rnd.Next(0, 9);
        }
        plate += "-";

        for (int i = 0; i < 2; i++)
        {
            plate += (char)rnd.Next(65, 90);
        }

        return plate;
    }

    public async Task Accelerate()
    {
        Accelerator = true;
        if (Type == CarType.truck)
        {
            Speed += 4;
            ConsumeFuel();
        }
        else if (Type == CarType.sport)
        {
            Speed += 7;
            ConsumeFuel();
        }
        else
        {
            Speed += 6;
            ConsumeFuel();
        }

        if (FuelType == CarFuelType.premiumFuel && Type != CarType.truck)
        {
            Speed += 1;
        }

        Accelerator = false;
        await Task.CompletedTask;
    }

    private void ConsumeFuel()
    {
        if (Type == CarType.sport || Type == CarType.truck)
        {
            if (Tank <= 5)
            {
                Tank = 0;
            }
            else
            {
                Tank -= 5;
            }
        }
        else
        {
            if (Tank <= 3)
            {
                Tank = 0;
            }
            else
            {
                Tank -= 3;
            }
        }
    }

    public async Task Break()
    {
        if (Type == CarType.truck)
        {
            if (Speed <= 6)
            {
                Speed = 0;
            }
            else
            {
                Speed -= 6;
            }
        }
        else
        {
            if (Speed <= 10)
            {
                Speed = 0;
            }
            else
            {
                Speed -= 10;
            }
        }

        await Task.CompletedTask;
    }

    public async Task steer(SteerDir direction, int degrees)
    {
        if (direction == SteerDir.destra)
        {
            if (SteeringWheel + degrees >= 720)
            {
                SteeringWheel = 720;
            }
            else
            {
                SteeringWheel += degrees;

            }
        }
        else
        {
            if (SteeringWheel - degrees <= -720)
            {
                SteeringWheel = -720;
            }
            else
            {
                SteeringWheel -= degrees;
            }
        }

        await Task.CompletedTask;
    }

    public async Task Fill(CarFuelType fuel)
    {
        Tank = 100;
        FuelType = fuel;

        await Task.CompletedTask;
    }

    public async Task<string> Honk()
    {
        switch(Horn)
        {
            case HornType.horn:
                return "BEEP!";
            case HornType.trumpet:
                return "DA-DA-DA-DA-DAAH!";
        }

        return "NO HORN MOUNTED";
    }
}

public enum CarType
{
    compact = 0,
    sport = 1,
    suv = 2,
    truck = 3
}

public enum CarFuelType
{
    gasoline = 0,
    premiumFuel = 1
}

public enum CarBody
{
    metal = 0,
    plastic = 1,
    carbonFiber = 2,
    aluminum = 3,
    steel = 4,
    titanium = 5
}

public enum SteerDir
{
    destra,
    sinistra
}

public enum HornType
{
    horn,
    trumpet
}
