namespace StudentWebApi.DomainModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string? PhsicalAddress { get; set; }
        public string? PostalAddress { get; set; }

        //bir de her ögrencinin 1 adresi olacagı için studentId tanımlayıp ögrencileri bu idlere göre bulup çekicem
        
        public Guid studentId { get; set; }
    }
}
