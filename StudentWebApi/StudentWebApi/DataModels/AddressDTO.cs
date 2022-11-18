namespace StudentWebApi.DataModels
{
    public class AddressDTO
    {
        public Guid Id { get; set; }
        public string? PhsicalAddress { get; set; }
        public string? PostalAddress { get; set; }
        public Guid StudentId { get; set; }
    }
}
