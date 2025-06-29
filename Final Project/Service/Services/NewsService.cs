using AutoMapper;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Products;
using Service.Helpers;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepo;
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;
        public NewsService(INewsRepository _newsRepo,
                              IWebHostEnvironment _env,
                              IMapper _map,
                              ICloudinaryManager cloudinaryManager)
        {
            newsRepo = _newsRepo;
            env = _env;
            mapper = _map;
            _cloudinaryManager = cloudinaryManager;
        }

        public async Task<(bool Success, List<string> Errors)> CreateNews(NewsCreateVM vm)
        {
            var errors = new List<string>();

            if (vm == null)
            {
                errors.Add("News data is null.");
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


            if (vm.Title is null)
            {
                errors.Add($" Title Is requared");
                return (false, errors);
            }

            if (vm.Tag is null)
            {
                errors.Add($" Tag Is requared");
                return (false, errors);
            }
            if (vm.Description is null)
            {
                errors.Add($" Description Is requared");
                return (false, errors);
            }

            var imagePath = await _cloudinaryManager.FileCreateAsync(vm.ImageUrl);


            var model = new News
            {

                Tag = vm.Tag,
                Description = vm.Description,
                ImageUrl = imagePath,
                Title = vm.Title
            };

            await newsRepo.CreateAsync(model);

            return (true, new List<string>());
        }

        public async Task<bool> Delete(int id)
        {
            var blog = await newsRepo.GetById(id);
            if (blog == null) return false;

            await newsRepo.DeleteAsync(blog);
            return true;
        }

        public async Task<NewsDetailVM> DetailAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await newsRepo.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return mapper.Map<NewsDetailVM>(product);
        }

       
        public async Task<IEnumerable<NewsVM>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<NewsVM>>(await newsRepo.GetAllAsync());
        }

        public async Task<NewsVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await newsRepo.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return mapper.Map<NewsVM>(product);
        }

        public async Task<NewsEditVM> NewsEditVM(int id)
        {
            var news = await newsRepo.GetById(id);

            if (news == null)
            {
                throw new NotFoundException("Not Found");
            }

            var newsEditVM = new NewsEditVM { Id = news.Id, Title = news.Title, Tag = news.Tag, Description = news.Description, ImageUrlPath = news.ImageUrl };

            return newsEditVM;
        }

        public async Task<bool> Edit(NewsEditVM vm)
        {
            if (vm == null)
                throw new NotFoundException("News not found");

            var existingBlog = await newsRepo.GetById(vm.Id);
            if (existingBlog == null)
                throw new NotFoundException("News not found.");

            bool isSameData =
                existingBlog.Title == vm.Title &&
                existingBlog.Description == vm.Description &&
                existingBlog.Tag == vm.Tag &&
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

            existingBlog.Title = vm.Title;
            existingBlog.Description = vm.Description;
            existingBlog.Tag = vm.Tag;
            existingBlog.ImageUrl = imagePath;

            newsRepo.EditAsync(existingBlog);
            await newsRepo.SaveChanges();

            return true;
        }



        //public async Task<PaginationResponse<NewsVM>> GetPaginateAsync(int page, int take)
        //{

        //    var products = await newsRepo.GetAllAsync();
        //    int totalItemCount = products.Count();
        //    int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

        //    var mappedDatas = mapper.Map<IEnumerable<ProductVM>>(products.Skip((page - 1) * take).Take(take).ToList());

        //    return new PaginationResponse<ProductVM>(mappedDatas, totalPage, page, totalItemCount);
        //}
    }
}
