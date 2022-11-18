using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentWebApi.DataModels;
using StudentWebApi.DomainModels;
using StudentWebApi.Repositories;

namespace StudentWebApi.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetStudentsAsync();
            return Ok(_mapper.Map<List<Student>>(students));

            //yani ne kadar Field varsa hepsini automapper sayesinde DataModels'e atadık ve bu DataModels bana dönüyor,Apiden de DTO gibi aynı sonucu alıyorum
        }

        [HttpGet]
        [Route("[controller]/{studentId:Guid}")]
        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] Guid studentId)
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId);

            //şimdi de firstordefaulttan boş gelip gelmeme durumunu kontrol edelim
            if (student == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Student>(student));
        }

        //update servisini yazalım
        //update işlemlerinde PUT kullanıyoruz.
        //önce böyle bir ögrenci var mı diye kontol edilmeli,bu kontrol de id ye sahip öğrenci var mı yok mu diye yapılır
        //kontrolden sonra geriye kaydedilmiş öğrenciyi döndürcem
        [HttpPut]
        [Route("[Controller]/{studentId:Guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequestDTO request)
        {
            if (await _studentRepository.Exist(studentId))
            {
                var updatedStudent = await _studentRepository.UpdateStudent(studentId, _mapper.Map<Student>(request));
                //eğer öğrenci güncellendiyse,güncellenen ögrencinin bilgileri gelsin
                if (updatedStudent!=null)
                {
                    return Ok(_mapper.Map<StudentDTO>(updatedStudent));


                }

                
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("[Controller]/{studentId:Guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            //önce böyle bir ögrenci var mı yok mu diye kontrol edelim
            if (await _studentRepository.Exist(studentId))
            {
                var student = await _studentRepository.DeleteStudent(studentId);
                if (student!=null)
                {
                    return Ok(_mapper.Map<StudentDTO>(student));
                }
            }
            return NotFound();
        }


        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody]AddStudentRequestDTO request)
        {
            var student = await _studentRepository.AddStudentAsync(_mapper.Map<Student>(request));
            return CreatedAtAction(nameof(GetStudentByIdAsync), new { studentId = student.Id },
            _mapper.Map<StudentDTO>(student));
        }

        [HttpPost]
        [Route("[Controller]/{studentId:guid}/{upload-image}")]
        public async Task<IActionResult> UploadImage([FromRoute]Guid studentId,IFormFile profileImage)
        {
            var validExtensions = new List<string>
            //validExtensions benim dosya tiplerimin kaydedildiği yerdir.Image Yükleyeceğim için uzantıları da ona uyumlu olmalıdır.
            {
               ".jpeg",
               ".png",
               ".gif",
               ".jpg"
            };
            //eğer profileImage null değilse ve içinde bir dosya var ise yani length i 0 dan büyük ise;
            //profileImage.fileName in GetExtension kullanarak sonunda ki eki alıcam
            //LINQ daki  Contains methodumla içinde bizim elemanlarımız (validExtensionlarımız) var mı diye kontrol edicem,varsa true, yoksa false döndürcem
            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    //böyle bir öğrenci olup olmadıgını da kontrol edelim yoksa kodlarımız patlar.
                    //var ise Guid (unique) bir değer veriyorum,path ini de upload methoduna gönderiyorum
                    if (await _studentRepository.Exist(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await _imageRepository.Upload(profileImage, fileName);
                        //bunu da studentRepository, UpdateProfileImage ismi ile ile Database ime kaydediyorum

                        if (await _studentRepository.UpdateProfileImage(studentId, fileImagePath))
                        {
                            //return olarak fileImagePath i döndürüyorum.
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            }

            return NotFound();
           

        }


    }
}
