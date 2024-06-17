using CarSim.BackEnd.Context;
using CarSim.BackEnd.lib;
using CarSim.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSim.BackEnd.Controller
{
    [Route("api/")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private DataContext _context;
        private Utility _utils;
        public CarController(DataContext context, Utility utils)
        {
            _context = context;
            _utils = utils;
        }

        // Create a new car and return the plate
        [Route("GenerateCar")]
        [HttpPost]
        public async Task<ActionResult<String>> GenerateCar()
        {
            try
            {
                var car = new Car();

                // if the plate already exists, generate a new one
                while (_utils.CheckPlate(car.Plate))
                {
                    car.Plate = car.GeneratePlate();
                }

                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return Ok(car.Plate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CreateCar")]
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar(int engine, CarBody body, CarType carType, CarFuelType fuelType)
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

                while (_utils.CheckPlate(car.Plate))
                {
                    car.Plate = car.GeneratePlate();
                }

                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return car;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetType")]
        [HttpGet]
        public async Task<ActionResult<CarType>> GetCarType(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: "+ plate);
                }

                return Ok(car.Type.ToString());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Accelerate")]
        [HttpPost]
        public async Task<ActionResult<Car>> Accelerate(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: " + plate);
                }

                if(car.Tank == 0)
                {
                    car.Speed = 0;
                    await _context.SaveChangesAsync();
                    return BadRequest("The car is out of fuel, please fill it up");
                }

                await car.Accelerate();

                await _context.SaveChangesAsync();

                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("Break")]
        [HttpPost]
        public async Task<ActionResult<Car>> Break(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: " + plate);
                }

                await car.Break();

                await _context.SaveChangesAsync();

                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("Steer")]
        [HttpPost]  
        public async Task<ActionResult<Car>> Steer(string plate, SteerDir direction, int angles)
        {
            if(angles > 720)
            {
                return BadRequest("Max steering angle is 720");
            }

            if(int.IsNegative(angles))
            {
                return BadRequest("Steering angle must be positive");
            }


            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: " + plate);
                }

                await car.steer(direction, angles);

                await _context.SaveChangesAsync();

                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetSpeed")]
        [HttpGet]
        public async Task<ActionResult<int>> GetSpeed(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: " + plate);
                }

                return Ok(car.Speed < 130 ? $"You are going at {car.Speed} km/h" : $"You are going at {car.Speed} km/h, slow down!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetDirection")]
        [HttpGet]
        public async Task<ActionResult<int>> GetDirection(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: " + plate);
                }

                return Ok(int.IsNegative(
                    car.SteeringWheel) ? 
                    $" The car is going to the left at {car.SteeringWheel} degrees" : 
                    $" The car is going to the right at {car.SteeringWheel} degrees"
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("FillCar")]
        [HttpPost]
        public async Task<ActionResult<Car>> FillCar(string plate, CarFuelType fuel)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: " + plate);
                }

                await car.Fill(fuel);

                await _context.SaveChangesAsync();

                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Honk")]
        [HttpPost]
        public async Task<ActionResult<Car>> Honk(string plate)
        {
            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(x => x.Plate == plate);

                if (car == null)
                {
                    return NotFound("PLATE NOT FOUND: " + plate);
                }

                await car.Honk();

                await _context.SaveChangesAsync();

                return Ok(car);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
