using AutoMapper;
using Final_Project.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Categories;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepo;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository brandRepository, IMapper _mapper)
        {
            categoryRepo = brandRepository;
            mapper = _mapper;

        }

        public async Task<bool> CreateAsync(CategoryCreateVM vm)
        {
            if (vm == null) return false;

            if (string.IsNullOrWhiteSpace(vm.Name))
                throw new NotFoundException("Category is null");

            var model = new Category
            {
                Name = vm.Name
            };

            await categoryRepo.CreateAsync(model);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await categoryRepo.GetById(id);
            if (category == null)
                throw new NotFoundException("Not Found Category");

            await categoryRepo.DeleteAsync(category);
            return true;
        }

        public async Task<bool> EditAsync(CategoryEditVM vm)
        {
            if (vm == null) return false;

            var category = await categoryRepo.GetById(vm.Id);
            if (category == null)
                throw new NotFoundException("Not Found Category Name");

            if (string.IsNullOrWhiteSpace(vm.Name))
                throw new NotFoundException("Name is null");
            category.Name = vm.Name;
            categoryRepo.EditAsync(category);
            await categoryRepo.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<CategoryVM>>(await categoryRepo.GetAllAsync());
        }

        public async Task<CategoryVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var brand = await categoryRepo.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return mapper.Map<CategoryVM>(brand);
        }

        public async Task<CategoryEditVM> GetCategoryEdit(int id)
        {
            var category = await categoryRepo.GetById(id);
            if (category is null) 
            {
                throw new NotFoundException("category is null ");
            }
            var model = new CategoryEditVM
            {
                Name = category.Name
            };
            return model;
        }
    }
}
