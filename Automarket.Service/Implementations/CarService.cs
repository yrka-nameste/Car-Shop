using Automarket.DAL.Interfaces;
using Automarket.Domain.ViewModels.Car;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Response;
using Automarket.Service.Interfaces;
using System;

namespace Automarket.Service.Implementations;

public class CarService:ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IBaseResponse<Car>> GetCar(int id)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.Get(id);
            if (car == null)
            {
                baseResponse.Description = "User not found";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                return baseResponse;
            }

            baseResponse.Data = car;
            return baseResponse;

        }
        catch (Exception e)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetCar]:{e.Message}",
                StatusCode = StatusCode.InrernalServerError

            };
            
        }
    }

    public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel)
    {
        var baseResponse = new BaseResponse<CarViewModel>();
        try
        {
            var car = new Car()
            {
                Description = carViewModel.Description,
                DateCreate = DateTime.Now,
                Dpeed = carViewModel.Dpeed,
                Model = carViewModel.Model,
                Price = carViewModel.Price,
                Name = carViewModel.Name,
                TypeCare = (TypeCare)Convert.ToInt32(carViewModel.TypeCar)
                
            };

            await _carRepository.Create(car);
        }
        catch (Exception e)
        {
            return new BaseResponse<CarViewModel>()
            {
                Description = $"[CreateCar]:{e.Message}",
                StatusCode = StatusCode.InrernalServerError

            };

        }

        return baseResponse;
    }
    public async Task<IBaseResponse<Car>> GetCarByName(string name)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.GetByName(name);
            if (car == null)
            {
                baseResponse.Description = "User not found";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                return baseResponse;
            }

            baseResponse.Data = car;
            return baseResponse;

        }
        catch (Exception e)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetCarByName]:{e.Message}",
                StatusCode = StatusCode.InrernalServerError

            };
            
        }
    }

    public async Task<IBaseResponse<bool>> DeleteCar(int id)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var car = await _carRepository.Get(id);
            if (car == null)
            {
                baseResponse.Description = "User not found";
                baseResponse.StatusCode = StatusCode.UserNotFound;
                return baseResponse;
            }

            await _carRepository.Delete(car);
            return baseResponse;

        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteCar]:{e.Message}",
                StatusCode = StatusCode.InrernalServerError

            };
        }
        
    }

    public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
    {
        var baseResponse = new BaseResponse<IEnumerable<Car>>();
        try
        {
            var cars =await _carRepository.Select();
            if (cars.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = StatusCode.Ok;
                return baseResponse;

            }

            baseResponse.Data = cars;
            baseResponse.StatusCode = StatusCode.Ok;
            return baseResponse;

        }
        catch (Exception e)
        {
            return new BaseResponse<IEnumerable<Car>>()
            {
                Description = $"[GetCars]:{e.Message}",
                StatusCode = StatusCode.InrernalServerError

            };
        }
    }

    public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel carViewModel)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {
            var car = await _carRepository.Get(id);
            if (car == null)
            {
                baseResponse.StatusCode = StatusCode.CarNotFound;
                baseResponse.Description = "Car not found";
                return baseResponse;
            }

            car.Description = carViewModel.Description;
            car.Model = carViewModel.Model;
            car.Price = carViewModel.Price;
            car.Dpeed = carViewModel.Dpeed;
            car.DateCreate = carViewModel.DateCreate;
            car.Name = carViewModel.Name;

            await _carRepository.Update(car);


            return baseResponse;
            // TypeCar

        }
        catch (Exception ex)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InrernalServerError

            };
        }
    }
}