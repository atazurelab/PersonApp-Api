using PersonApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonApp.Repository
{
    public interface IPersonRepository
    {
        Task<bool> Save(Person person);

        Task<IEnumerable<Person>> GetAll();
         
    }

}
