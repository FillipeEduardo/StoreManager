using AutoMapper;
using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;
using StoreManager.Models;

namespace StoreManager.DTOs
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, ProductInputModel>().ReverseMap();
        }
    }
}
