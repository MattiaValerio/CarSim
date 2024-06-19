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
        
        // Verify if the plate already exists
        public bool checkplate(string plate)
        {
            return _context.Cars.Any(c => c.Plate == plate);
        }

        public async Task<ResponseMessage<CarDto>> GenerateCar()
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

                return new ResponseMessage<CarDto>() { 
                    Message = "Car generated succesfully",
                    Data = car.CreateCarDto(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CarDto>()
                {
                    Message = $"Car not generated: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<CarDto>> CreateCar(int engine, CarBody body, CarType carType, CarFuelType fuelType)
        {
            try
            {
                if(engine < 1000 || engine > 5000)
                {
                    return new ResponseMessage<CarDto>()
                    {
                        Message = "Engine must be between 1000 and 5000",
                        Success = false
                    };
                }

                Car car = new Car
                {
                    Engine = engine,
                    Body = body,
                    Type = carType,
                    FuelType = fuelType
                };

                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return new ResponseMessage<CarDto>()
                {
                    Message = "Car created succesfully",
                    Data = car.CreateCarDto(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CarDto>()
                {
                    Message = $"Car not created: {ex.Message}",
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
                    Message = $"Car not found: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<CarDto>> Accelerate(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<CarDto>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                if (car.Tank == 0)
                {
                    car.Speed = 0;
                    await _context.SaveChangesAsync();
                    return new ResponseMessage<CarDto>()
                    {
                        Message = "The car is out of fuel, please fill it up",
                        Success = false
                    };
                }

                await car.Accelerate();

                await _context.SaveChangesAsync();

                return new ResponseMessage<CarDto>()
                {
                    Message = "Car accelerated",
                    Data = car.CreateCarDto(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CarDto>()
                {
                    Message = $"Error during acceleration: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<CarDto>> Break(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<CarDto>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                await car.Break();

                await _context.SaveChangesAsync();

                return new ResponseMessage<CarDto>()
                {
                    Message = "Car breaking",
                    Data = car.CreateCarDto(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CarDto>()
                {
                    Message = $"Error during breaking: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<CarDto>> Steer(string plate, SteerDir direction, int angles)
        {
            if (angles > 720)
            {
                return new ResponseMessage<CarDto>()
                {
                    Message = "Max steering angle is 720",
                    Success = false
                };
            }

            if (int.IsNegative(angles))
            {
                return new ResponseMessage<CarDto>
                {
                    Message = "Steering angle cannot be negative",
                    Success = false
                };
            }


            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<CarDto>
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                await car.steer(direction, angles);

                await _context.SaveChangesAsync();

                return new ResponseMessage<CarDto>
                {
                    Message = "Car steered",
                    Data = car.CreateCarDto(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<CarDto>
                {
                    Message = $"Error during steering: {ex.Message}",
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
                    $" The car is going to the left, the steering wheel is rotated by {car.SteeringWheel} degrees" :
                    $" The car is going to the right, the steering wheel is rotated by {car.SteeringWheel} degrees",
                    Data = car.SteeringWheel,
                    Success = true
                };

            }
            catch (Exception ex)
            {
                return new ResponseMessage<int>()
                {
                    Message = $"Error while getting the direction: {ex.Message}",
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
                    Message = $"Error during steering: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<string>> FillCar(string plate, CarFuelType fuel)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return new ResponseMessage<string>()
                    {
                        Message = "Car not found",
                        Success = false
                    };
                }

                if(car.Tank == 100)
                {
                    return new ResponseMessage<string>()
                    {
                        Message = "The car is already full",
                        Success = false
                    };
                }

                await car.Fill(fuel);

                await _context.SaveChangesAsync();

                return new ResponseMessage<string>()
                {
                    Message = "Car filled",
                    Data = $" Car thank is now filled at 100%",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseMessage<string>()
                {
                    Message = $"Error during filling: {ex.Message}",
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
                    Message = $"Error during honking: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ResponseMessage<List<CarDto>>> GetAllCars()
        {
            try
            {
                var cars = _context.Cars.ToList();

                if (cars.Count == 0)
                {
                    return new ResponseMessage<List<CarDto>>()
                    {
                        Message = "There is no cars saved",
                        Success = false
                    };
                }

                return new ResponseMessage<List<CarDto>>()
                {
                    Success = true,
                    Data = cars.Select(x => x.CreateCarDto()).ToList(),
                    Message = $"Found {cars.Count()} saved"
                };

            }
            catch (Exception ex) {
                return new ResponseMessage<List<CarDto>>()
                {
                    Message = $"Error during getting all cars: {ex.Message}",
                    Success = false
                };
            }
        }
    }
}
