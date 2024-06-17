using CarSim.BackEnd.Context;
using CarSim.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSim.BackEnd.Controller
{
    [Route("api/")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private DataContext _context;

        public CarController(DataContext context)
        {
            _context = context;
        }

        // Create a new car and return the plate
        [Route("CreateCar")]
        [HttpGet]
        public async Task<ActionResult<String>> CreateCar()
        {
            try
            {
                var car1 = new Car();

                // search for the same plate in the db
                var car = _context.Cars.FirstOrDefault(c => c.Plate == car1.Plate);

                // if the plate already exists, generate a new one
                if (car != null)
                {
                    while(_context.Cars.Any(c => c.Plate == car1.Plate)){
                        car1.Plate = car1.GeneratePlate();
                    }
                }

                await _context.Cars.AddAsync(car1);
                await _context.SaveChangesAsync();

                return Ok(car1.Plate);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
