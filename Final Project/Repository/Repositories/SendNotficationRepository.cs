using Domain.Models;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SendNotficationRepository : BaseRepository<SendNotfication>, ISendNotficationRepository
    {
        public SendNotficationRepository(Db context) : base(context) { }
    }
}
