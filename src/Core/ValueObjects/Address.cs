namespace EGID.Core.ValueObjects
{
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
        }
    }
}
