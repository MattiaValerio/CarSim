using CarSim.BackEnd.Context;
using CarSim.BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSim.BackEnd.lib
{
    public class Utility
    {
        private DataContext _context;

        public Utility(DataContext conext)
        {
            _context = conext;
        }

        // check in the db if the plate already exists
        public bool CheckPlate(string plate)
        {
            return _context.Cars.Any(c => c.Plate == plate);
        }
    }
}
