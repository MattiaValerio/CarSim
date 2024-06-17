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
        [Route("GenerateCar")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<string>>> GenerateCar()
        {
            try
            {
                var resp = await _carService.GenerateCar();

                if(!resp.Success)
                {
                    return BadRequest(resp);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CreateCar")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<Car>>> CreateCar(int engine, CarBody body, CarType carType, CarFuelType fuelType)
        {
            try
            {
                var resp = await _carService.CreateCar(engine, body, carType, fuelType);

                if(!resp.Success)
                {
                    return BadRequest(resp.Message);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetType")]
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<CarType>>> GetCarType(string plate)
        {
            try
            {
                var resp = await _carService.GetCarType(plate);

                if(!resp.Success)
                {
                    return BadRequest(resp.Message);
                }

                return Ok(resp);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Accelerate")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<Car>>> Accelerate(string plate)
        {
            try
            {
                var resp = await _carService.Accelerate(plate);

                if(!resp.Success)
                {
                    return BadRequest(resp);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("Break")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<Car>>> Break(string plate)
        {
            try
            {
                var resp = await _carService.Break(plate);

                if(!resp.Success)
                {
                    return BadRequest(resp.Message);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("Steer")]
        [HttpPost]  
        public async Task<ActionResult<ResponseMessage<Car>>> Steer(string plate, SteerDir direction, int angles)
        {
            try
            {
                var resp = await _carService.Steer(plate, direction, angles);

                if(!resp.Success)
                {
                    return BadRequest(resp);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetSpeed")]
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<int>>> GetSpeed(string plate)
        {
            try
            {
                var resp = await _carService.GetSpeed(plate);

                if(!resp.Success)
                {
                    return BadRequest(resp);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetDirection")]
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<int>>> GetDirection(string plate)
        {
            try
            {
                var resp = await _carService.GetDirection(plate);

                if(!resp.Success)
                {
                    return BadRequest(resp.Message);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("FillCar")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<Car>>> FillCar(string plate, CarFuelType fuel)
        {
            try
            {
                var resp = await _carService.FillCar(plate, fuel);

                if (!resp.Success)
                {
                    return BadRequest(resp.Message);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("Honk")]
        [HttpPost]
        public async Task<ActionResult<ResponseMessage<Car>>> Honk(string plate)
        {
            try
            {
                var resp = await _carService.Honk(plate);

                if (!resp.Success)
                {
                    return BadRequest(resp.Message);
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
