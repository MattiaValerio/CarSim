using CarSim.BackEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSim.BackEnd.Services.CarServices;

public interface ICarService
{
    public Task<ResponseMessage<String>> GenerateCar();
    public Task<ResponseMessage<Car>> CreateCar(int engine, CarBody body, CarType carType, CarFuelType fuelType);
    public Task<ResponseMessage<CarType>> GetCarType(string plate);
    public Task<ResponseMessage<Car>> Accelerate(string plate);
    public Task<ResponseMessage<Car>> Break(string plate);
    public Task<ResponseMessage<Car>> Steer(string plate, SteerDir direction, int angles);
    public Task<ResponseMessage<int>> GetSpeed(string plate);
    public Task<ResponseMessage<int>> GetDirection(string plate);
    public Task<ResponseMessage<Car>> FillCar(string plate, CarFuelType fuel);
    public Task<ResponseMessage<String>> Honk(string plate);
}
