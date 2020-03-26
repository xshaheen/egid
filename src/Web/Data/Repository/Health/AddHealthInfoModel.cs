﻿using System;
using EGID.Core.Enums;

namespace EGID.Web.Data.Repository.Health
{
    public class AddHealthInfoModel
    {
        public Guid CitizenId { get; set; }

        public BloodType BloodType { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }
    }
}
