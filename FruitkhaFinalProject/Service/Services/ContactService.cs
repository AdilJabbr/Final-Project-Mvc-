using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Contact;
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

        public ContactService(IContactRepository contactRepository,
                              IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;

        }

        public async Task CreateAsync(ContactCreateVM model)
        {
            if (model is null) throw new ArgumentNullException();
            await _contactRepository.CreateAsync(_mapper.Map<Contact>(model));
            await _contactRepository.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var contact = await _contactRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            await _contactRepository.DeleteAsync(contact);
            await _contactRepository.SaveChanges();
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
    }
}
