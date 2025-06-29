using Service.ViewModel.Admin.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface INewsService
    {
        Task<(bool Success, List<string> Errors)> CreateNews(NewsCreateVM vm);
        Task<bool> Edit(NewsEditVM vm);
        Task<bool> Delete(int id);
        Task<NewsEditVM> NewsEditVM(int id);
        Task<IEnumerable<NewsVM>> GetAllAsync();
        Task<NewsVM> GetByIdAsync(int? id);
        Task<NewsDetailVM> DetailAsync(int? id);
    }
}
