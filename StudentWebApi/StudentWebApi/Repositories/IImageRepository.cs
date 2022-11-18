namespace StudentWebApi.Repositories
{
    public interface IImageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
        //methodumuzun adı Upload,Ne göndereceğim?FormFile(Form Dosyası),2. parametre ise dosyamızın adı olacak (fileName)
    }
}
