using System;
using EGID.Common.Interfaces;

namespace EGID.Infrastructure.Time
{
    public class UtcDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
