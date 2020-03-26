using System;
using System.Threading.Tasks;
using EGID.Core.Common.Pages;
using EGID.Web.Entities;

namespace EGID.Web.Data.Repository.Citizens
{
    public interface IUpdateRequestRepo
    {
        #region Commands

        Task AddRequestAsync(AddUpdateRequestModel model);

        Task AcceptRequestAsync(Guid id);

        #endregion

        #region Query

        Task<Page<CitizenUpdateRequest>> GetAllRequests();

        #endregion
    }
}
