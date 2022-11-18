namespace StudentWebApi.DataModels
{
    public class UpdateStudentRequestDTO
    {
        //şimdi bu güncellenecek ögrencinin içinde ne olacak ?
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public Guid GenderId { get; set; }
        public string? PhsicalAddress { get; set; }
        public string? PostalAddress { get; set; }
    }
}
