using AutoMapper;
using Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Repository;
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
        private readonly IProductImagesRepository imageRepo;
        private readonly IBrandRepository brandRepo;
        private readonly IWebHostEnvironment env;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly ISubscribeRepository subscribeRepo;
        private readonly ICloudinaryManager cloudinaryManager;

        public ProductService(IProductRepository _productRepo,
                              IProductImagesRepository _imageRepo,
                              ICategoryRepository _categoryRepo,
                              IBrandRepository _brandRepo,
                              IWebHostEnvironment _env,
                              ISubscribeRepository _subscribeRepo,
                              IEmailService _emailService,
                              ICloudinaryManager _cloudinaryManager,
                              IMapper _map)
        {
            cloudinaryManager = _cloudinaryManager;
            imageRepo = _imageRepo;
            emailService = _emailService;
            subscribeRepo = _subscribeRepo;
            productRepo = _productRepo;
            categoryRepo = _categoryRepo;
            brandRepo = _brandRepo;
            env = _env;
            mapper = _map;
        }

        //public async Task AddImageAsync(int productId, IFormFile uploadImage)
        //{

        //    //throw new NotImplementedException();

        //    if (uploadImage == null || uploadImage.Length == 0)
        //    {
        //        throw new ArgumentException("No file uploaded.");
        //    }

        //    var product = await productRepo.GetById(productId);
        //    if (product == null)
        //    {
        //        throw new NotFoundException("Product not found.");
        //    }

        //    string fileName = $"{Guid.NewGuid()}-{uploadImage.FileName}";
        //    string path = env.GenerateFilePath("images", fileName);

        //    await uploadImage.SaveFileToLocalAsync(path);

        //    var productImage = new ProductImages { ImageUrl = fileName };
        //    product.ProductImages.Add(productImage);

        //    await productRepo.EditAsync(product);
        //    await productRepo.SaveChanges();
        //}

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await productRepo.GetById(id);
            if (product == null)
                throw new NotFoundException("Not Found Product");

            await productRepo.DeleteAsync(product);
            return true;
        }

        //public async Task DeleteImageAsync(int productId, int productImageId)
        //{
        //    // throw new NotImplementedException();

        //    var product = await productRepo.GetByIdWithAsync(productId);
        //    if (product == null)
        //    {
        //        throw new NotFoundException("Product not found.");
        //    }

        //    var image = product.ProductImages.FirstOrDefault(i => i.Id == productImageId);
        //    if (image == null)
        //    {
        //        throw new NotFoundException("Image not found.");
        //    }

        //    foreach (var item in product.ProductImages)
        //    {
        //        string path = env.GenerateFilePath("images", item.ImageUrl);
        //        path.DeleteFileFromLocal();
        //    }

        //    await productRepo.EditAsync(product);
        //    await productRepo.SaveChanges();
        //}

        public async Task<ProductDetailVM> DetailAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await productRepo.GetByIdWithAsync((int)id) ?? throw new NotFoundException("Data not found");

            return mapper.Map<ProductDetailVM>(product);
        }

        public async Task<IEnumerable<ProductVM>> FilterAsync(string brandName, string categoryName, decimal? minPrice, decimal? maxPrice)
        {
            return mapper.Map<IEnumerable<ProductVM>>(await productRepo.FilterAsync(brandName, categoryName, minPrice, maxPrice));
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

        public async Task<ProductCreateVM> GetCreatedProductDto()
        {
            var categories = await categoryRepo.GetAllAsync();
            var brands = await brandRepo.GetAllAsync();


            var model = new ProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),
                Brands = brands.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return model;
        }

        public async Task<PaginationResponse<ProductVM>> GetPaginateAsync(int page, int take)
        {

            var products = await productRepo.GetAllAsync();
            int totalItemCount = products.Count();
            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

            var mappedDatas = mapper.Map<IEnumerable<ProductVM>>(products.Skip((page - 1) * take).Take(take).ToList());

            return new PaginationResponse<ProductVM>(mappedDatas, totalPage, page, totalItemCount);
        }

        public async Task<ProductVM> GetProduct(int id)
        {
            var product = await productRepo.GetById(id);
            if (product == null)
                throw new NotFoundException("Product not found");

            var images = imageRepo.GetAllIncludes().Where(x => x.ProductId == id).ToList();
            var category = await categoryRepo.GetAsync(x => x.Id == product.CategoryId);
            var brand = await brandRepo.GetAsync(x => x.Id == product.BrandId);


            return new ProductVM
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = category.Name,
                BrandName = brand.Name,

                IsMainPicture = product.IsMainPicture,
                ProductImages = images.Select(x => new ProductImageVM { ImageUrl = x.ImageUrl }).ToList()
            };
        }

        public async Task<ProductEditVM> GetProductUpdateDto(int productId)
        {
            var product = await productRepo.GetById(productId);
            if (product == null) return null;

            var categories = await categoryRepo.GetAllAsync();
            var brands = await brandRepo.GetAllAsync();

            var images = imageRepo.GetAllIncludes().Where(x => x.ProductId == productId).ToList();

            var productUpdateDto = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,

                Description = product.Description,
                ImageMain = product.IsMainPicture,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == product.CategoryId
                }).ToList(),
                CategoryId = product.CategoryId,

                Brands = brands.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = c.Id == product.BrandId
                }).ToList(),
                BrandId = product.BrandId,
                imgUrl = images.Select(x => new ProductImageVM { ImageUrl = x.ImageUrl }).ToList()

            };

            return productUpdateDto;
        }

        public async Task<ProductEditVM> GetUpdateProduct(int id)
        {
            var product = await productRepo.GetById(id);
            if (product == null)
                throw new NotFoundException("Product not found");

            var images = imageRepo.GetAllIncludes().Where(x => x.ProductId == id).ToList();
            var category = await categoryRepo.GetAsync(x => x.Id == product.CategoryId);
            var brand = await brandRepo.GetAsync(x => x.Id == product.BrandId);

            return new ProductEditVM
            {
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,               
                Description = product.Description,
                ImageMain = product.IsMainPicture,
                Categories = new List<SelectListItem>
                {
                     new SelectListItem { Value = category.Id.ToString(), Text = category.Name }
                },
                Brands = new List<SelectListItem>
                {
                     new SelectListItem { Value = brand.Id.ToString(), Text = brand.Name }
                },
                imgUrl = images.Select(x => new ProductImageVM { ImageUrl = x.ImageUrl }).ToList()
            };
        }

        public async Task<(bool Success, List<string> Errors)> ProductCreate(ProductCreateVM dto)
        {
            var errors = new List<string>();

            if (dto == null)
            {
                errors.Add("Product data is null.");
                return (false, errors);
            }

            if (dto.ProductImages.Count == 0)
            {
                errors.Add("Product images are required.");
                return (false, errors);
            }

            if (string.IsNullOrEmpty(dto.Name))
            {
                errors.Add("Product name is required.");
            }

            if (string.IsNullOrEmpty(dto.Description))
            {
                errors.Add("Product description is required.");
            }
           

            if (dto.Price < 0)
            {
                errors.Add($"Price: {dto.Price} is not valid");
            }
            if (dto.CategoryId <= 0)
            {
                errors.Add("Category must be a valid ID.");
            }
            var category = await categoryRepo.GetById(dto.CategoryId);
            if (category == null)
            {
                errors.Add("Category must be a valid ID.");
            }
            var brand = await categoryRepo.GetById(dto.BrandId);
            if (brand == null)
            {
                errors.Add("Category must be a valid ID.");
            }

            if (dto.MainImageUrl == null)
            {
                errors.Add("main image is required");
            }


            if (errors.Any()) return (false, errors);

            var imageMain = await cloudinaryManager.FileCreateAsync(dto.MainImageUrl);

            var model = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                IsMainPicture = imageMain,
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId

            };

            if (dto.ProductImages is null)
            {
                errors.Add(" Images are required");
            }
            await productRepo.CreateAsync(model);

            foreach (var image in dto.ProductImages)
            {
                var uploadedImageUrl = await cloudinaryManager.FileCreateAsync(image);
                var imageRecord = new ProductImages { ProductId = model.Id, ImageUrl = uploadedImageUrl };
                await imageRepo.CreateAsync(imageRecord);
            }

            var subscribers = await subscribeRepo.GetAllAsync();
            foreach (var subscriber in subscribers)
            {
                string body = $"<h2>Yeni məhsul əlavə olundu!</h2>" +
                              $"<p><strong>{dto.Name}</strong> - <strong>{dto.Price} AZN</strong></p>" +
                              $"<p>{dto.Description}</p>" +
                $"<p>Ətraflı məlumat üçün saytımıza baxın.</p>";

                emailService.Send(subscriber.Email, "Yeni məhsul xəbərdarlığı", body);
            }
            return (true, new List<string>());
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

        public async Task<(bool Success, List<string> Errors)> UpdateProductAsync(ProductEditVM dto)
        {
            var errors = new List<string>();

            if (dto == null)
            {
                errors.Add("Product data is null.");
                return (false, errors);
            }

            var product = await productRepo.GetById(dto.Id);
            if (product == null)
            {
                errors.Add("Product not found.");
                return (false, errors);
            }

            bool isUpdated = false;

            if (string.IsNullOrEmpty(dto.Name))
                errors.Add("Product name is required.");
            else if (dto.Name != product.Name)
            {
                product.Name = dto.Name;
                isUpdated = true;
            }

            if (string.IsNullOrEmpty(dto.Description))
                errors.Add("Product description is required.");
            else if (dto.Description != product.Description)
            {
                product.Description = dto.Description;
                isUpdated = true;
            }

            if (dto.Count < 0)
                errors.Add("Count must be a positive value.");
            else if (dto.Count != product.Count)
            {
                product.Count = dto.Count;
                isUpdated = true;
            }

            if (dto.Price < 0)
                errors.Add("Price must be a positive value.");
            else if (dto.Price != product.Price)
            {
                product.Price = dto.Price;
                isUpdated = true;
            }

            if (dto.CategoryId <= 0)
                errors.Add("Invalid category.");
            else if (dto.CategoryId != product.CategoryId)
            {
                product.CategoryId = dto.CategoryId;
                isUpdated = true;
            }

            if (dto.BrandId <= 0)
                errors.Add("Invalid brand.");
            else if (dto.BrandId != product.BrandId)
            {
                product.BrandId = dto.BrandId;
                isUpdated = true;
            }



            if (dto.MainImageUrl != null && dto.ImageMain != product.IsMainPicture)
            {
                var newMainImage = await cloudinaryManager.FileCreateAsync(dto.MainImageUrl);
                product.IsMainPicture = newMainImage;
                isUpdated = true;
            }

            productRepo.EditAsync(product);

            await productRepo.SaveChanges();

            if (dto.ProductImages != null && dto.ProductImages.Any())
            {
                foreach (var image in dto.ProductImages)
                {
                    var uploadedImageUrl = await cloudinaryManager.FileCreateAsync(image);
                    var imageRecord = new ProductImages { ProductId = product.Id, ImageUrl = uploadedImageUrl };
                    await imageRepo.CreateAsync(imageRecord);
                }
            }


            return (true, new List<string>());
        }
    }

}
