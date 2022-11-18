namespace StudentWebApi.DomainModels
{
    public class Gender
    {
        public Guid Id { get; set; }
        //bu ID sayesinde ögrencileri yakalayıp cinsiyet ataması yapıcaz
        public string? Description { get; set; }

        //burada ki description bizim öğrencilerimizin MALE-FEMALE-OTHER durumu
    }
}
