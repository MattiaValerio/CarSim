using CarSim.BackEnd.Context;
using CarSim.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarSim.BackEnd.Services.CarServices
{
    public class CarService : ICarService
    {
        private DataContext _context;
        public CarService(DataContext context)
        {
            _context = context;
        }
        public bool checkplate(string plate)
        {
            return _context.Cars.Any(c => c.Plate == plate);
        }

        public async Task<ResponseMessage<String>> GenerateCar()
        {
            try
            {
                var car = new Car();

                // if the plate already exists, generate a new one
                while (checkplate(car.Plate))
                {
                    car.Plate = car.GeneratePlate();
                }

                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return new ResponseMessage<string>() { 
                    Message = "Car generated succesfully",
                    Data = car.Plate,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<string>()
                {
                    Message = "Car not generated",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<Car>> CreateCar(int engine, CarBody body, CarType carType, CarFuelType fuelType)
        {
            try
            {
                Car car = new Car
                {
                    Engine = engine,
                    Body = body,
                    Type = carType,
                    FuelType = fuelType
                };

                

                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return new ResponseMessage<Car>()
                {
                    Message = "Car created succesfully",
                    Data = car,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<Car>()
                {
                    Message = "Car not created",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<CarType>> GetCarType(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<CarType>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                return new ResponseMessage<CarType>()
                {
                    Message = $"Car type: {car.Type.ToString()}",
                    Data = car.Type,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CarType>()
                {
                    Message = "Car not found",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<Car>> Accelerate(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<Car>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                if (car.Tank == 0)
                {
                    car.Speed = 0;
                    await _context.SaveChangesAsync();
                    return new ResponseMessage<Car>()
                    {
                        Message = "The car is out of fuel, please fill it up",
                        Success = false
                    };
                }

                await car.Accelerate();

                await _context.SaveChangesAsync();

                return new ResponseMessage<Car>()
                {
                    Message = "Car accelerated",
                    Data = car,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<Car>()
                {
                    Message = "Error during acceleration",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<Car>> Break(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<Car>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                await car.Break();

                await _context.SaveChangesAsync();

                return new ResponseMessage<Car>()
                {
                    Message = "Car breaking",
                    Data = car,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<Car>()
                {
                    Message = "Error during breaking",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<Car>> Steer(string plate, SteerDir direction, int angles)
        {
            if (angles > 720)
            {
                return new ResponseMessage<Car>()
                {
                    Message = "Max steering angle is 720",
                    Success = false
                };
            }

            if (int.IsNegative(angles))
            {
                return new ResponseMessage<Car>{
                    Message = "Steering angle cannot be negative",
                    Success = false
                };
            }


            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<Car>{
                        Message = "Car not found",
                        Success = false
                    };
                }

                await car.steer(direction, angles);

                await _context.SaveChangesAsync();

                return new ResponseMessage<Car>{
                    Message = "Car steered",
                    Data = car,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<Car>{
                    Message = "Error during steering",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<int>> GetDirection(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<int>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                return new ResponseMessage<int>()
                {
                    Message = int.IsNegative(car.SteeringWheel) ?
                    $" The car is going to the left at {car.SteeringWheel} degrees" :
                    $" The car is going to the right at {car.SteeringWheel} degrees",
                    Data = car.SteeringWheel,
                    Success = true
                };

            }
            catch (Exception ex)
            {
                return new ResponseMessage<int>()
                {
                    Message = $"Error during steering \n {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<int>> GetSpeed(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<int>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                return new ResponseMessage<int>()
                {
                    Message = car.Speed < 130 ?
                    $"You are going at {car.Speed} km/h" :
                    $"You are going at {car.Speed} km/h, slow down!",
                    Data = car.Speed,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<int>()
                {
                    Message = $"Error during steering: \n {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<Car>> FillCar(string plate, CarFuelType fuel)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<Car>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                await car.Fill(fuel);

                await _context.SaveChangesAsync();

                return new ResponseMessage<Car>()
                {
                    Message = "Car filled",
                    Data = car,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<Car>()
                {
                    Message = $"Error during filling: \n {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<string>> Honk(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<String>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                var resp = await car.Honk();

                return new ResponseMessage<string>()
                {
                    Message = car.Type != CarType.truck ? "Car honked!" : "Truck honked!",
                    Data = await car.Honk(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<String>()
                {
                    Message = $"Error during honking: \n {ex.Message}",
                    Success = false
                };
            }
        }

       
    }
}
