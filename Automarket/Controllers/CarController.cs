using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Automarket.Controllers;

public class CarController : Controller
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCars()
    {

        var response = await _carService.GetCars();
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response.Data.ToList());
        }

        return RedirectToAction("Error");
    }

    [HttpGet]
    public async Task<IActionResult> GetCar(int id)
    {
        var response = await _carService.GetCar(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response.Data);
        }
        return RedirectToAction("Error");

       
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _carService.DeleteCar(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return RedirectToAction("GetCars");
        }

        return RedirectToAction("Error");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Save(int id)
    {
        if (id == 0)
        {
            return View();
        }

        var response = await _carService.GetCar(id);
        if (response.StatusCode == Domain.Enum.StatusCode.Ok)
        {
            return View(response.Data);
        }

        return RedirectToAction("Error");

    }

    [HttpPost]
    public async Task<IActionResult> Save(CarViewModel model)
    {
        ModelState.Remove("DateCreate");
        if (ModelState.IsValid)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    await _carService.Create(model, imageData);
                }
                else
                {
                    await _carService.Edit(model.Id, model);
                }



                return RedirectToAction("GetCars");
            }
        }

        return View();


        }
    }




