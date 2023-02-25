using Contracts.EntitiesRepoInterface;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EntitiesRepositories
{
    public class MembersRepository : RepositoryBase<Member>, IMembersRepository
    {
        public MembersRepository(RepositoryContext context) : base(context)
        {

        }
    }
}
