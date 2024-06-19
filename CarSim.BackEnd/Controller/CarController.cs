using CarSim.BackEnd.Context;
using CarSim.BackEnd.Models;
using CarSim.BackEnd.Services.CarServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSim.BackEnd.Controller
{
    [Route("api/")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private DataContext _context;
        private ICarService _carService;

        public CarController(DataContext context, ICarService carService)
        {
            _context = context;
            _carService = carService;
        }

        // Create a new car and return the plate
        [Route("GetAllCars")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<string>>> GetAllCars()
        {
            var resp = await _carService.GetAllCars();

            if (!resp.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        // Create a new car and return the plate
        [Route("GenerateCar")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<string>>> GenerateCar()
        {
            var resp = await _carService.GenerateCar();

            if (!resp.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [Route("CreateCar")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<CarDto>>> CreateCar(int engine, CarBody body, CarType carType, CarFuelType fuelType)
        {
            var resp = await _carService.CreateCar(engine, body, carType, fuelType);

            if (!resp.Success)
            {
                return BadRequest(resp.Message);
            }

            return Ok(resp);
        }

        [Route("GetType")]
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<CarType>>> GetCarType(string plate)
        {
            var resp = await _carService.GetCarType(plate);

            if (!resp.Success)
            {
                return BadRequest(resp.Message);
            }

            return Ok(resp);
        }

        [Route("Accelerate")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<CarDto>>> Accelerate(string plate)
        {
            var resp = await _carService.Accelerate(plate);

            if (!resp.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [Route("Break")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<CarDto>>> Break(string plate)
        {
            var resp = await _carService.Break(plate);

            if (!resp.Success)
            {
                return BadRequest(resp.Message);
            }

            return Ok(resp);
        }

        [Route("Steer")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<CarDto>>> Steer(string plate, SteerDir direction, int angles)
        {
            var resp = await _carService.Steer(plate, direction, angles);

            if (!resp.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [Route("GetSpeed")]
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<int>>> GetSpeed(string plate)
        {
            var resp = await _carService.GetSpeed(plate);

            if (!resp.Success)
            {
                return BadRequest(resp);
            }

            return Ok(resp);
        }

        [Route("GetDirection")]
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<int>>> GetDirection(string plate)
        {
            var resp = await _carService.GetDirection(plate);

            if (!resp.Success)
            {
                return BadRequest(resp.Message);
            }

            return Ok(resp);
        }


        [Route("FillCar")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<string>>> FillCar(string plate, CarFuelType fuel)
        {
            var resp = await _carService.FillCar(plate, fuel);

            if (!resp.Success)
            {
                return BadRequest(resp.Message);
            }

            return Ok(resp);
        }

        [Route("Honk")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<CarDto>>> Honk(string plate)
        {
            var resp = await _carService.Honk(plate);

            if (!resp.Success)
            {
                return BadRequest(resp.Message);
            }

            return Ok(resp);
        }
    }
}
