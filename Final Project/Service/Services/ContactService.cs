using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Categories;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Contact;
using Service.ViewModel.Admin.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;


        public ContactService(IContactRepository contactRepository,
                              IMapper mapper,
                              IEmailService emailService)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<ContactCreateVM> ContactCreateVMAsync(int id)
        {
            var model = await _contactRepository.GetById(id);

            if (model == null)
            {
                throw new NotFoundException("Contact cant be null");
            }

            var vm = new ContactCreateVM { Name = model.Name, Email = model.Email, Message = model.Message, Id = model.Id };

            return vm;
        }

        public async Task<IEnumerable<ContactVM>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ContactVM>>(await _contactRepository.GetAllAsync());
        }

        public async Task<ContactVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var contact = await _contactRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return _mapper.Map<ContactVM>(contact);
        }

        public async Task<bool> SendEmailContact(ContactCreateVM dto)
        {
            if (dto == null)
            {
                throw new NotFoundException("Contact cant be null");
            }



            _emailService.SendEmail(dto.Email, "Dear Customer", dto.Answer);

            var model = await _contactRepository.GetById(dto.Id);
            if (model == null)
            {
                throw new NotFoundException("contact cant be null");
            }

            model.IsAnswer = true;

            _contactRepository.EditAsync(model);
            await _contactRepository.SaveChanges();

            return true;
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var contact = await _contactRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            await _contactRepository.DeleteAsync(contact);
            await _contactRepository.SaveChanges();
        }

        public async Task<bool> CreateAsync(ContactCreateVM vm)
        {
            if (vm == null) return false;

            if (string.IsNullOrWhiteSpace(vm.Name))
                throw new NotFoundException("Contact is null");

            var model = new Contact
            {
                Id = vm.Id,
                Name = vm.Name,
                Email = vm.Email,
                Message = vm.Message,
                IsAnswer = vm.IsAnswer
            };

            await _contactRepository.CreateAsync(model);
            return true;
        }
    }
}
