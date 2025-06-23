using AutoMapper;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using service.services.ınterfaces;
using Service.DTOs.Admin.Products;
using Service.Helpers;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepo;
        private readonly ICategoryRepository categoryRepo;
        private readonly IBrandRepository brandRepo;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;

        public ProductService(IProductRepository _productRepo,
                              ICategoryRepository _categoryRepo,
                              IBrandRepository _brandRepo,
                              IWebHostEnvironment _env,
                              IMapper _map)
        {
            productRepo = _productRepo;
            categoryRepo = _categoryRepo;
            brandRepo = _brandRepo;
            env = _env;
            mapper = _map;
        }

        public async Task AddImageAsync(int productId, IFormFile uploadImage)
        {
            if (uploadImage == null || uploadImage.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            var product = await productRepo.GetById(productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            string fileName = $"{Guid.NewGuid()}-{uploadImage.FileName}";
            string path = env.GenerateFilePath("images", fileName);

            await uploadImage.SaveFileToLocalAsync(path);

            var productImage = new ProductImages { Name = fileName, IsMain = false };
            product.ProductImages.Add(productImage);

            await productRepo.EditAsync(product);
            await productRepo.SaveChanges();
        }

        public async Task CreateAsync(ProductCreateVM model)
        {
            if (model is null) throw new ArgumentNullException();

            if (await productRepo.ExistAsync(model.Name))
            {
                throw new ExistException("Product with this name already exists");
            }

            // Brand validation
            if (await brandRepo.GetById(model.BrandId) is null)
            {
                throw new NotFoundException("Brand not found");
            }

            // Category validation
            if (await categoryRepo.GetById(model.CategoryId) is null)
            {
                throw new NotFoundException("Category not found");
            }

            // Create product entity
            var product = mapper.Map<Product>(model);

            // Handle product images
            List<ProductImages> images = new();

            foreach (var item in model.UploadImages)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                string path = env.GenerateFilePath("images", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new ProductImages { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            model.ProductImages = images;

            await productRepo.CreateAsync(product);
            await productRepo.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await productRepo.GetByIdWithAsync((int)id) ?? throw new NotFoundException("Data not found");
            foreach (var item in product.ProductImages)
            {
                string path = env.GenerateFilePath("images", item.Name);
                path.DeleteFileFromLocal();
            }


            await productRepo.DeleteAsync(product);
            await productRepo.SaveChanges();
        }

        public async Task DeleteImageAsync(int productId, int productImageId)
        {
            var product = await productRepo.GetByIdWithAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("Product not found.");
            }

            var image = product.ProductImages.FirstOrDefault(i => i.Id == productImageId);
            if (image == null)
            {
                throw new NotFoundException("Image not found.");
            }

            foreach (var item in product.ProductImages)
            {
                string path = env.GenerateFilePath("images", item.Name);
                path.DeleteFileFromLocal();
            }

            await productRepo.EditAsync(product);
            await productRepo.SaveChanges();
        }

        public async Task<ProductDetailVM> DetailAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await productRepo.GetByIdWithAsync((int)id) ?? throw new NotFoundException("Data not found");

            return mapper.Map<ProductDetailVM>(product);
        }

        public async Task EditAsync(int? id, ProductEditVM model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await productRepo.GetByIdWithAsync((int)id)
                ?? throw new NotFoundException("Data not found");

            // Check if name already exists (excluding current product)
            if (await productRepo.ExistAsync(model.Name))
            {
                throw new ExistException("Product with this name already exists");
            }

            // Brand validation
            if (await brandRepo.GetById(model.BrandId) is null)
            {
                throw new NotFoundException("Brand not found");
            }

            // Category validation
            if (await categoryRepo.GetById(model.CategoryId) is null)
            {
                throw new NotFoundException("Category not found");
            }

            // Handle image updates
            List<ProductImages> images = new();

            foreach (var item in model.UploadImages)
            {
                string oldPath = env.GenerateFilePath("images", item.Name);
                oldPath.DeleteFileFromLocal();


                string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                string path = env.GenerateFilePath("images", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new ProductImages { Name = fileName });
            }

            // Update product properties
            images.FirstOrDefault().IsMain = true;

            model.ProductImages = images;
            mapper.Map(model, product);

            await productRepo.EditAsync(product);
            await productRepo.SaveChanges();
        }




        public async Task<IEnumerable<ProductVM>> FilterAsync(string brandName, string categoryName, decimal? minPrice, decimal? maxPrice)
        {
            return mapper.Map<IEnumerable<ProductVM>>(await productRepo.FilterAsync( brandName,  categoryName,   minPrice,   maxPrice));
        }

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<ProductVM>>(await productRepo.GetAllAsync());
        }

        public async Task<ProductVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await productRepo.GetByIdWithAsync((int)id) ?? throw new NotFoundException("Data not found");

            return mapper.Map<ProductVM>(product);
        }

        public async Task<PaginationResponse<ProductVM>> GetPaginateAsync(int page, int take)
        {

            var products = await productRepo.GetAllAsync();
            int totalItemCount = products.Count();
            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

            var mappedDatas = mapper.Map<IEnumerable<ProductVM>>(products.Skip((page - 1) * take).Take(take).ToList());

            return new PaginationResponse<ProductVM>(mappedDatas, totalPage, page, totalItemCount);
        }

        public async Task<IEnumerable<ProductVM>> SearchByName(string name)
        {
            var productsQuery = productRepo.GetAllWithIncludes(m => m.Name.Contains(name));

            var products = await productsQuery
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<IEnumerable<ProductVM>>(products);
        }

        public async Task<IEnumerable<ProductVM>> SortByPriceAscending()
        {
            return mapper.Map<IEnumerable<ProductVM>>(await productRepo.SortByPriceAscending());
        }
        public async Task<IEnumerable<ProductVM>> SortByPriceDescending()
        {
            return mapper.Map<IEnumerable<ProductVM>>(await productRepo.SortByPriceDescending());
        }
    }
}
