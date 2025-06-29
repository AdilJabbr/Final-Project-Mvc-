using AutoMapper;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Repository.Repositories.Interfaces;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Employees;
using Service.ViewModel.Admin.News;
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
        private readonly ICloudinaryManager _cloudinaryManager;


        public EmployeeService(
                                  IEmployeeRepository employeeRepository,
                                  IMapper mapper,
                                  IWebHostEnvironment env,
                                  ICloudinaryManager cloudinaryManager
                                 )
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _env = env;
            _cloudinaryManager = cloudinaryManager;
        }

        public async Task<(bool Success, List<string> Errors)> CreateEmployee(EmployeeCreateVM vm)
        {
            var errors = new List<string>();

            if (vm == null)
            {
                errors.Add("Employee data is null.");
                return (false, errors);

            }

            if (vm.ImageUrl is null)
            {
                errors.Add($" IMAGE Is requared");
                return (false, errors);
            }
            var result = FileExtension.ValidateImage(vm.ImageUrl);

            if (!result.IsSuccess)
            {
                errors.Add($" File Is not image or file size  200 mb");
                return (false, errors);
            }


            if (vm.Name is null)
            {
                errors.Add($" Name Is requared");
                return (false, errors);
            }

            if (vm.Position is null)
            {
                errors.Add($" Position Is requared");
                return (false, errors);
            }
            if (vm.Description is null)
            {
                errors.Add($" Description Is requared");
                return (false, errors);
            }

            var imagePath = await _cloudinaryManager.FileCreateAsync(vm.ImageUrl);


            var model = new Employee
            {

                Position = vm.Position,
                Description = vm.Description,
                ImageUrl = imagePath,
                Name = vm.Name
            };

            await _employeeRepository.CreateAsync(model);

            return (true, new List<string>());
        }

        public async Task<bool> Delete(int id)
        {
            var blog = await _employeeRepository.GetById(id);
            if (blog == null) return false;

            await _employeeRepository.DeleteAsync(blog);
            return true;
        }

        public async Task<bool> Edit(EmployeeEditVM vm)
        {
            if (vm == null)
                throw new NotFoundException("Employee not found");

            var existingBlog = await _employeeRepository.GetById(vm.Id);
            if (existingBlog == null)
                throw new NotFoundException("Blog not found.");

            bool isSameData =
                existingBlog.Name == vm.Name &&
                existingBlog.Description == vm.Description &&
                existingBlog.Position == vm.Position &&
                vm.ImageUrl == null;

            if (isSameData)
                return true;

            string? imagePath = existingBlog.ImageUrl;
            if (vm.ImageUrl != null)
            {
                var validationResult = FileExtension.ValidateImage(vm.ImageUrl);
                if (!validationResult.IsSuccess)
                    throw new NotFoundException("File is not image və size is not 200MB.");

                imagePath = await _cloudinaryManager.FileCreateAsync(vm.ImageUrl);
            }

            existingBlog.Name = vm.Name;
            existingBlog.Description = vm.Description;
            existingBlog.Position = vm.Position;
            existingBlog.ImageUrl = imagePath;

            _employeeRepository.EditAsync(existingBlog);
            await _employeeRepository.SaveChanges();

            return true;
        }

        public async Task<EmployeeEditVM> EmployeeEditVM(int id)
        {
            var news = await _employeeRepository.GetById(id);

            if (news == null)
            {
                throw new NotFoundException("Not Found");
            }

            var employeeEditVM = new EmployeeEditVM { Id = news.Id, Name = news.Name, Position = news.Position, Description = news.Description, ImageUrlPath = news.ImageUrl };

            return employeeEditVM;
        }

        public async Task<IEnumerable<EmployeeVM>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<EmployeeVM>>(await _employeeRepository.GetAllAsync());

        }

        public async Task<EmployeeVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var employee = await _employeeRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return _mapper.Map<EmployeeVM>(employee);
        }
    }
}
