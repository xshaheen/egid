using System.Collections.Generic;

namespace EGID.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string ThirdName { get; private set; }
        public string LastName { get; private set; }

        public FullName() { }

        public FullName(string first, string second, string third, string last)
        {
            FirstName = first;
            SecondName = second;
            ThirdName = third;
            LastName = last;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return SecondName;
            yield return ThirdName;
            yield return LastName;
        }
    }
}