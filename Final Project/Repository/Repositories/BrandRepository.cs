using Domain.Models;
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
    public class BrandRepository : BaseRepository<Brands>, IBrandRepository
    {
        public BrandRepository(Db context) : base(context)
        {
        }
       
    }
}
