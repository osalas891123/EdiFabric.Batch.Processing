using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EdiFabric.Batch.Processing.Contracts.Application.Enums;

namespace EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces
{
    public interface ISupervisor
    {
        Task<ReturnCode> SuperviseWork();
    }
}
