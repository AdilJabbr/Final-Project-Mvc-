using Final_Project.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(Db context) : base(context)
        {
        }

     
    }
}
