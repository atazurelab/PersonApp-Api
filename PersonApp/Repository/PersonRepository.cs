using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PersonApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonApp.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<PersonRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly object _fileAccess = new object();


        private string JsonFilePath => "JsonFilePath"; 

        public PersonRepository(IWebHostEnvironment env, ILogger<PersonRepository> logger, IConfiguration configuration)
        {
            _env = env;
            _logger = logger;
            _configuration = configuration;
        }
            

        public async Task<bool> Save(Person person)
        {
            try
            { 

                List<Person> personlist = new List<Person>();
               
                var path = _configuration[JsonFilePath];  

                lock (_fileAccess)
                {
                   if (File.Exists(path))
                    {
                        using (StreamReader r = new StreamReader(path))
                        {
                            string json = r.ReadToEnd();
                            personlist = JsonConvert.DeserializeObject<List<Person>>(json);

                        }
                    }
                   
                    personlist.Add(person);
                    string jsonToWrite = JsonConvert.SerializeObject(personlist.ToArray(), Formatting.Indented);
                    File.WriteAllText(path, jsonToWrite);
              
                }

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
            IEnumerable<Person> personlist = null;
 
            var path = _configuration[JsonFilePath];

            if (File.Exists(path))
            {
                lock (_fileAccess)
                {
                    using (StreamReader r = new StreamReader(path))
                    {
                        string json = r.ReadToEnd();
                        personlist = JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
                    }

                }
            }
            return await Task.FromResult(personlist);

        }

      
    }

}
