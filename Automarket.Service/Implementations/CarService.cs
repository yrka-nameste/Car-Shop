using Automarket.DAL.Interfaces;
using Automarket.Domain.ViewModels.Car;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Response;
using Automarket.Service.Interfaces;
using System;
using Microsoft.EntityFrameworkCore;

namespace Automarket.Service.Implementations;

public class CarService:ICarService
{
    private readonly IBaseRepository<Car> _carRepository;

    public CarService(IBaseRepository<Car> carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<IBaseResponse<CarViewModel>> GetCar(int id)
    {
      
        try
        {
            var car = await _carRepository.GetAll().FirstOrDefaultAsync(x=>x.Id==id);
            if (car == null)
            {
                return new BaseResponse<CarViewModel>()
                {
                    Description = "Пользователь не найден",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var data = new CarViewModel()
            {
                Description = car.Description,
                TypeCar = car.TypeCare.ToString(),
                Dpeed = car.Dpeed,
                Model = car.Model,
                Image = car.Avatar,
            };
            return new BaseResponse<CarViewModel>()
            {
                StatusCode = StatusCode.Ok,
                Data = data
            };

        }
        catch (Exception e)
        {
            return new BaseResponse<CarViewModel>()
            {
                Description = $"[GetCar]:{e.Message}",
                StatusCode = StatusCode.InrernalServerError

            };
            
        }
    }

   

    public async Task<IBaseResponse<Car>> Create(CarViewModel model, byte[] imageData)
    {
        
        try
        {
            var car = new Car()
            {
              Name = model.Name,
              Model = model.Model,
              Description = model.Description,
              DateCreate = DateTime.Now,
              Dpeed = model.Dpeed,
              TypeCare = (TypeCare)Convert.ToInt32(model.TypeCar),
              Price = model.Price,
              Avatar = imageData
                
            };

            await _carRepository.Create(car);

            return new BaseResponse<Car>()
            {
                StatusCode = StatusCode.Ok,
                Data = car
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[Create]:{e.Message}",
                StatusCode = StatusCode.InrernalServerError

            };

        }
        
    }
    
    public async Task<IBaseResponse<bool>> DeleteCar(int id)
    {
     
        try
        {
            var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "User not Found",
                    StatusCode = StatusCode.UserNotFound,
                    Data = false
                };
            }
            

            await _carRepository.Delete(car);
            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.Ok
            };

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

    public async Task<IBaseResponse<Car>> GetCarByName(string name)
    {
        var baseResponse = new BaseResponse<Car>();
        try
        {

            var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Name == name);
            if (car == null)
            {

                return new BaseResponse<Car>()
                {
                    Description = "User not found",
                    StatusCode = StatusCode.UserNotFound
                };
            }


            return new BaseResponse<Car>()
            {
                Data = car,
                StatusCode = StatusCode.Ok,
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Car>()
            {
                Description = $"[GetCarByName]:{ex.Message}",
                StatusCode = StatusCode.InrernalServerError

            };

        }
    }

    public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
    {
        
        try
        {
            var cars = _carRepository.GetAll();
            if (!cars.Any())
            {
                return new BaseResponse<IEnumerable<Car>>()
                {
                    Description = "Найдено 0 элементов",
                    StatusCode = StatusCode.Ok

                };

            }

            return new BaseResponse<IEnumerable<Car>>()
            {
                Data = cars,
                StatusCode = StatusCode.Ok
            };
            

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

    Task<IBaseResponse<Car>> ICarService.GetCar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel carViewModel)
    {
       
        try
        {
            var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
            {
                return new BaseResponse<Car>()
                {
                    Description = "Car not found",
                    StatusCode = StatusCode.CarNotFound
                };
            }

            car.Description = carViewModel.Description;
            car.Model = carViewModel.Model;
            car.Price = carViewModel.Price;
            car.Dpeed = carViewModel.Dpeed;
            car.DateCreate = carViewModel.DateCreate;
            car.Name = carViewModel.Name;

            await _carRepository.Update(car);


            return new BaseResponse<Car>()
            {
                Data = car,
                StatusCode = StatusCode.Ok
            };
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