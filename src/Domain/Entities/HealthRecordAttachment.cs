namespace EGID.Domain.Entities
{
    public class HealthRecordAttachment
    {
        public string Id { get; set; }

        public string HealthRecordId { get; set; }
        public virtual HealthRecord HealthRecord { get; set; }
    }
}