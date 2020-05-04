namespace EGID.Application
{
    public static class Roles
    {
        public const string Admin = "Administrator";
        public const string CivilAffairs = "CivilAffairsEmpolyee";
        public const string Doctor = "Doctor";

        public static string[] GetRoles() => new[] { Admin, CivilAffairs, Doctor };
    }
}
