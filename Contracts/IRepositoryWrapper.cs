using Contracts.EntitiesRepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IMembersRepository MembersRepository { get; }
        void Save();
    }
}
