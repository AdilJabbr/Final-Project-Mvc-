using AutoMapper;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Repository.Repositories.Interfaces;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _env;


        public EmployeeService(
                                  IEmployeeRepository employeeRepository,
                                  IMapper mapper,
                                  IWebHostEnvironment env
                                 )
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _env = env;
        }
        public async Task CreateAsync(EmployeeCreateVM model)
        {
            if (model is null) throw new ArgumentNullException();
            string fileName = $"{Guid.NewGuid()}-{model.UploadImage.FileName}";

            string path = _env.GenerateFilePath("images", fileName);
            await model.UploadImage.SaveFileToLocalAsync(path);

            model.Image = fileName;

            //await _categoryRepository.CreateAsync(_mapper.Map<Category>(model));
            var data = _mapper.Map<Employee>(model);
            if (await _employeeRepository.ExistAsync(model.Name))
            {
                throw new ExistException("Category with this name already exists");
            }
            await _employeeRepository.CreateAsync(data);

          
            await _employeeRepository.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var category = await _employeeRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");
            string imagePath = _env.GenerateFilePath("images", category.Image);
            imagePath.DeleteFileFromLocal();

            await _employeeRepository.DeleteAsync(category);
            await _employeeRepository.SaveChanges();
        }

        public async Task EditAsync(int? id, EmployeeEditVM  model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));
            var category = await _employeeRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");


            if (model.UploadImage is not null)
            {
                string oldPath = _env.GenerateFilePath("images", category.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{model.UploadImage.FileName}";
                string newPath = _env.GenerateFilePath("images", fileName);
                await model.UploadImage.SaveFileToLocalAsync(newPath);

                model.Image = fileName;
            }
            if (await _employeeRepository.ExistAsync(model.Name))
            {
                throw new ExistException("Category with this name already exists");
            }

            _mapper.Map(model, category);
            await _employeeRepository.EditAsync(category);
            await _employeeRepository.SaveChanges();
        }

        public async Task<IEnumerable<EmployeeVM>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<EmployeeVM>>(await _employeeRepository.GetAllAsync());
        }

      

        public async Task<EmployeeVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var category = await _employeeRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return _mapper.Map<EmployeeVM>(category);
        }
    }
}
