using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSim.Shared.Models
{
    public class Car
    {
        public int wheels { get; } = 4; // 4 wheels
        public int Engine { get; set; }
        public CarBody Body { get; set; } // material of the car body
        public int SteeringWheel { get; set; } // degrees of steering wheel
        public bool BrakePedal { get; set; } // when true, car is braking
        public int Tank
        {
            get { return this.Tank; }
            set
            {   
                if (value > 100)
                {
                    this.Tank = 100;
                }

                else if (value < 0)
                {
                    this.Tank = 0;
                }
            }
        } // percentage of fuel
        public CarFuelType FuelType { get; set; } // type of fuel 
        public bool Accelerator { get; set; } // when true, car is accelerating
        public CarType Type { get; set; }


        private Random rnd = new Random();
        public Car CreateCar(Car car)
        {
            car.Tank = rnd.Next(0, 100);
            car.SteeringWheel = 0;
            car.Accelerator = false;
            car.BrakePedal = false;
            car.Engine = rnd.Next(1000, 3000);

            Array carBody = Enum.GetValues(typeof(CarBody));
            car.Body = (CarBody)carBody.GetValue(rnd.Next(carBody.Length));

            Array fuel = Enum.GetValues(typeof(CarFuelType));
            car.FuelType = (CarFuelType)fuel.GetValue(rnd.Next(fuel.Length));

            Array carModel = Enum.GetValues(typeof(CarType));
            car.Type = (CarType)carModel.GetValue(rnd.Next(carModel.Length));

            return car;
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


}
