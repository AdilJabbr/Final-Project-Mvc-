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
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;

        }
        public async Task CreateAsync(BrandCreateVM model)
        {
           

            if (model is null) throw new ArgumentNullException();
            string fileName = $"{Guid.NewGuid()}-{model.UploadImage.FileName}";

            string path = _env.GenerateFilePath("images", fileName);
            await model.UploadImage.SaveFileToLocalAsync(path);

            model.Image = fileName;

            var data = _mapper.Map<Brands>(model);
            if (await _brandRepository.ExistAsync(model.Name))
            {
                throw new ExistException("Brand with this name already exists");
            }
            await _brandRepository.CreateAsync(data);

            
           
            await _brandRepository.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var brand = await _brandRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            await _brandRepository.DeleteAsync(brand);
            await _brandRepository.SaveChanges();
        }

        public async Task EditAsync(int? id, BrandEditVM model)
        {
        

            ArgumentNullException.ThrowIfNull(nameof(id));
            var brand = await _brandRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");


            if (model.UploadImage is not null)
            {
                string oldPath = _env.GenerateFilePath("images", brand.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{model.UploadImage.FileName}";
                string newPath = _env.GenerateFilePath("images", fileName);
                await model.UploadImage.SaveFileToLocalAsync(newPath);

                model.Image = fileName;
            }
            if (await _brandRepository.ExistAsync(model.Name))
            {
                throw new ExistException("Brand with this name already exists");
            }

            _mapper.Map(model, brand);
            await _brandRepository.EditAsync(brand);
            await _brandRepository.SaveChanges();
        }

        public async Task<IEnumerable<BrandVM>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<BrandVM>>(await _brandRepository.GetAllAsync());
        }

        public async Task<BrandVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var brand = await _brandRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return _mapper.Map<BrandVM>(brand);
        }
    }

}
