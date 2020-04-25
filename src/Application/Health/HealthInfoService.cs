/*
using System;
using System.Threading.Tasks;
using EGID.Common.Exceptions;
using EGID.Data.Health.Dto;
using EGID.Domain.Entities;

namespace EGID.Application.Health
{
    // get health information records

    // add a record

    // add a hospital discharge

    // update basic health information (Chronic disease)
    // update basic health information (Family phones)

    public class HealthInfoService : IHealthInfoService
    {
        private readonly IEgidDbContext _context;

        public HealthInfoService(IEgidDbContext context) => _context = context;

        #region Commands

        public Task<HealthInfo> AddHealthInfoAsync(AddHealthInfoModel model)
        {
            throw new NotImplementedException();
        }

        public Task<HealthRecord> AddRecord(HealthRecord record) => throw new NotImplementedException();

        public void UpdateRecord() => throw new NotImplementedException();

        public Task PutFamilyPhonesAsync(Guid info) => throw new NotImplementedException();

        #endregion Commands

        #region Query

        public async Task<HealthInfo> GetHealthInfoAsync(Guid id)
        {
            var healthInfo = await _context.HealthInformation.FindAsync(id);

            if (healthInfo == null) throw new EntityNotFoundException(nameof(HealthInfo), id.ToString());

            var isRecordsLoaded = _context.Entry(healthInfo)
                .Collection(h => h.HealthRecords)
                .IsLoaded;

            if (!isRecordsLoaded)
                await _context.Entry(healthInfo)
                    .Collection(h => h.HealthRecords)
                    .LoadAsync();

            return healthInfo;
        }

        #endregion Query
    }
}
*/
