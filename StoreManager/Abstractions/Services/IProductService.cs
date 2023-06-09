﻿using StoreManager.DTOs.InputModels;
using StoreManager.DTOs.ViewModels;

namespace StoreManager.Abstractions.Services
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllProducts();
        Task<ProductViewModel> GetProductById(int id);
        Task<ProductViewModel> Createproduct(ProductInputModel model);
        Task<ProductViewModel> UpdateProduct(int id, ProductInputModel model);
        Task DeleteProduct(int id);
        Task<List<ProductViewModel>> SearchByTerm(string term);
    }
}
