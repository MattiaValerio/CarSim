using CarSim.BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CarSim.BackEnd.Context;

public class DataContext : DbContext
{
    private DbContextOptions<DataContext> _options;
    private IConfiguration _conf;

    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        _options = options;
        _conf = configuration; 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_conf.GetConnectionString("DefaultConnection")); 
    }

    public DbSet<Car> Cars { get; set; }
}
