using AutoMapper;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly ICloudinaryManager _cloudinaryManager;


        public BrandService(IBrandRepository brandRepository,
                            IMapper mapper,
                            IWebHostEnvironment webHostEnvironment,
                            ICloudinaryManager cloudinaryManager)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _env = webHostEnvironment;
            _cloudinaryManager = cloudinaryManager;
        }

        public async Task<bool> CreateAsync(BrandCreateVM vm)
        {
            if (vm == null) return false;

            if (string.IsNullOrWhiteSpace(vm.Name))
                throw new NotFoundException("Category is null");

            if (vm.ImageFile == null || vm.ImageFile.Length == 0)
                throw new NotFoundException("Image is requared");
            var result = FileExtension.ValidateImage(vm.ImageFile);

            if (!result.IsSuccess)
            {
                throw new NotFoundException($" File Is not image or file size  200 mb");

            }
            var img = await _cloudinaryManager.FileCreateAsync(vm.ImageFile);
            if (img == null)
                throw new Exception("image is not upload");


            var model = new Brands
            {
                Name = vm.Name,
                ImageUrl = img
            };

            await _brandRepository.CreateAsync(model);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand == null)
                throw new NotFoundException("Not Found brand");

            await _brandRepository.DeleteAsync(brand);
            return true;
        }

        public async Task<bool> EditAsync(BrandEditVM vm)
        {
            if (vm == null) return false;

            var brand = await _brandRepository.GetById(vm.Id);
            if (brand == null)
                throw new NotFoundException("Not Found Category Name");

            if (string.IsNullOrWhiteSpace(vm.Name))
                throw new NotFoundException("Name is null");

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                var img = await _cloudinaryManager.FileCreateAsync(vm.ImageFile);
                if (img == null)
                    throw new NotFoundException("Not Found Image");
                var result = FileExtension.ValidateImage(vm.ImageFile);

                if (!result.IsSuccess)
                {
                    throw new NotFoundException($" File Is not image or file size  200 mb");

                }
                brand.ImageUrl = img;
            }

            brand.Name = vm.Name;
            _brandRepository.EditAsync(brand);
            await _brandRepository.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<BrandVM>> GetAllAsync()
        {
           // return _mapper.Map<IEnumerable<BrandVM>>(await _brandRepository.GetAllAsync());
           var brands = await _brandRepository.GetAllAsync();
           return  brands.Select(x => new BrandVM { Id = x.Id, Name = x.Name, ImageUrl = x.ImageUrl}).ToList();
        }

        public async Task<BrandEditVM> GetBrandEdit(int id)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand is null)
            {
                throw new NotFoundException("category is null ");
            }

            var model = new BrandEditVM
            {
                Name = brand.Name,
                ImageUrl = brand.ImageUrl
            };

            return model;
        }

        public async Task<BrandVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var brand = await _brandRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");
           
            return new BrandVM { Name = brand.Name, ImageUrl = brand.ImageUrl };
        }
    }

}
