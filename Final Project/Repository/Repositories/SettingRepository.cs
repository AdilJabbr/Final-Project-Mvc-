using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SettingRepository : BaseRepository<Setting>, ISettingRepository
    {
        private readonly Db db;
        public SettingRepository(Db context) : base(context) { db = context; }
        public string GetSettingByKey(string key)
        {
            return db.Setting
                           .Where(s => s.Key == key)
                           .Select(s => s.Value)
                           .FirstOrDefault();
        }

    }
}
