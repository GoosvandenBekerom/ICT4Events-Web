using System.Collections.Generic;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class PersonLogic
    {
        private readonly IPersonContext _context;

        public PersonLogic(IPersonContext context)
        {
            _context = context;
        }

        public List<Person> GetAllPersons()
        {
            return _context.GetAll();
        }

        public Person GetByID(int id)
        {
            return _context.GetById(id);
        }

        public bool UpdatePerson(Person person)
        {
            return _context.Update(person);
        }

        public bool Insert(Person person)
        {
            return _context.Insert(person);
        }

        public Person GetLastAdded()
        {
            return _context.GetLastAdded();
        }
    }
}

