namespace EGID.Domain.ValueObjects
{
    public sealed class FullName
    {
        public string FirstName { get; }
        public string SecondName { get; }
        public string ThirdName { get; }
        public string LastName { get; }

        public FullName(string first, string second, string third, string last)
        {
            FirstName = first;
            SecondName = second;
            ThirdName = third;
            LastName = last;
        }

        public override string ToString() => $"{FirstName} {SecondName} {ThirdName} {LastName}";

        public override int GetHashCode() => ToString().GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            return this == (FullName)obj;
        }

        public static bool operator ==(FullName x, FullName y)
        {
            if (x == null && y == null) return true;

            return x != null && y != null && x.ToString() == y.ToString();
        }

        public static bool operator !=(FullName x, FullName y) => !(x == y);
    }
}
