﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Automarket.Domain.ViewModels.Car;

public class CarViewModel
{
    public int Id { get; set; }
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Введите имя")]
    [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
    public string Name { get; set; }
    
    [Display(Name = "Описание")]
    public string Description { get; set; }
    
    [Display(Name = "Модель")]
    [Required(ErrorMessage = "Укажите модель")]
    [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]   
    public string Model { get; set; }
    
    [Display(Name = "Скорость")]
    [Required(ErrorMessage = "Укажите скорость")]
    [Range(0, 600, ErrorMessage = "Длина должна быть в диапазоне от 0 до 600")] 
    public double Dpeed { get; set; }
        
    [Display(Name = "Стоимость")]
    [Required(ErrorMessage = "Укажите стоимость")]
    public decimal Price { get; set; }
        
    public DateTime DateCreate { get; set; }
        
    public string TypeCar { get; set; }
    
  public IFormFile Avatar { get; set; }
    
    public byte[]? Image { get; set; }
}