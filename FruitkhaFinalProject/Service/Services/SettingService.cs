using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Helpers.Exceptions;
using Service.Services.Interfaces;
using Service.ViewModel.Admin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;
        public SettingService(ISettingRepository settingRepository, IMapper mapper)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;

        }
        public async Task CreateAsync(SettingCreateVM model)
        {
            if (model is null) throw new ArgumentNullException();
            await _settingRepository.CreateAsync(_mapper.Map<Setting>(model));
            await _settingRepository.SaveChanges();
        }

        public async Task DeleteAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var setting = await _settingRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            await _settingRepository.DeleteAsync(setting);
            await _settingRepository.SaveChanges();
        }

        public async Task EditAsync(int? id, SettingEditVM model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var setting = await _settingRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            _mapper.Map(model, setting);

            await _settingRepository.EditAsync(setting);
            await _settingRepository.SaveChanges();
        }

        public async Task<IEnumerable<SettingVM>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<SettingVM>>(await _settingRepository.GetAllAsync());
        }

        public async Task<SettingVM> GetByIdAsync(int? id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var setting = await _settingRepository.GetById((int)id) ?? throw new NotFoundException("Data not found");

            return _mapper.Map<SettingVM>(setting);
        }
    }
}
