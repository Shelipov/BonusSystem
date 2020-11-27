using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BonusSystem.DataProviders.Interfaces
{
    public interface IDataIsInitialized
    {
        Task Execute();
    }
}
