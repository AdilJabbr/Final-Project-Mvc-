using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Helpers.Exceptions;
using Service.Helpers.Extensions;
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
        private readonly ICloudinaryManager _cloudinaryManager;
        private readonly ISettingRepository _settingRepository;
        private readonly IMapper _mapper;
        public SettingService(ISettingRepository settingRepository, IMapper mapper, ICloudinaryManager cloudinaryManager)
        {
            _settingRepository = settingRepository;
            _mapper = mapper;
            _cloudinaryManager = cloudinaryManager;
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

        public async Task EditSettingAsync(SettingEditVM settingEditVM)
        {
            var setting = await _settingRepository.GetAsync(s => s.Id == settingEditVM.Id);

            if (setting == null)
            {
                throw new NotFoundException("Setting not found");
            }

            if (settingEditVM.UploadedImage != null)
            {
                var validationResult = FileExtension.ValidateImage(settingEditVM.UploadedImage);
                if (!validationResult.IsSuccess)
                    throw new NotFoundException("File is not image və size is not 200MB.");


                var filePath = await _cloudinaryManager.FileCreateAsync(settingEditVM.UploadedImage);

                setting.Value = filePath;
            }
            else
            {
                if (settingEditVM.Value == null)
                {
                    throw new NotFoundException("Setting not found");
                }
                setting.Value = settingEditVM.Value;

            }
            _settingRepository.EditAsync(setting);

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

        public string GetSetting(string key)
        {
            return _settingRepository.GetSettingByKey(key);

        }

        public async Task<SettingEditVM> SettingEditVM(int id)
        {
            var setting = await _settingRepository.GetAsync(x => x.Id == id);


            var model = new SettingEditVM
            {
                Key = setting.Key,
                Value = setting.Value
            };

            return model;
        }
    }
}
