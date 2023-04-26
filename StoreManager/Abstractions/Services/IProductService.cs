﻿using StoreManager.DTOs.ViewModels;

namespace StoreManager.Abstractions.Services
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductById(int id);
    }
}