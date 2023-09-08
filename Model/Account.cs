namespace Model
{
    public class Account
    {
        public string TestKey { get; set; }
        public string AccountType { get; set; }
        public string Salutation { get; set; }
        public string Gender { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Mobile { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public string ResidentialStreet { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string ResidentialCountry { get; set; }
        public string AccommodationType { get; set; }
        public string AdditionalDetail { get; set; }
        public string GetAddress()
        {
            return ResidentialStreet + ", " + Suburb + ", " + State + ", " + PostCode;
        }
    }
}