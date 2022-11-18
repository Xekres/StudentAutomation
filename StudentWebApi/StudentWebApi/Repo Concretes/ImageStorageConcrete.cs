using StudentWebApi.Repositories;

namespace StudentWebApi.Repo_Concretes
{
    public class ImageStorageConcrete : IImageRepository
    {
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            var filePath=Path.Combine(Directory.GetCurrentDirectory(),@"Resources\Images",fileName);
            //path.combine ile CurrentDirectory (yani studentWebApi projem) içerisine  @resources\images gidiyor.
            //burada fileName diye kaydedicem ör C:User\...\fileName diye
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            //daha sonra bir fileStream oluşturuyorum
            await file.CopyToAsync(fileStream);
            //CopyToAsync methodum ile bunu kaydediyorum.
            return GetServerRelativePath(fileName);
            //kaydettikten sonra da geriye Dosyanın yolunu döneceğim
            //bunu da parametre olarak fileName i alan GetServerRelativePath methoduyla yapıp geri döndürdüm.

        }
        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images",fileName);
        }
    }
}
