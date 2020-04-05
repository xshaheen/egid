using System;
using System.Threading.Tasks;
using EGID.Core.Common.Pages;
using EGID.Domain.Entities;

namespace EGID.Data.Citizens
{
    public interface IUpdateRequestService
    {
        #region Commands

        Task AddRequestAsync();

        Task AcceptRequestAsync(Guid id);

        #endregion Commands

        #region Query

        Task<Page<CitizenUpdateRequest>> GetAllRequests();

        #endregion Query
    }
}
