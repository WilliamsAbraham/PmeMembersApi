using Contracts;
using Contracts.EntitiesRepoInterface;
using Entities;
using Repository.EntitiesRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrappers : IRepositoryWrapper
    {
        private RepositoryContext context;
        private IMembersRepository?  _membersRepository;

        public RepositoryWrappers(RepositoryContext _context)
        {
            context = _context;
        }
        public IMembersRepository MembersRepository
        {
            get
            {
                if (_membersRepository == null)
                {
                    _membersRepository = new MembersRepository(context);
                }
        return _membersRepository;
            } }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
