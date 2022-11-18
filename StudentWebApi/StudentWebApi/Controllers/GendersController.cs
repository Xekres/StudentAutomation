using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentWebApi.DataModels;
using StudentWebApi.DomainModels;
using StudentWebApi.Repositories;

namespace StudentWebApi.Controllers
{
    public class GendersController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public GendersController(IStudentRepository studentRepository,IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("[controller]")]
        public async Task< IActionResult> GetAllGendersAsync()
        {
            var genderList = await _studentRepository.GetGendersAsync();
            if (genderList==null||!genderList.Any())
            {
                return NotFound();

            }
            return Ok(_mapper.Map<List<GenderDTO>>(genderList));
            //domain modelden gelen veriyi data modele mapledikten sonra listemizin içine atıyoruz 
        }
    }
}
