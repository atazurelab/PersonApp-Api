using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace PersonApp.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<PersonRepository> _logger;

        public string FileName => "persons.json"; 

        public PersonRepository(IWebHostEnvironment env, ILogger<PersonRepository> logger)
        {
            _env = env;
            _logger = logger;
        }


        public async Task<bool> Save(Person person)
        {
            try
            {
                List<Person> personlist = new List<Person>();

                var path = Path.Combine(_env.ContentRootPath, FileName);

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    personlist = JsonConvert.DeserializeObject<List<Person>>(json);

                }

                personlist.Add(person);


                string jsonToWrite = JsonConvert.SerializeObject(personlist.ToArray(), Formatting.Indented);

                File.WriteAllText(path, jsonToWrite);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }




        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var path = Path.Combine(_env.ContentRootPath, FileName);

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var _data = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
                return await Task.FromResult(_data);

            }



        }

    }

}
