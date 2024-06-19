using CarSim.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSim.BackEnd.Services.CarServices;

public interface ICarService
{
    public Task<ResponseMessage<List<CarDto>>> GetAllCars();
    public Task<ResponseMessage<CarDto>> GenerateCar();
    public Task<ResponseMessage<CarDto>> CreateCar(int engine, CarBody body, CarType carType, CarFuelType fuelType);
    public Task<ResponseMessage<CarType>> GetCarType(string plate);
    public Task<ResponseMessage<CarDto>> Accelerate(string plate);
    public Task<ResponseMessage<CarDto>> Break(string plate);
    public Task<ResponseMessage<CarDto>> Steer(string plate, SteerDir direction, int angles);
    public Task<ResponseMessage<int>> GetSpeed(string plate);
    public Task<ResponseMessage<int>> GetDirection(string plate);
    public Task<ResponseMessage<string>> FillCar(string plate, CarFuelType fuel);
    public Task<ResponseMessage<String>> Honk(string plate);
}
