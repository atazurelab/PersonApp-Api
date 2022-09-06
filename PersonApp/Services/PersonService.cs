using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PersonApp.Model;
using PersonApp.Repository;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PersonApp.Services
{
    public class PersonService : IPersonService
    {

      
        private readonly IPersonRepository _personRepository;
       

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;             
        }



        public async Task<bool> Save(Person person)
        {            
            return    await _personRepository.Save(person);        
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _personRepository.GetAll();
        }
    }

}
