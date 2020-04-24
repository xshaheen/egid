namespace EGID.Domain.Entities
{
    public class ExitHospitalRecordAttachment
    {
        public string Id { get; set; }

        public string ExitHospitalRecordId { get; set; }
        public ExitHospitalRecord ExitHospitalRecord { get; set; }
    }
}
