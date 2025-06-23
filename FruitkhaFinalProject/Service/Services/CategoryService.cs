using AutoMapper;
using Final_Project.Models;
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
        public async Task CreateAsync(CategoryCreateVM model)
        {
            if (model is null) throw new ArgumentNullException();
            var data = await categoryRepo.FindAll(br => br.Name.ToLower() == model.Name.ToLower());
            if (data.ToList().Count > 0)
            {
                throw new BadRequestException("This name has already exist");
            }
            await categoryRepo.CreateAsync(mapper.Map<Category>(model));
            await categoryRepo.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var category = await categoryRepo.GetById((int)id) ?? throw new NotFoundException("Data not found");

            await categoryRepo.DeleteAsync(category);
            await categoryRepo.SaveChanges();
        }

        public async Task EditAsync(int? id, CategoryEditVM model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var brand = await categoryRepo.GetById((int)id) ?? throw new NotFoundException("Data not found");
            var data = await categoryRepo.FindAll(br => br.Name.ToLower() == model.Name.ToLower());
            if (data.ToList().Count > 0)
            {
                throw new BadRequestException("This name has already exist");
            }

            mapper.Map(model, brand);

            await categoryRepo.EditAsync(brand);
            await categoryRepo.SaveChanges();
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
    }
}
