using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Comments;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CommentService :ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public CommentService(ICommentRepository repository, IMapper mapper, IAccountService accountService, IHttpContextAccessor httpContextAccessor, IProductRepository postRepository) 
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _productRepository = postRepository;
            _accountService = accountService;
            

        }

        public async Task<CommentVM> AddCommentAsync(CommentCreateVM dto, string userId)
        {
            var product = await _productRepository.GetByIdWithIncludes(dto.ProductId);
            if (product == null) throw new NotFoundException("Product not found");

            var user = await _accountService.FindUserByIdAsync(userId);

            Comment comment = new()
            {
                Text = dto.Text,
                UserId = userId,
                ProductId = dto.ProductId,
                CreatedDate = DateTime.UtcNow
            };

            product.Comment.Add(comment);
            //product.CommentCount++;

            await _repository.CreateAsync(comment);

            await _productRepository.SaveChanges();


            var newDto = _mapper.Map<CommentVM>(comment);
            newDto.UserName = user.UserName;
          

            return newDto;
        }
       
        public async Task<bool> DeleteCommentAsync(int commentId, int postId)
        {
            var product = await _productRepository.GetByIdWithAsync(postId);
            if (product == null)
            {
                throw new NotFoundException("Post not found");

            }
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (userId is null)
            {
                throw new NotFoundException("User not found");
            }
            var cmm = await _repository.GetById(commentId);
            if (cmm == null)
            {
                throw new NotFoundException("Comment not found");
            }

            if (cmm.UserId == userId)
            {
                //product.CommentCount--;
                _productRepository.EditAsync(product);
                await _productRepository.SaveChanges();
                await _repository.DeleteAsync(cmm);
            }

           

            return true;
        }
    }
}
