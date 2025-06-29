using Final_Project.Models;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AbtRepository : BaseRepository<Abt>, IAbtRepository
    {
        public AbtRepository(Db context) : base(context)
        {
        }
    }
}
