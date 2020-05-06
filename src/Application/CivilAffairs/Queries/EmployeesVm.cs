using System;
using EGID.Domain.Enums;
using EGID.Domain.ValueObjects;

namespace EGID.Application.CivilAffairs.Queries
{
    public class EmployeesVm
    {
        public string Id { get; set; }

        public string CardId { get; set; }

        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public Gender Gender { get; set; }

        public Religion Religion { get; set; }

        public SocialStatus SocialStatus { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhotoUrl { get; set; }
    }
}