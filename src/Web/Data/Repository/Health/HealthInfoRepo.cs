using System;
using System.Linq;
using System.Threading.Tasks;
using EGID.Core.Exceptions;
using EGID.Web.Entities;
 
namespace EGID.Web.Data.Repository.Health
{
    public class HealthInfoRepo : IHealthInfoRepo
    {
        private readonly EgidDbContext _context;

        public HealthInfoRepo(EgidDbContext context) => _context = context;

        #region Commands

        public Task<HealthInfo> AddHealthInfoAsync(AddHealthInfoModel model) => throw new NotImplementedException();

        public Task<HealthRecord> AddRecord(HealthRecord record) => throw new NotImplementedException();

        public void UpdateRecord() => throw new NotImplementedException();

        public Task PutFamilyPhonesAsync(Guid info) => throw new NotImplementedException();

        #endregion Commands

        #region Query

        public async Task<HealthInfo> GetHealthInfoAsync(Guid id)
        {
            var healthInfo = await _context.HealthInformation.FindAsync(id);

            if (healthInfo == null) throw new EntityNotFoundException();

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
