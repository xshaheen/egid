using System;
using System.Threading.Tasks;
using EGID.Core.Exceptions;
using EGID.Data.Health.Dto;
using EGID.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EGID.Data.Health
{
    public interface IHealthInfoService
    {
        #region Commands

        /// <summary>
        ///     Add health information
        /// </summary>
        /// <exception cref="DbUpdateException">
        ///     Can not save changes to database.
        /// </exception>
        Task<HealthInfo> AddHealthInfoAsync(AddHealthInfoModel model);

        /// <summary>
        ///     Add Health record to an HealthObject
        /// </summary>
        /// <exception cref="DbUpdateException">
        ///     Can not save changes to database.
        /// </exception>
        Task<HealthRecord> AddRecord(HealthRecord record);

        /// <summary>
        ///     Update a existence record.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can't find this record.
        /// </exception>
        /// <exception cref="DbUpdateException">
        ///     Can not save changes to database.
        /// </exception>
        void UpdateRecord();

        /// <summary>
        ///     Put new family phones.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can't find this record.
        /// </exception>
        Task PutFamilyPhonesAsync(Guid info);

        #endregion Commands

        #region Query

        /// <summary>
        ///     Finds HealthInfo entity and its health records.
        /// </summary>
        /// <exception cref="EntityNotFoundException">
        ///     Can not find the HealthInfo.
        /// </exception>
        Task<HealthInfo> GetHealthInfoAsync(Guid id);

        #endregion Query
    }
}
