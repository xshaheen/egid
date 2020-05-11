using EGID.Domain.Enums;

namespace EGID.Application.Health.Queries
{
    public class EmergencyInfo
    {
        public BloodType BloodType { get; set; }

        public string Notes { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
    }
}
