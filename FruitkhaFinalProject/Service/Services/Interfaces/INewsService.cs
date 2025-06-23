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
        Task CreateAsync(NewsCreateVM model);
        Task EditAsync(int? id, NewsEditVM model);
        Task DeleteAsync(int? id);
        Task<IEnumerable<NewsVM>> GetAllAsync();
        Task<NewsVM> GetByIdAsync(int? id);
        Task<NewsDetailVM> DetailAsync(int? id);
    }
}
