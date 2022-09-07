using PersonApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonApp.Services
{
    public interface IPersonService
    {
        Task<bool> Save(Person person);
        Task<IEnumerable<Person>> GetAll();
 

    }

}
