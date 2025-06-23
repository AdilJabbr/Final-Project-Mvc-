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
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepo;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;
        public NewsService(INewsRepository _newsRepo,
                              IWebHostEnvironment _env,
                              IMapper _map)
        {
            newsRepo = _newsRepo;
            env = _env;
            mapper = _map;
        }
        public async Task CreateAsync(NewsCreateVM model)
        {
            if (model is null) throw new ArgumentNullException();
            string fileName = $"{Guid.NewGuid()}-{model.UploadImage.FileName}";

            string path = env.GenerateFilePath("images", fileName);
            await model.UploadImage.SaveFileToLocalAsync(path);

            model.Image = fileName;
            var product = mapper.Map<News>(model);
         
            await newsRepo.CreateAsync(product);
            await newsRepo.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await newsRepo.GetById((int)id) ?? throw new NotFoundException("Data not found");
          


            await newsRepo.DeleteAsync(product);
            await newsRepo.SaveChanges();
        }
        public async Task<NewsDetailVM> DetailAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await newsRepo.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return mapper.Map<NewsDetailVM>(product);
        }

        public async Task EditAsync(int? id, NewsEditVM model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await newsRepo.GetById((int)id)
                ?? throw new NotFoundException("Data not found");

          


            // Handle image updates
            List<ProductImages> images = new();

            if (model.UploadImage is not null)
            {
                string oldPath = env.GenerateFilePath("images", model.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{model.UploadImage.FileName}";
                string newPath = env.GenerateFilePath("images", fileName);
                await model.UploadImage.SaveFileToLocalAsync(newPath);

                model.Image = fileName;
            }
            mapper.Map(model, product);

            await newsRepo.EditAsync(product);
            await newsRepo.SaveChanges();
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
