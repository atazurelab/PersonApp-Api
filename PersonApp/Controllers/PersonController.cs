using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonApp.DTO;
using PersonApp.Model;
using PersonApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        //This is test App V1 -Chnages by AT
        private readonly IPersonService _personService; 
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PersonController(IPersonService personService, IMapper mapper, ILogger<PersonController> logger )
        {
            _personService = personService;
            _mapper = mapper;
            _logger = logger; 
        }
         

       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]       
        public async Task<IActionResult> Get()
        {
           var persons  = await   _personService.GetAll();

            if (persons.Count() == 0 )
            {
                return NotFound();
            }

            return   Ok(_mapper.Map<IEnumerable<PersonDto>>(persons));

        }

       
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var person = await Task.FromResult(new PersonDto());

            return  Ok(person) ;
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] PersonDto person)
        {
               Person personToSave = null;

            
                if (person == null)
                {
                     _logger.LogError("PersonDto object sent from client is null.");
                    return NoContent();
                  
                }

                if (!ModelState.IsValid)
                {
                   _logger.LogError("Invalid model state for the PersonDto object");
                    return UnprocessableEntity(ModelState);
                }

                personToSave = _mapper.Map<Person>(person);
                await _personService.Save(personToSave); 

                //Temp Url is generated as Id is not being used now.
                 return   CreatedAtAction("Get", new { Id = 1}, personToSave);
        }
         

    }
}
