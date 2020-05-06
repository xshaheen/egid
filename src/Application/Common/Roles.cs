namespace EGID.Application.Common
{
    public static class Roles
    {
        public const string Admin = "Administrator";
        public const string CivilAffairs = "CivilAffairsEmpolyee";
        public const string Doctor = "Doctor";

        public static string[] GetRoles()
        {
            return new[] {Admin, CivilAffairs, Doctor};
        }
    }
}